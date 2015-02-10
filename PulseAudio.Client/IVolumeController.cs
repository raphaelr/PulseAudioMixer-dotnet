using System.Threading.Tasks;

namespace PulseAudio.Client
{
    public interface IVolumeController
    {
        string ObjectType { get; }
        Task Mute(int index, bool mute);
        Task SetVolumes(int index, int numChannels, uint[] volumes);
    }
}