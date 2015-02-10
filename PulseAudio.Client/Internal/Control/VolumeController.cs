using System;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Control
{
    internal abstract class VolumeController : IVolumeController
    {
        protected Context Context { get; private set; }
        public abstract string ObjectType { get; }

        protected VolumeController(Context context)
        {
            Context = context;
        }

        protected abstract IntPtr MuteNative(int index, int mute, PaContext.SuccessCallback cb);
        protected abstract IntPtr SetVolumesNative(int index, ref PaCVolume volumes, PaContext.SuccessCallback cb);

        public Task Mute(int index, bool mute)
        {
            using (Context.MainLoop.Lock())
            {
                return Task.Factory.FromPulseAction("Mute", cb => MuteNative(index, mute ? 1 : 0, cb));
            }
        }

        public Task SetVolumes(int index, int numChannels, uint[] volumes)
        {
            var volume = MakeCvolume(numChannels, volumes);
            using (Context.MainLoop.Lock())
            {
                return Task.Factory.FromPulseAction("SetVolumes", cb => SetVolumesNative(index, ref volume, cb));
            }
        }

        private static PaCVolume MakeCvolume(int numChannels, uint[] volumes)
        {
            return new PaCVolume {NumChannels = (byte) numChannels, Volumes = volumes};
        }
    }
}