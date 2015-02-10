using System.Threading.Tasks;

namespace PulseAudio.Client
{
    public interface IVolumes
    {
        bool IsReadOnly { get; }
        bool IsSoftware { get; }
        int NumChannels { get; }
        Volume Average { get; }
        Volume this[int channel] { get; set; }
        void SetAllChannels(Volume volume);
        Task Commit();
    }
}