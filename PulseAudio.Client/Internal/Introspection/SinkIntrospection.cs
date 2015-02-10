using System;
using PulseAudio.Client.Ffi;
using PulseAudio.Client.Internal.Subscription;

namespace PulseAudio.Client.Internal.Introspection
{
    internal class SinkIntrospection : Introspection<ISink, PaSinkInfo>
    {
        public SinkIntrospection(Context context) : base(context)
        {
        }

        public override string ObjectType
        {
            get { return "Sink"; }
        }

        protected override EventFacility EventFacility
        {
            get { return EventFacility.Sink; }
        }

        protected override ISink MakeManagedFromNative(PaSinkInfo obj)
        {
            return new Sink(Context.DirectControl.Sinks, ref obj);
        }

        protected override IntPtr GetAllNative(PaIntrospection.EnumCallback callback)
        {
            return PaIntrospection.pa_context_get_sink_info_list(Context.Ptr, callback, IntPtr.Zero);
        }

        protected override IntPtr GetByIndexNative(uint index, PaIntrospection.EnumCallback callback)
        {
            return PaIntrospection.pa_context_get_sink_info_by_index(Context.Ptr, index, callback, IntPtr.Zero);
        }
    }
}