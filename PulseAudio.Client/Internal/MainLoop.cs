using System;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal
{
    internal class MainLoop : BaseResource
    {
        private readonly ManagedPtr _ptr;

        public MainLoop()
        {
            _ptr = new ManagedPtr("MainLoop", PaThreadedMainLoop.pa_threaded_mainloop_new(), DisposeMainLoop);
            EstablishOwnership(_ptr);
            Start();
        }

        public IntPtr Api
        {
            get { return PaThreadedMainLoop.pa_threaded_mainloop_get_api(_ptr); }
        }

        public SafeLock Lock()
        {
            return new SafeLock(this);
        }

        private void LockManually()
        {
            PaThreadedMainLoop.pa_threaded_mainloop_lock(_ptr);
        }

        private void UnlockManually()
        {
            PaThreadedMainLoop.pa_threaded_mainloop_unlock(_ptr);
        }

        public void Quit()
        {
            PaThreadedMainLoop.pa_threaded_mainloop_stop(_ptr);
        }

        private void Start()
        {
            PaThreadedMainLoop.pa_threaded_mainloop_start(_ptr);
        }

        private void DisposeMainLoop(IntPtr obj)
        {
            Quit();
            PaThreadedMainLoop.pa_threaded_mainloop_free(obj);
        }

        public struct SafeLock : IDisposable
        {
            private readonly MainLoop _mainLoop;

            public SafeLock(MainLoop mainLoop)
            {
                _mainLoop = mainLoop;
                _mainLoop.LockManually();
            }

            public void Dispose()
            {
                _mainLoop.UnlockManually();
            }
        }
    }
}