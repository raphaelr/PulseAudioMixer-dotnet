using System;

namespace PulseAudio.Client
{
    public interface IContext : IDisposable
    {
        IDirectControl DirectControl { get; }
        IIntrospection<ISinkInput> SinkInputs { get; }
        IIntrospection<ISink> Sinks { get; }
    }
}
