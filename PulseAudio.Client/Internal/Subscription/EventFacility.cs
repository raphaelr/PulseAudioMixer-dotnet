namespace PulseAudio.Client.Internal.Subscription
{
    internal enum EventFacility : uint
    {
        Sink = 0x0000U,
        Source = 0x0001U,
        SinkInput = 0x0002U,
        SourceOutput = 0x0003U,
        Module = 0x0004U,
        Client = 0x0005U,
        SampleCache = 0x0006U,
        Server = 0x0007U,
        Autoload = 0x0008U,
        Card = 0x0009U,
        Mask = 0x000FU,
    }
}