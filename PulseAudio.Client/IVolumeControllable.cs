using System.Threading.Tasks;

namespace PulseAudio.Client
{
    public interface IVolumeControllable
    {
        string Name { get; }
        bool Muted { get; }
        IVolumes Volumes { get; }
        Task SetMuted(bool mute);
    }
}