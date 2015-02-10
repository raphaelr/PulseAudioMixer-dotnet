using System;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Control
{
    internal class SinkVolumeController : VolumeController
    {
        public SinkVolumeController(Context context) : base(context)
        {
        }

        public override string ObjectType
        {
            get { return "Sink"; }
        }

        protected override IntPtr MuteNative(int index, int mute, PaContext.SuccessCallback cb)
        {
            return PaControl.pa_context_set_sink_mute_by_index(Context.Ptr, index, mute, cb, IntPtr.Zero);
        }

        protected override IntPtr SetVolumesNative(int index, ref PaCVolume volumes, PaContext.SuccessCallback cb)
        {
            return PaControl.pa_context_set_sink_volume_by_index(Context.Ptr, index, ref volumes, cb, IntPtr.Zero);
        }
    }
}