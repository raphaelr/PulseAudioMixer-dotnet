using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PaSinkInputInfo
    {
        public int Index;
        public IntPtr Name;
        public int OwnerModule;
        public int Client;
        public int Sink;
        public PaSampleSpec SampleSpec;
        public PaChannelMap ChannelMap;
        public PaCVolume Volume;
        public ulong BufferUsec;
        public ulong SinkUsec;
        public IntPtr ResampleMethod;
        public IntPtr Driver;
        public int Mute;
        public IntPtr Proplist;
        public int Corked;
        public int HasVolume;
        public int VolumeWritable;
        public IntPtr FormatInfo;
    }
}