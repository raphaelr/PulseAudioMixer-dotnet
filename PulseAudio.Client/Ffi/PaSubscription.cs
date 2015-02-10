using System;
using System.Runtime.InteropServices;

namespace PulseAudio.Client.Ffi
{
    internal static class PaSubscription
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SubscribeCallback(IntPtr context, uint @event, uint index, IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pa_context_set_subscribe_callback(IntPtr context, SubscribeCallback cb,
            IntPtr userdata);

        [DllImport(PaCommon.Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pa_context_subscribe(IntPtr context, SubscriptionMask mask,
            PaContext.SuccessCallback cb, IntPtr userdata);

        [Flags]
        public enum SubscriptionMask : uint
        {
            Null = 0x0000U,
            Sink = 0x0001U,
            Source = 0x0002U,
            SinkInput = 0x0004U,
            SourceOutput = 0x0008U,
            Module = 0x0010U,
            Client = 0x0020U,
            SampleCache = 0x0040U,
            Server = 0x0080U,
            Autoload = 0x0100U,
            Card = 0x0200U,
            All = 0x02ffU
        }
    }
}