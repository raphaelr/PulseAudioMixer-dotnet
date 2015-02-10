namespace PulseAudio.Client
{
    public interface ISink : IVolumeControllable
    {
        int Index { get; }
        string Description { get; }
        Volume BaseVolume { get; }
        SinkState State { get; }
    }
}