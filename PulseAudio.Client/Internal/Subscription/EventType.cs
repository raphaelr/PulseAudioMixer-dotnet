namespace PulseAudio.Client.Internal.Subscription
{
    internal enum EventType : uint
    {
        New = 0x0000U,
        Change = 0x0010U,
        Remove = 0x0020U,
        Mask = 0x0030U
    }
}