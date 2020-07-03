using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace SvFishingMod
{
    [DataContract]
    public class Settings
    {
        private static Settings _instance = null;
        private static string _moduleWorkingDirectory = null;
        private float _distanceFromCatchingOverride = -1;
        private int _overrideFishQuality = -1;
        private int _overrideFishType = -1;

        public static Settings Instance
        {
            get
            {
                if (_instance == null) _instance = LoadFromFile();

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public static string ModuleWorkingDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_moduleWorkingDirectory))
                {
                    FileInfo fi = new FileInfo(Assembly.GetAssembly(typeof(Settings)).Location);
                    _moduleWorkingDirectory = fi.Directory.FullName;
                }

                return _moduleWorkingDirectory;
            }
        }

        [DataMember] public bool AlwaysCatchDoubleFish { get; set; } = false;
        [DataMember] public bool AlwaysCatchTreasure { get; set; } = false;
        [DataMember] public bool AlwaysPerfectCatch { get; set; } = true;
        [DataMember] public bool AutoReelFish { get; set; } = true;
        [DataMember] public bool DisableMod { get; set; } = false;

        [DataMember]
        public float DistanceFromCatchingOverride
        {
            get
            {
                if (_distanceFromCatchingOverride > 1.0f)
                    return 1.0f;
                if (_distanceFromCatchingOverride < 0.0f)
                    return 0.0f;

                return _distanceFromCatchingOverride;
            }
            set
            {
                _distanceFromCatchingOverride = value;
            }
        }

        [DataMember] public int OverrideBarHeight { get; set; } = -1;

        [DataMember]
        public int OverrideFishQuality
        {
            get
            {
                if (_overrideFishQuality > 4)
                    return 4;
                if (_overrideFishQuality < 0)
                    return -1;

                return _overrideFishQuality;
            }
            set
            {
                _overrideFishQuality = value;
            }
        }

        [DataMember]
        public int OverrideFishType
        {
            get
            {
                if (_overrideFishType == -1)
                    return -1;
                if (_overrideFishType < 128)
                    return 128;

                return _overrideFishType;
            }
            set
            {
                _overrideFishType = value;
            }
        }

        [DataMember] public bool ReelFishCycling { get; set; } = false;

        public static Settings LoadFromFile()
        {
            return LoadFromFile(Path.Combine(ModuleWorkingDirectory, "svfishmod.cfg"));
        }

        public static Settings LoadFromFile(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            if (!fi.Exists)
            {
                Settings def = new Settings(); // Default settings
                def.SaveToFile(fi.FullName);
                return def;
            }
            else
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(Settings));
                Settings output = null;
                using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    output = ser.ReadObject(fs) as Settings;

                return output;
            }
        }

        public void SaveToFile(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            if (!fi.Directory.Exists)
                fi.Directory.Create();

            DataContractSerializer ser = new DataContractSerializer(typeof(Settings));
            using (FileStream fs = new FileStream(fi.FullName, FileMode.Create, FileAccess.Write, FileShare.None))
                ser.WriteObject(fs, this);
        }
    }
}