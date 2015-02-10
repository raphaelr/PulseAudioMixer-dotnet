using System;
using System.Collections.Generic;

namespace PulseAudio.Client.Internal
{
    internal class BaseResource : IDisposable
    {
        private readonly List<IDisposable> _managedObjects = new List<IDisposable>();

        protected bool Disposed { get; private set; }

        protected virtual void DisposeSelf()
        {
        }

        internal void EstablishOwnership(IDisposable resource)
        {
            _managedObjects.Add(resource);
        }

        protected void TransferOwnership(IDisposable resource, BaseResource target)
        {
            var index = _managedObjects.IndexOf(resource);
            if (index == -1)
            {
                throw new InvalidOperationException(
                    "Cannot transfer ownership of the specified resource because this BaseResource does not have ownership");
            }
            
            _managedObjects.RemoveAt(index);
            target.EstablishOwnership(resource);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(Disposed) return;

            if (disposing)
            {
                _managedObjects.ForEach(x => x.Dispose());
            }
            
            DisposeSelf();
            Disposed = true;
        }

        ~BaseResource()
        {
            Dispose(false);
        }
    }
}