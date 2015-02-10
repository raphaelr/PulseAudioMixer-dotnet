using System;

namespace PulseAudio.Client.Internal
{
    internal class ManagedPtr : BaseResource
    {
        private readonly string _objectName;
        private readonly IntPtr _ptr;
        private readonly Action<IntPtr> _free;

        public ManagedPtr(string objectName, IntPtr ptr, Action<IntPtr> free)
        {
            _objectName = objectName;
            _ptr = ptr;
            _free = free;
        }

        public IntPtr Ptr
        {
            get
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(_objectName);
                }
                return _ptr;
            }
        }

        protected override void DisposeSelf()
        {
            _free(_ptr);
        }

        public static implicit operator IntPtr(ManagedPtr ptr)
        {
            return ptr.Ptr;
        }
    }
}