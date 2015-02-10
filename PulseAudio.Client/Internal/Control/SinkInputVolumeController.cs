using System;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Control
{
    internal class SinkInputVolumeController : VolumeController
    {
        public SinkInputVolumeController(Context context) : base(context)
        {
        }

        public override string ObjectType
        {
            get { return "SinkInput"; }
        }

        protected override IntPtr MuteNative(int index, int mute, PaContext.SuccessCallback cb)
        {
            return PaControl.pa_context_set_sink_input_mute(Context.Ptr, index, mute, cb, IntPtr.Zero);
        }

        protected override IntPtr SetVolumesNative(int index, ref PaCVolume volumes, PaContext.SuccessCallback cb)
        {
            return PaControl.pa_context_set_sink_input_volume(Context.Ptr, index, ref volumes, cb, IntPtr.Zero);
        }
    }
}