using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaControl
    {
        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_set_sink_input_mute(IntPtr context, int index, int mute,
            PaContext.SuccessCallback cb, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_set_sink_input_volume(IntPtr context, int index, ref PaCVolume volume,
            PaContext.SuccessCallback cb, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_set_sink_mute_by_index(IntPtr context, int index, int mute,
            PaContext.SuccessCallback cb, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_set_sink_volume_by_index(IntPtr context, int index, ref PaCVolume volume,
            PaContext.SuccessCallback cb, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_set_sink_mute_by_name(IntPtr context, string name, int mute,
            PaContext.SuccessCallback cb, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_set_sink_volume_by_name(IntPtr context, string name, ref PaCVolume volume,
            PaContext.SuccessCallback cb, IntPtr userdata);
    }
}