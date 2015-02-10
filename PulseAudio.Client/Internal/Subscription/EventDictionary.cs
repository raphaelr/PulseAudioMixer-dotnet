using System;
using System.Collections.Generic;

namespace PulseAudio.Client.Internal.Subscription
{
    internal class EventDictionary<TKey, TEvent>
    {
        private readonly Func<Delegate, TEvent> _downcaster;
        private readonly Dictionary<TKey, Delegate> _dict;

        public EventDictionary(Func<Delegate, TEvent> downcaster)
        {
            _downcaster = downcaster;
            _dict = new Dictionary<TKey, Delegate>();
        } 

        public void Subscribe(TKey key, Delegate handler)
        {
            if (_dict.ContainsKey(key))
            {
                _dict[key] = Delegate.Combine(_dict[key], handler);
            }
            else
            {
                _dict[key] = handler;
            }
        }

        public void Unsubscribe(TKey key, Delegate handler)
        {
            _dict[key] = Delegate.Remove(_dict[key], handler);
        }

        public void Invoke(TKey key, Action<TEvent> invoker)
        {
            Delegate dlg;
            _dict.TryGetValue(key, out dlg);
            if (dlg != null)
            {
                invoker(_downcaster(dlg));
            }
        }
    }
}