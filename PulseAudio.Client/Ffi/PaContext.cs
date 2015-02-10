using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaContext
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NotifyCallback(IntPtr context, IntPtr userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SuccessCallback(IntPtr context, int success, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_new(IntPtr mainloopApi, string applicationName);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_context_unref(IntPtr context);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_context_set_state_callback(IntPtr context, NotifyCallback callback, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern State pa_context_get_state(IntPtr context);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pa_context_connect(IntPtr context, string server, Flags flags, IntPtr spawnApi);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_context_disconnect(IntPtr context);

        internal enum State
        {
            Unconnected,
            Connecting,
            Authorizing,
            SettingName,
            Ready,
            Failed,
            Terminated
        }

        [Flags]
        internal enum Flags
        {
            None = 0,
            NoAutoSpawn = 1,
            NoFail = 2,
        }
    }
}