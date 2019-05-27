using System.Runtime.InteropServices;

namespace Pedreizor.Internal
{
    public static class OperationalSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static string GetLibExtension()
        {
            if (IsLinux()) return "so";

            if (IsMacOS()) return "dylib";

            return "dll";
        }
    }
}
