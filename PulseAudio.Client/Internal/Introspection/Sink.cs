using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Introspection
{
    internal class Sink : ISink
    {
        private readonly IVolumeController _volumeController;

        public int Index { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Volume BaseVolume { get; private set; }
        public SinkState State { get; private set; }

        public bool Muted { get; private set; }
        public IVolumes Volumes { get; private set; }

        public Sink(IVolumeController volumeController, ref PaSinkInfo paSinkInfo)
        {
            _volumeController = volumeController;
            Index = paSinkInfo.Index;
            Name = PaCommon.GetString(paSinkInfo.Name);
            Description = PaCommon.GetString(paSinkInfo.Description);
            BaseVolume = (Volume) paSinkInfo.BaseVolume;
            State = (SinkState) paSinkInfo.State;
            Muted = paSinkInfo.Mute != 0;
            Volumes = new Volumes(_volumeController, Index, false, false, ref paSinkInfo.Volume);
        }

        public async Task SetMuted(bool mute)
        {
            await _volumeController.Mute(Index, mute).ConfigureAwait(false);
            Muted = mute;
        }
    }
}