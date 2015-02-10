using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaOperation
    {
        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_operation_unref(IntPtr operation);
    }
}