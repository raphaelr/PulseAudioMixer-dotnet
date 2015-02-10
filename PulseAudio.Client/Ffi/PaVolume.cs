using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaVolume
    {
        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint pa_sw_volume_from_linear(double vol);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern double pa_sw_volume_to_linear(uint vol);
    }
}