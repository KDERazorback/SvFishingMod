using StardewModdingAPI;
using StardewValley.Tools;

namespace SvFishingMod
{
    public sealed partial class FishingMod : Mod
    {
        private IReflectedField<int>? maxFishingBiteTimeField = null;

        private IReflectedField<int>? minFishingBiteTimeField = null;

        private int bobberBarHeight // Hardcoded Max: 568
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<int>(FishMenu, nameof(bobberBarHeight), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<int>(FishMenu, nameof(bobberBarHeight), true).SetValue(value);
            }
        }

        private bool bossFish
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(bossFish), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(bossFish), true).SetValue(value);
            }
        }
        private float difficulty
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<float>(FishMenu, nameof(difficulty), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<float>(FishMenu, nameof(difficulty), true).SetValue(value);
            }
        }

        private float distanceFromCatching
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<float>(FishMenu, nameof(distanceFromCatching), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<float>(FishMenu, nameof(distanceFromCatching), true).SetValue(value);
            }
        }

        private bool fadeOut
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(fadeOut), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(fadeOut), true).SetValue(value);
            }
        }

        private int fishQuality
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<int>(FishMenu, nameof(fishQuality), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<int>(FishMenu, nameof(fishQuality), true).SetValue(value);
            }
        }

        private int fishSize
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<int>(FishMenu, nameof(fishSize), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<int>(FishMenu, nameof(fishSize), true).SetValue(value);
            }
        }

        private bool fromFishPond
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(fromFishPond), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(fromFishPond), true).SetValue(value);
            }
        }

        private bool handledFishResult
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(handledFishResult), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(handledFishResult), true).SetValue(value);
            }
        }

        private int maxFishingBiteTime
        {
            get
            {
                if (maxFishingBiteTimeField == null)
                    maxFishingBiteTimeField = Helper.Reflection.GetField<int>(typeof(FishingRod), nameof(maxFishingBiteTime), true);

                return maxFishingBiteTimeField.GetValue();
            }
            set
            {
                if (maxFishingBiteTimeField == null)
                    maxFishingBiteTimeField = Helper.Reflection.GetField<int>(typeof(FishingRod), nameof(maxFishingBiteTime), true);

                maxFishingBiteTimeField.SetValue(value);
            }
        }

        private int maxFishSize
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<int>(FishMenu, nameof(maxFishSize), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<int>(FishMenu, nameof(maxFishSize), true).SetValue(value);
            }
        }

        private int minFishingBiteTime
        {
            get
            {
                if (minFishingBiteTimeField == null)
                    minFishingBiteTimeField = Helper.Reflection.GetField<int>(typeof(FishingRod), nameof(minFishingBiteTime), true);

                return minFishingBiteTimeField.GetValue();
            }
            set
            {
                if (minFishingBiteTimeField == null)
                    minFishingBiteTimeField = Helper.Reflection.GetField<int>(typeof(FishingRod), nameof(minFishingBiteTime), true);

                minFishingBiteTimeField.SetValue(value);
            }
        }
        private bool perfect
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(perfect), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(perfect), true).SetValue(value);
            }
        }

        private bool treasure
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(treasure), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(treasure), true).SetValue(value);
            }
        }

        private bool treasureCaught
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<bool>(FishMenu, nameof(treasureCaught), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<bool>(FishMenu, nameof(treasureCaught), true).SetValue(value);
            }
        }

        private string whichFish
        {
            get
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                return Helper.Reflection.GetField<string>(FishMenu, nameof(whichFish), true).GetValue();
            }
            set
            {
                if (FishMenu == null) throw new NullReferenceException(nameof(FishMenu));
                Helper.Reflection.GetField<string>(FishMenu, nameof(whichFish), true).SetValue(value);
            }
        }
    }
}