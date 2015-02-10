using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal
{
    internal static class TaskFactoryExtensions
    {
        public static Task FromPulseAction(this TaskFactory factory, string operationName,
            Func<PaContext.SuccessCallback, IntPtr> operationRunner)
        {
            var promise = new Promise<bool>();
            PaContext.SuccessCallback callback = delegate(IntPtr context, int success, IntPtr userdata)
            {
                if (success != 0)
                {
                    promise.SetResult(true);
                }
                else
                {
                    promise.SetException(PulseAudioException.FromContext(operationName, context));
                }
            };

            PaOperation.pa_operation_unref(operationRunner(callback));
            return promise.Task;
        }

        public static Task<IReadOnlyList<TManaged>> FromPulseEnumeration<TManaged, TNative>(this TaskFactory factory,
            string operationName, Func<TNative, TManaged> converter,
            Func<PaIntrospection.EnumCallback, IntPtr> operationRunner)
        {
            var list = new List<TManaged>();
            var promise = new Promise<IReadOnlyList<TManaged>>();
            PaIntrospection.EnumCallback callback = delegate(IntPtr context, IntPtr ptr, int status, IntPtr userdata)
            {
                if (status == 0)
                {
                    list.Add(converter((TNative) Marshal.PtrToStructure(ptr, typeof (TNative))));
                }
                else
                {
                    if (status > 0)
                    {
                        promise.SetResult(list);
                    }
                    else
                    {
                        promise.SetException(PulseAudioException.FromContext(operationName, context));
                    }
                }
            };

            PaOperation.pa_operation_unref(operationRunner(callback));
            return promise.Task;
        }
    }
}