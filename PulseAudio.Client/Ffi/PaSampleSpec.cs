using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PaSampleSpec
    {
        public int SampleFormat;
        public int Rate;
        public byte NumChannels;
    }
}