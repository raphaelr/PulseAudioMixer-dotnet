using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaIntrospection
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EnumCallback(IntPtr context, IntPtr obj, int status, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_get_sink_info_list(IntPtr context, EnumCallback callback,
            IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_get_sink_info_by_index(IntPtr context, uint index, EnumCallback callback,
            IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_get_sink_input_info_list(IntPtr context, EnumCallback callback,
            IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_get_sink_input_info_by_index(IntPtr context, uint index,
            EnumCallback callback, IntPtr userdata);
    }
}