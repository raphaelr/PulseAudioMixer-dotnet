using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PaCVolume
    {
        public byte NumChannels;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = PaCommon.ChannelsMax)]
        public uint[] Volumes;
    }
}