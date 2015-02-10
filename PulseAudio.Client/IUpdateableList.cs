using System.Collections.Generic;

namespace PulseAudio.Client
{
    public interface IUpdateableList<out T> : IUpdateable<IReadOnlyList<IUpdateable<T>>>
    {
    }
}