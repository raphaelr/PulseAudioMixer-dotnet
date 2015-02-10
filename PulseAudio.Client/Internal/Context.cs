using PulseAudio.Client.Internal.Control;
using PulseAudio.Client.Internal.Introspection;
using PulseAudio.Client.Internal.Subscription;

namespace PulseAudio.Client.Internal
{
    internal class Context : BaseResource, IContext
    {
        internal MainLoop MainLoop { get; private set; }
        internal ManagedPtr Ptr { get; private set; }
        internal EventSubscription EventSubscription { get; private set; }

        public IDirectControl DirectControl { get; private set; }
        public IIntrospection<ISinkInput> SinkInputs { get; private set; }
        public IIntrospection<ISink> Sinks { get; private set; }

        public Context(MainLoop mainLoop, ManagedPtr ptr)
        {
            MainLoop = mainLoop;
            Ptr = ptr;
            EventSubscription = new EventSubscription(this);
            DirectControl = new DirectControl(this);
            SinkInputs = new SinkInputIntrospection(this);
            Sinks = new SinkIntrospection(this);
        }
    }
}