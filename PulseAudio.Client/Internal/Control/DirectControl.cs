
namespace PulseAudio.Client.Internal.Control
{
    internal class DirectControl : IDirectControl
    {
        public IVolumeController SinkInputs { get; private set; }
        public IVolumeController Sinks { get; private set; }

        public DirectControl(Context context)
        {
            SinkInputs = new SinkInputVolumeController(context);
            Sinks = new SinkVolumeController(context);
        }
    }
}