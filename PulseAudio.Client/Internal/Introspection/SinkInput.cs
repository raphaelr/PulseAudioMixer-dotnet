using System;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Introspection
{
    internal class SinkInput : ISinkInput
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public int ClientIndex { get; private set; }
        public int SinkIndex { get; private set; }

        public bool HasVolume { get; private set; }
        public bool Muted { get; private set; }

        private readonly IVolumes _volumes;
        private readonly IVolumeController _volumeController;

        public SinkInput(IVolumeController volumeController, ref PaSinkInputInfo paSinkInputInfo)
        {
            _volumeController = volumeController;
            Index = paSinkInputInfo.Index;
            Name = PaCommon.GetString(paSinkInputInfo.Name);
            ClientIndex = paSinkInputInfo.Client;
            SinkIndex = paSinkInputInfo.Sink;
            HasVolume = paSinkInputInfo.HasVolume != 0;
            Muted = paSinkInputInfo.Mute != 0;

            if (HasVolume)
            {
                _volumes = new Volumes(_volumeController, Index, paSinkInputInfo.VolumeWritable == 0, true,
                    ref paSinkInputInfo.Volume);
            }
        }


        public async Task SetMuted(bool mute)
        {
            await _volumeController.Mute(Index, mute).ConfigureAwait(false);
            Muted = mute;
        }

        public IVolumes Volumes
        {
            get
            {
                EnsureHasVolumes();
                return _volumes;
            }
        }

        private void EnsureHasVolumes()
        {
            if (!HasVolume)
            {
                throw new InvalidOperationException("This sink input does not have volumes");
            }
        }
    }
}
