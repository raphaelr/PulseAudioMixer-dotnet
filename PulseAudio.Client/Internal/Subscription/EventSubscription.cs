using System;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Subscription
{
    internal class EventSubscription
    {
        public delegate void NotificationHandler(EventType type, uint index);

        public delegate Task UnsubscribeAction();

        private readonly SubscriptionManager _subscriptionManager;
        private readonly EventDictionary<EventFacility, NotificationHandler> _eventDictionary;

        public EventSubscription(Context context)
        {
            _subscriptionManager = new SubscriptionManager(context);
            _eventDictionary = new EventDictionary<EventFacility, NotificationHandler>(dlg => (NotificationHandler) dlg);
            PaSubscription.pa_context_set_subscribe_callback(context.Ptr, OnEventReceived, IntPtr.Zero);
        }

        public async Task<UnsubscribeAction> Subscribe(EventFacility facility, NotificationHandler handler)
        {
            await _subscriptionManager.AddReference(facility).ConfigureAwait(false);
            _eventDictionary.Subscribe(facility, handler);

            var unsubscribed = false;
            return delegate
            {
                if (unsubscribed)
                {
                    throw new InvalidOperationException("This event handler has already been unsubscribed");
                }
                unsubscribed = true;
                _eventDictionary.Unsubscribe(facility, handler);
                return _subscriptionManager.RemoveReference(facility);
            };
        }

        public Task<UnsubscribeAction> SubscribeToIndex(EventFacility facility, uint index, NotificationHandler handler)
        {
            NotificationHandler filteredHandler = delegate(EventType type, uint i)
            {
                if (i == index)
                {
                    handler(type, index);
                }
            };
            return Subscribe(facility, filteredHandler);
        }

        private void OnEventReceived(IntPtr context, uint @event, uint index, IntPtr userdata)
        {
            var facility = ((EventFacility) @event) & EventFacility.Mask;
            var type = ((EventType) @event) & EventType.Mask;
            _eventDictionary.Invoke(facility, handler => handler(type, index));
        }
    }
}