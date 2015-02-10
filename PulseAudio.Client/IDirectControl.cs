namespace PulseAudio.Client
{
    public interface IDirectControl
    {
        IVolumeController SinkInputs { get; }
        IVolumeController Sinks { get; }
    }
}