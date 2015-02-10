using System;
using System.Threading.Tasks;
using CTask = System.Threading.Tasks.Task;

namespace PulseAudio.Client.Internal
{
    internal class Promise<T>
    {
        private readonly TaskCompletionSource<T> _taskCompletionSource;

        public Promise()
        {
            _taskCompletionSource = new TaskCompletionSource<T>();
        }

        public void SetException(Exception exception)
        {
            CTask.Run(() => _taskCompletionSource.SetException(exception));
        }

        public void SetResult(T result)
        {
            CTask.Run(() => _taskCompletionSource.SetResult(result));
        }

        public Task<T> Task
        {
            get { return _taskCompletionSource.Task; }
        }
    }
}