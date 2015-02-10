using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PaSinkInfo
    {
        public IntPtr Name;
        public int Index;
        public IntPtr Description;
        public PaSampleSpec SampleSpec;
        public PaChannelMap ChannelMap;
        public int OwnerModule;
        public PaCVolume Volume;
        public int Mute;
        public uint MonitorSource;
        public IntPtr MonitorSourceName;
        public long Latency;
        public IntPtr Driver;
        public int Flags;
        public IntPtr Proplist;
        public long ConfiguredLatency;
        public uint BaseVolume;
        public int State;
        public uint VolumeStepCount;
        public uint Card;
        public uint PortCount;
        public IntPtr Ports;
        public IntPtr ActivePort;
        public byte FormatCount;
        public IntPtr Formats;
    }
}