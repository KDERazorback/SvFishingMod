using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace SvFishingMod
{
    public sealed partial class FishingMod : Mod
    {
        private static int _defaultMaxFishingBiteTime = -1;
        private static int _defaultMinFishingBiteTime = -1;
        private const string MultiplayerSettingsMessageType = "EnforcedSessionSettings";

        private CircularBuffer<string>? CircularFishList { get; set; } = null;
        private SortedList<string, string>? FishList { get; set; } = null;
        private BobberBar? FishMenu { get; set; } = null;
        private bool EnableDebugOutput { get; set; } = false;
        private ISemanticVersion? CurrentVersion { get; set; } = null;
        private string CurrentModId { get; set; } = String.Empty;

        public override void Entry(IModHelper helper)
        {
            Settings.HelperInstance = helper;
            Settings.MonitorInstance = Monitor;
            Settings.ConfigFilePath = "svfishmod.json";
            Settings.LoadFromFile();

            CurrentVersion = helper.ModRegistry.Get(helper.ModRegistry.ModID)?.Manifest.Version;
            CurrentModId = helper.ModRegistry.ModID;

            helper.Events.Display.MenuChanged += Display_MenuChanged;
            helper.Events.Input.ButtonPressed += Input_ButtonPressed;
            helper.Events.GameLoop.ReturnedToTitle += GameLoop_ReturnedToTitle;
            helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
            helper.Events.Multiplayer.ModMessageReceived += Multiplayer_ModMessageReceived;
            helper.Events.Multiplayer.PeerConnected += Multiplayer_PeerConnected;

            helper.ConsoleCommands.Add("sv_fishing_debug", "Enables or disables Debug mode on the SvFishingMod.\nUsage: sv_fishing_debug 0|1", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_enabled", "Enables or disables SvFishingMod.\nUsage: sv_fishing_enabled 0|1", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_autoreel", "Enables or disables the Auto reel functionality of the SvFishingMod.\nUsage: sv_fishing_autoreel 0|1.", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_reload", "Reloads from disk the configuration file used by the SvFishingMod.\nUsage: sv_fishing_reload", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_search", "Searches the fish list for a fish that contains the specified string on its name.\nUsage: sv_fishing_search <keyword>", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_setfish", "Forces the next fishing event to give a fish with the specified id.\nUsage: sv_fishing_setfish <fish_id>\nUse sv_fishing_search to get the id of a given fish by name.\nUse -1 or 0 as the fish id to restore original game functionality.", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_fishcycling", "Enables or disables the reeled fish cycling feature.\nUsage: sv_fishing_fishcycling 0|1\nWhen enabled, this feature will allow you to automatically reel all possibles fishes one after another each time you throw your fishrod.", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_bitedelay", "Enables or disables the bite delay for fishes once the rod has been casted into the water.\nUsage: sv_fishing_bitedelay 0|1\nIf the value is 1, the original game mechanics will be used and the fish will bite after a random amount of time.", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_status", "Displays the current mod status, including synced data from the host in multiplayer sessions.\nUsage: sv_fishing_status\nThis command doesnt accept any parameters.", HandleCommand);

            _defaultMaxFishingBiteTime = maxFishingBiteTime;
            _defaultMinFishingBiteTime = minFishingBiteTime;
        }

        private void Multiplayer_PeerConnected(object? sender, StardewModdingAPI.Events.PeerConnectedEventArgs e)
        {
            if (!e.Peer.IsHost)
            {
                // Send Settings update package
                if (e.Peer.HasSmapi)
                {
                    Monitor.Log(string.Format("Sending mod settings to newly connected peer with ID: {0}, GameVersion {1} and SMAPIVersion {2}.", e.Peer.PlayerID, e.Peer.GameVersion, e.Peer.ApiVersion));
                    Helper.Multiplayer.SendMessage(Settings.Local, MultiplayerSettingsMessageType, new string[] { CurrentModId }, new long[] { e.Peer.PlayerID });
                }
                else
                {
                    Monitor.Log(string.Format("Connected peer {0} doesnt have SMAPI installed, hence the mod settings could not be synced.", e.Peer.PlayerID));
                }
            }
        }

        private void Multiplayer_ModMessageReceived(object? sender, StardewModdingAPI.Events.ModMessageReceivedEventArgs e)
        {
            if (!string.Equals(e.FromModID, CurrentModId, StringComparison.OrdinalIgnoreCase))
                return;

            ISemanticVersion? ver = Helper.Multiplayer.GetConnectedPlayer(e.FromPlayerID)?.GetMod(CurrentModId)?.Version ?? null;
            bool compatible = ver?.IsNewerThan(CurrentVersion) ?? false;

            if (!compatible)
            {
                Monitor.Log(string.Format("Cannot use this mod during this online session because the host is currently using a more recent version of the mod. Update this mod to enable it on this server. RemoteVer: {0}. LocalVer: {1}", ver?.ToString() ?? "Null", CurrentVersion));
                Settings.Remote = Settings.DefaultDisabled;
                return;
            }

            foreach (Farmer farmer in Game1.getAllFarmers())
            {
                if (farmer.UniqueMultiplayerID == e.FromPlayerID)
                {
                    if (farmer.IsMainPlayer)
                    {
                        // Message received from server
                        if (string.Equals(e.Type, MultiplayerSettingsMessageType, StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                Settings data = e.ReadAs<Settings>();

                                Settings.Remote = data;
                                Monitor.Log(string.Format("Remote settings updated from server. FarmerId: {0}.", e.FromPlayerID));
                            }
                            catch (Exception ex)
                            {
                                Monitor.Log(string.Format("Remote settings cannot be updated from server. FarmerId: {0}. Ex: {1}. Msg: {2}. InnerEx: {3}", e.FromPlayerID, ex.GetType().Name, ex.Message, ex.InnerException?.GetType().Name ?? "None"));
                            }
                        }
                        else
                        {
                            Monitor.Log(string.Format("Unknown mod message received from server. FarmerId: {0}. Type: {1}.", e.FromPlayerID, e.Type));
                        }
                    }
                    else
                    {
                        Monitor.Log(string.Format("Remote mod message received from a non-host farmer. FarmerId: {0}. Type: {1}.", e.FromPlayerID, e.Type));
                    }

                    return;
                }
            }

            Monitor.Log(string.Format("Remote mod message received from an unknown farmer. PlayerId: {0}. Type: {1}", e.FromPlayerID, e.Type));
        }

        private void GameLoop_SaveLoaded(object? sender, StardewModdingAPI.Events.SaveLoadedEventArgs e)
        {
            Settings.Remote = null;
            if (Game1.multiplayerMode == 0)
            {
                Settings.IsMultiplayerSession = false;
                Settings.IsServer = true;
            }
            else
            {
                Settings.IsMultiplayerSession = true;
                Settings.IsServer = Game1.multiplayerMode == Game1.multiplayerServer;
            }
        }

        private void GameLoop_ReturnedToTitle(object? sender, StardewModdingAPI.Events.ReturnedToTitleEventArgs e)
        {
            Settings.Remote = null;
        }

        private void Input_ButtonPressed(object? sender, StardewModdingAPI.Events.ButtonPressedEventArgs e)
        {
            FishingRod? rod = Game1.player.CurrentTool as FishingRod;

            if (rod is null)
                return;

            if (Settings.Active.RemoveBiteDelay)
            {
                minFishingBiteTime = -20000;
                maxFishingBiteTime = 1;
            }    
            else
            {
                minFishingBiteTime = _defaultMinFishingBiteTime;
                maxFishingBiteTime = _defaultMaxFishingBiteTime;
            }
        }

        protected override void Dispose(bool disposing)
        {
            Helper.Events.Display.MenuChanged -= Display_MenuChanged;
            base.Dispose(disposing);
        }

        private void Display_MenuChanged(object? sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            BobberBar? fishBarMenu = e.NewMenu as BobberBar;
            FishingRod? fishTool = Game1.player.CurrentTool as FishingRod;

            if (fishBarMenu is null || fishTool is null || Settings.Active.DisableMod)
                return;

            FishMenu = fishBarMenu;

            int attachmentValue = fishTool.attachments[0]?.parentSheetIndex.Value ?? -1;
            bool caughtDouble = Settings.Active.AlwaysCatchDoubleFish || (bossFish && attachmentValue == 774 && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0);
            string setMailFlag = String.Empty;

            if (!string.IsNullOrWhiteSpace(Settings.Active.OverrideFishType)) whichFish = Settings.Active.OverrideFishType;
            if (Settings.Active.OverrideFishQuality >= 0) fishQuality = Settings.Active.OverrideFishQuality;
            if (Settings.Active.AlwaysPerfectCatch) perfect = true;
            if (Settings.Active.AlwaysCatchTreasure)
            {
                treasure = true;
                treasureCaught = true;
            }
            if (Settings.Active.DistanceFromCatchingOverride >= 0) distanceFromCatching = Settings.Active.DistanceFromCatchingOverride;
            if (Settings.Active.OverrideBarHeight >= 0) bobberBarHeight = Settings.Active.OverrideBarHeight;
            if (Settings.Active.ReelFishCycling)
            {
                if (CircularFishList is null) LoadFishList();
                if (CircularFishList is null) throw new NullReferenceException("Cannot load fish list.");
                whichFish = CircularFishList.ElementAt(0);
                CircularFishList.Rotate(1);
            }

            if (Settings.Active.AutoReelFish)
            {
                // Emulate BobberBar.update() when fadeOut = true
                if (EnableDebugOutput) Monitor.Log(string.Format("Auto-reeling fish with id: {0}. : {1},", whichFish, GetFishNameFromId(whichFish)));
                fadeOut = true;
                handledFishResult = true;
                distanceFromCatching = 1;
                fishTool.pullFishFromWater(whichFish, fishSize, fishQuality, (int)difficulty, treasure, perfect, fromFishPond, setMailFlag, bossFish, caughtDouble ? 2 : 1);
                Game1.exitActiveMenu();
            }

            Game1.setRichPresence("location", Game1.currentLocation.Name);
        }

        private string GetFishNameFromId(string id)
        {
            if (FishList is null) LoadFishList();

            if (FishList?.TryGetValue(id, out string? name) ?? false)
                return name;

            return "";
        }

        private void HandleCommand(string command, string[] args)
        {
            if (string.Equals(command, "sv_fishing_debug", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                        EnableDebugOutput = true;
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                        EnableDebugOutput = false;
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_enabled", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                        Settings.Local.DisableMod = false;
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                        Settings.Local.DisableMod = true;
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_autoreel", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                        Settings.Local.AutoReelFish = true;
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                        Settings.Local.AutoReelFish = false;
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_reload", StringComparison.OrdinalIgnoreCase))
            {
                Game1.playSound("jingle1");
                Settings.ClearLocalSettings();
                Monitor.Log("Successfully reloaded SvFishingMod settings from disk.", LogLevel.Info);
                return;
            }

            if (string.Equals(command, "sv_fishing_search", StringComparison.OrdinalIgnoreCase))
            {
                if (FishList is null) LoadFishList();
                if (FishList is null) throw new NullReferenceException("Cannot load fish list.");
                int matchCount = 0;
                foreach (var fish in FishList)
                {
                    foreach (string word in args)
                    {
                        if (fish.Value.ToLowerInvariant().Contains(word.ToLowerInvariant().Trim()))
                        {
                            Monitor.Log(string.Format("   {0}: {1}", fish.Key, fish.Value), LogLevel.Info);
                            matchCount++;
                        }
                    }
                }
                Monitor.Log(string.Format("Found a total of {0:N0} fishes.", matchCount), LogLevel.Info);
                return;
            }

            if (string.Equals(command, "sv_fishing_setfish", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length > 0)
                {
                    var fishId = args[0].Trim();
                    if (fishId is "0" or "-1")
                    {
                        Settings.Local.OverrideFishType = String.Empty;
                        Monitor.Log("Done. The next fish will be a random one.", LogLevel.Info);
                    }
                    else
                    {
                        Settings.Local.OverrideFishType = fishId;
                        Monitor.Log(string.Format("Done. The next reeled fish will be {0}: {1}.", fishId, GetFishNameFromId(fishId)), LogLevel.Info);
                    }
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_fishcycling", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                        Settings.Local.ReelFishCycling = true;
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                        Settings.Local.ReelFishCycling = false;
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_bitedelay", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                        Settings.Local.RemoveBiteDelay = false;
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                        Settings.Local.RemoveBiteDelay = true;
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_status", StringComparison.OrdinalIgnoreCase))
            {
                Monitor.Log("SvFishingMod V. " + ModManifest.Version, LogLevel.Info);
                Monitor.Log("\tTime: " + DateTime.Now, LogLevel.Info);
                Monitor.Log("\tIs Multiplayer Session: " + (Settings.IsMultiplayerSession ? "YES" : "NO"), LogLevel.Info);
                Monitor.Log("\tIs Server Mode: " + (Settings.IsServer? "YES" : "NO"), LogLevel.Info);
                Monitor.Log("\tRemote Settings Received: " + (Settings.RemoteSettingsSet ? "YES" : "NO"), LogLevel.Info);
                Monitor.Log("\tLocal Config File Path: " + Settings.ConfigFilePath, LogLevel.Info);
                Monitor.Log("\tActive Settings: " + (Settings.Active == Settings.Local ? "LOCAL" : "REMOTE"), LogLevel.Info);
                if (Settings.IsServer)
                    Monitor.Log("\tEnforce Multiplayer Settings: " + (Settings.Local.EnforceMultiplayerSettings ? "YES" : "NO"), LogLevel.Info);
                return;
            }
        }

        private void LoadFishList()
        {
            if (FishList is not null)
                FishList.Clear();
            else
                FishList = new SortedList<string, string>();

            var fishData = Game1.content.Load<Dictionary<string, string>>("Data\\Fish");
            CircularFishList = new CircularBuffer<string>(fishData.Count);
            foreach (var fish in fishData)
            {
                string[] segments = fish.Value.Split('/');
                string name = segments[0] + '/' + segments[segments.Length - 1];
                FishList.Add(fish.Key, name);

                // if (FirstFishId == -1 || fish.Key < FirstFishId)
                //     FirstFishId = fish.Key;
                // if (LastFishId == -1 || fish.Key > LastFishId)
                //     LastFishId = fish.Key;

                CircularFishList.InsertBackwards(fish.Key);
            }

            if (EnableDebugOutput) Monitor.Log(string.Format("Loaded {0} fishes from internal content database.", FishList.Count));
        }
    }
}