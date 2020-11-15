using System.Runtime.InteropServices;

namespace PicturaBuildTools
{
    public class OperatingSystem
    {
        public static bool isWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool isMacOS() => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        public static bool isLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        private static string name;
        public static string Name
        {
            get { return isWindows() ? "Windows" : (isMacOS() ? "MacOS" : "Unix"); }
            set { name = value; }
        }

    }
}