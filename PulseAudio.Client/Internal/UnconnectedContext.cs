using System;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal
{
    internal class UnconnectedContext : BaseResource
    {
        private readonly ManagedPtr _ptr;
        private readonly MainLoop _mainLoop;
        private bool _connectInProgress;
        private Promise<IContext> _connectPromise;

        public UnconnectedContext(MainLoop mainLoop, string applicationName)
        {
            _mainLoop = mainLoop;
            _ptr = new ManagedPtr("Context",
                PaContext.pa_context_new(mainLoop.Api, applicationName),
                PaContext.pa_context_unref);
            EstablishOwnership(_ptr);
            EstablishOwnership(_mainLoop);

            PaContext.pa_context_set_state_callback(_ptr, OnStateChange, IntPtr.Zero);
        }

        public Task<IContext> Connect(string server)
        {
            if (_connectInProgress)
            {
                throw new InvalidOperationException("Connection is already being established.");
            }

            _connectInProgress = true;
            _connectPromise = new Promise<IContext>();

            PaContext.pa_context_connect(_ptr, server, PaContext.Flags.NoAutoSpawn, IntPtr.Zero);
            return _connectPromise.Task;
        }

        private void OnStateChange(IntPtr context, IntPtr userdata)
        {
            if (!_connectInProgress)
            {
                return;
            }

            var state = PaContext.pa_context_get_state(context);
            if (state == PaContext.State.Ready)
            {
                _connectInProgress = false;
                _connectPromise.SetResult(CreateConnectedContext());
            }
            else if (state == PaContext.State.Failed)
            {
                _connectInProgress = false;
                _connectPromise.SetException(PulseAudioException.FromContext("Connect", context));
            }
        }

        private Context CreateConnectedContext()
        {
            var ctx = new Context(_mainLoop, _ptr);
            TransferOwnership(_ptr, ctx);
            TransferOwnership(_mainLoop, ctx);
            return ctx;
        }
    }
}