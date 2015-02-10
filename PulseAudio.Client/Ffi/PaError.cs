using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaError
    {
        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pa_context_errno(IntPtr context);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pa_strerror")]
        private static extern IntPtr pa_strerror_intptr(int error);

        public static string pa_strerror(int error)
        {
            var ptr = pa_strerror_intptr(error);
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}