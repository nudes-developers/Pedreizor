using System;
using System.Runtime.InteropServices;

namespace Nudes.Pedreizor.Internal
{
    internal static class OperationalSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static string GetLibExtension()
        {
            if (IsWindows()) return "dll";

            if (IsLinux()) return "so";

            if (IsMacOS()) return "dylib";

            throw new NotImplementedException("Invalid operational system, this library works only for distributions windows, linux and macOS");
        }
    }
}
