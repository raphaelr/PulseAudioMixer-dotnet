using System;
using System.Linq;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal
{
    internal class Volumes : IVolumes
    {
        private readonly int _index;
        private readonly IVolumeController _volumeController;
        private readonly Volume[] _volumes;

        internal Volumes(IVolumeController volumeController, int index, bool readOnly, bool software, ref PaCVolume cvolume)
        {
            _volumeController = volumeController;
            _index = index;
            NumChannels = cvolume.NumChannels;
            IsReadOnly = readOnly;
            IsSoftware = software;
            _volumes = cvolume.Volumes.Select(x => new Volume(x)).ToArray();
        }

        public int NumChannels { get; private set; }
        public bool IsReadOnly { get; private set; }
        public bool IsSoftware { get; private set; }

        public Volume this[int channel]
        {
            get { return _volumes[channel]; }
            set
            {
                if (channel >= NumChannels)
                {
                    throw new IndexOutOfRangeException("Channel number too high");
                }
                EnsureVolumesWritable();
                _volumes[channel] = value;
            }
        }

        public void SetAllChannels(Volume volume)
        {
            EnsureVolumesWritable();
            for (var x = 0; x < _volumes.Length; x++)
            {
                _volumes[x] = volume;
            }
        }

        public Volume Average
        {
            get { return (Volume) (uint) _volumes.Take(NumChannels).Average(v => v.Value); }
        }

        public async Task Commit()
        {
            EnsureVolumesWritable();
            await _volumeController
                .SetVolumes(_index, NumChannels, _volumes.Select(x => x.Value).ToArray())
                .ConfigureAwait(false);
        }

        private void EnsureVolumesWritable()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("The volumes of this " + _volumeController.ObjectType +
                                                    " cannot be modified");
            }
        }
    }
}