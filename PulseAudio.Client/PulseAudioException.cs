using System;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client
{
    public class PulseAudioException : Exception
    {
        private readonly string _message;

        public string OperationName { get; private set; }
        public int ErrorCode { get; private set; }
        public string ErrorText { get; private set; }
        public override string Message { get { return _message; } }

        internal PulseAudioException(PulseAudioException inner)
            : base(inner.Message, inner)
        {
            OperationName = inner.OperationName;
            ErrorCode = inner.ErrorCode;
            ErrorText = inner.ErrorText;
            _message = inner.Message;
        }

        internal PulseAudioException(string operationName, int errorCode)
        {
            OperationName = operationName;
            ErrorCode = errorCode;
            ErrorText = PaError.pa_strerror(ErrorCode);
            _message = string.Format("{0} failed: {1}", OperationName, ErrorText);
        }

        internal static PulseAudioException FromContext(string operationName, IntPtr context)
        {
            return new PulseAudioException(operationName, PaError.pa_context_errno(context));
        }
    }
}