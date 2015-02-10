namespace PulseAudio.Client
{
    public interface ISinkInput : IVolumeControllable
    {
        int Index { get; }
        int ClientIndex { get; }
        int SinkIndex { get; }
        bool HasVolume { get; }
    }
}