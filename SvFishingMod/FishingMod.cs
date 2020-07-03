using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace SvFishingMod
{
    public class FishingMod : Mod
    {
        protected int bobberBarHeight // Hardcoded Max: 568
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<int>(fishMenu, nameof(bobberBarHeight), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<int>(fishMenu, nameof(bobberBarHeight), true).SetValue(value);
            }
        }

        protected bool bossFish
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(bossFish), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(bossFish), true).SetValue(value);
            }
        }

        protected float difficulty
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<float>(fishMenu, nameof(difficulty), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<float>(fishMenu, nameof(difficulty), true).SetValue(value);
            }
        }

        protected float distanceFromCatching
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<float>(fishMenu, nameof(distanceFromCatching), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<float>(fishMenu, nameof(distanceFromCatching), true).SetValue(value);
            }
        }

        protected bool EnableDebugOutput { get; set; } = false;
        protected bool fadeOut
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(fadeOut), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(fadeOut), true).SetValue(value);
            }
        }

        protected SortedDictionary<int, string> fishList { get; set; } = null;
        protected BobberBar fishMenu { get; set; }
        protected int fishQuality
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<int>(fishMenu, nameof(fishQuality), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<int>(fishMenu, nameof(fishQuality), true).SetValue(value);
            }
        }

        protected int fishSize
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<int>(fishMenu, nameof(fishSize), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<int>(fishMenu, nameof(fishSize), true).SetValue(value);
            }
        }

        protected bool fromFishPond
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(fromFishPond), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(fromFishPond), true).SetValue(value);
            }
        }

        protected bool handledFishResult
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(handledFishResult), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(handledFishResult), true).SetValue(value);
            }
        }

        protected int maxFishSize
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<int>(fishMenu, nameof(maxFishSize), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<int>(fishMenu, nameof(maxFishSize), true).SetValue(value);
            }
        }

        protected bool perfect
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(perfect), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(perfect), true).SetValue(value);
            }
        }

        protected bool treasure
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(treasure), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(treasure), true).SetValue(value);
            }
        }

        protected bool treasureCaught
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<bool>(fishMenu, nameof(treasureCaught), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<bool>(fishMenu, nameof(treasureCaught), true).SetValue(value);
            }
        }

        protected int whichFish
        {
            get
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                return Helper.Reflection.GetField<int>(fishMenu, nameof(whichFish), true).GetValue();
            }
            set
            {
                if (fishMenu == null) throw new NullReferenceException(nameof(fishMenu));
                Helper.Reflection.GetField<int>(fishMenu, nameof(whichFish), true).SetValue(value);
            }
        }
        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.MenuChanged += Display_MenuChanged;
            helper.ConsoleCommands.Add("sv_fishing_debug", "Enables or disables Debug mode on the SvFishingMod.\nUsage: sv_fishing_debug 0|1", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_enabled", "Enables or disables SvFishingMod.\nUsage: sv_fishing_enabled 0|1", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_autoreel", "Enables or disables the Auto reel functionality of the SvFishingMod.\nUsage: sv_fishing_autoreel 0|1.", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_reload", "Reloads from disk the configuration file used by the SvFishingMod.\nUsage: sv_fishing_reload", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_search", "Searches the fish list for a fish that contains the specified string on its name.\nUsage: sv_fishing_search <keyword>", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_setfish", "Forces the next fishing event to give a fish with the specified id.\nUsage: sv_fishing_setfish <fish_id>\nUse sv_fishing_search to get the id of a given fish by name.\nUse -1 as the fish id to restore original game functionality.", HandleCommand);
        }

        protected override void Dispose(bool disposing)
        {
            Helper.Events.Display.MenuChanged -= Display_MenuChanged;
            base.Dispose(disposing);
        }

        protected string GetFishNameFromId(int id)
        {
            if (fishList == null) LoadFishList();

            if (fishList.TryGetValue(id, out string name))
                return name;

            return "";
        }

        protected void HandleCommand(string command, string[] args)
        {
            if (string.Equals(command, "sv_fishing_debug", StringComparison.OrdinalIgnoreCase))
            {
                if (args != null && args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                    {
                        EnableDebugOutput = true;
                    }
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                    {
                        EnableDebugOutput = false;
                    }
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_enabled", StringComparison.OrdinalIgnoreCase))
            {
                if (args != null && args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                    {
                        Settings.Instance.DisableMod = false;
                    }
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                    {
                        Settings.Instance.DisableMod = true;
                    }
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_autoreel", StringComparison.OrdinalIgnoreCase))
            {
                if (args != null && args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                    {
                        Settings.Instance.AutoReelFish = true;
                    }
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                    {
                        Settings.Instance.AutoReelFish = false;
                    }
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_reload", StringComparison.OrdinalIgnoreCase))
            {
                Game1.playSound("jingle1");
                Settings.Instance = null;
                Monitor.Log("Successfully reloaded SvFishingMod settings from disk.", LogLevel.Info);
                return;
            }

            if (string.Equals(command, "sv_fishing_search", StringComparison.OrdinalIgnoreCase))
            {
                if (fishList == null) LoadFishList();
                int matchCount = 0;
                foreach (var fish in fishList)
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
                if (args != null && args.Length > 0)
                {
                    if (int.TryParse(args[0].Trim(), out int fishId))
                    {
                        Settings.Instance.OverrideFishType = fishId;
                        Monitor.Log(string.Format("Done. The next reeled fish will be {0}: {1}.", fishId, GetFishNameFromId(fishId)), LogLevel.Info);
                    }
                    else
                        Monitor.Log("Invalid fish id specified.", LogLevel.Info);
                }
                return;
            }
        }

        protected void LoadFishList()
        {
            fishList = new SortedDictionary<int, string>();
            var fishData = Game1.content.Load<Dictionary<int, string>>("Data\\Fish");
            foreach (var fish in fishData)
            {
                string[] segments = fish.Value.Split('/');
                string name = segments[0] + '/' + segments[segments.Length - 1];
                fishList.Add(fish.Key, name);
            }

            if (EnableDebugOutput) Monitor.Log(string.Format("Loaded {0} fishes from internal content database.", fishList.Count));
        }

        private void Display_MenuChanged(object sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            BobberBar fishBarMenu = e.NewMenu as BobberBar;
            FishingRod fishTool = Game1.player.CurrentTool as FishingRod;

            if (fishBarMenu == null || fishTool == null || Settings.Instance.DisableMod)
                return;

            fishMenu = fishBarMenu;

            int attachmentValue = fishTool.attachments[0] == null ? -1 : fishTool.attachments[0].parentSheetIndex;
            bool caughtDouble = Settings.Instance.AlwaysCatchDoubleFish || (bossFish && attachmentValue == 774 && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0);

            if (Settings.Instance.OverrideFishType >= 0) whichFish = Settings.Instance.OverrideFishType;
            if (Settings.Instance.OverrideFishQuality >= 0) fishQuality = Settings.Instance.OverrideFishQuality;
            if (Settings.Instance.AlwaysPerfectCatch) perfect = true;
            if (Settings.Instance.AlwaysCatchTreasure)
            {
                treasure = true;
                treasureCaught = true;
            }
            if (Settings.Instance.DistanceFromCatchingOverride >= 0) distanceFromCatching = Settings.Instance.DistanceFromCatchingOverride;
            if (Settings.Instance.OverrideBarHeight >= 0) bobberBarHeight = Settings.Instance.OverrideBarHeight;

            if (Settings.Instance.AutoReelFish)
            {
                // Emulate BobberBar.update() when fadeOut = true
                if (EnableDebugOutput) Monitor.Log(string.Format("Auto-reeling fish with id: {0}. : {1},", whichFish, GetFishNameFromId(whichFish)));
                fadeOut = true;
                handledFishResult = true;
                distanceFromCatching = 1;
                fishTool.pullFishFromWater(whichFish, fishSize, fishQuality, (int)difficulty, treasure, perfect, fromFishPond, caughtDouble);
                Game1.exitActiveMenu();
            }

            Game1.setRichPresence("location", (object)Game1.currentLocation.Name);
        }
    }
}