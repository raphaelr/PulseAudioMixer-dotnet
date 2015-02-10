using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaThreadedMainLoop
    {
        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_threaded_mainloop_new();

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pa_threaded_mainloop_start(IntPtr mainloop);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_threaded_mainloop_lock(IntPtr mainloop);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_threaded_mainloop_unlock(IntPtr mainloop);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_threaded_mainloop_stop(IntPtr mainloop);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_threaded_mainloop_free(IntPtr mainloop);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_threaded_mainloop_get_api(IntPtr mainloop);
    }
}