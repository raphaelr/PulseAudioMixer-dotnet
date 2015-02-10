using System;
using System.Threading.Tasks;

namespace PulseAudio.Client
{
    public interface IUpdateable<out T> : IDisposable
    {
        T Value { get; }

        Task Update();
        Task WaitUntilUpdateAvailable();
    }
}