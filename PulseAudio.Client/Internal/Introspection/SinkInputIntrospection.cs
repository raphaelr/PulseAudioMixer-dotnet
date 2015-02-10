using System;
using PulseAudio.Client.Ffi;
using PulseAudio.Client.Internal.Subscription;

namespace PulseAudio.Client.Internal.Introspection
{
    internal class SinkInputIntrospection : Introspection<ISinkInput, PaSinkInputInfo>
    {
        public SinkInputIntrospection(Context context) : base(context)
        {
        }

        public override string ObjectType
        {
            get { return "SinkInput"; }
        }

        protected override EventFacility EventFacility
        {
            get { return EventFacility.SinkInput; }
        }

        protected override ISinkInput MakeManagedFromNative(PaSinkInputInfo obj)
        {
            return new SinkInput(Context.DirectControl.SinkInputs, ref obj);
        }

        protected override IntPtr GetAllNative(PaIntrospection.EnumCallback callback)
        {
            return PaIntrospection.pa_context_get_sink_input_info_list(Context.Ptr, callback, IntPtr.Zero);
        }

        protected override IntPtr GetByIndexNative(uint index, PaIntrospection.EnumCallback callback)
        {
            return PaIntrospection.pa_context_get_sink_input_info_by_index(Context.Ptr, index, callback, IntPtr.Zero);
        }
    }
}