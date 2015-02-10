using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaCommon
    {
        public const string Library = "libpulse-0.dll";
        public const int ChannelsMax = 32;

        public static string GetString(IntPtr name)
        {
            return Marshal.PtrToStringAnsi(name);
        }
    }
}