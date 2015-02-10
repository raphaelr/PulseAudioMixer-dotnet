using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client.Internal.Subscription
{
    internal class SubscriptionManager
    {
        private readonly Context _context;
        private readonly Dictionary<EventFacility, int> _referenceCounts;

        public SubscriptionManager(Context context)
        {
            _context = context;
            _referenceCounts = new Dictionary<EventFacility, int>();
        }

        public Task AddReference(EventFacility facility)
        {
            if (_referenceCounts.ContainsKey(facility))
            {
                _referenceCounts[facility]++;
                return Task.FromResult(true);
            }
            _referenceCounts[facility] = 1;
            return UpdateSubscriptionMask();
        }

        public Task RemoveReference(EventFacility facilty)
        {
            _referenceCounts[facilty]--;
            if(_referenceCounts[facilty] <= 0)
            {
                _referenceCounts.Remove(facilty);
                return UpdateSubscriptionMask();
            }
            return Task.FromResult(true);
        }

        private Task UpdateSubscriptionMask()
        {
            var mask = _referenceCounts.Keys.Aggregate(PaSubscription.SubscriptionMask.Null,
                (memo, facility) => memo | FacilityToSubscriptionMask(facility));
            return Task.Factory.FromPulseAction("Subscribe",
                cb => PaSubscription.pa_context_subscribe(_context.Ptr, mask, cb, IntPtr.Zero));
        }

        private PaSubscription.SubscriptionMask FacilityToSubscriptionMask(EventFacility facility)
        {
            switch (facility)
            {
                case EventFacility.Sink:
                    return PaSubscription.SubscriptionMask.Sink;
                case EventFacility.Source:
                    return PaSubscription.SubscriptionMask.Source;
                case EventFacility.SinkInput:
                    return PaSubscription.SubscriptionMask.SinkInput;
                case EventFacility.SourceOutput:
                    return PaSubscription.SubscriptionMask.SourceOutput;
                case EventFacility.Module:
                    return PaSubscription.SubscriptionMask.Module;
                case EventFacility.Client:
                    return PaSubscription.SubscriptionMask.Client;
                case EventFacility.SampleCache:
                    return PaSubscription.SubscriptionMask.SampleCache;
                case EventFacility.Server:
                    return PaSubscription.SubscriptionMask.Server;
                case EventFacility.Autoload:
                    return PaSubscription.SubscriptionMask.Autoload;
                case EventFacility.Card:
                    return PaSubscription.SubscriptionMask.Card;
                default:
                    return PaSubscription.SubscriptionMask.Null;
            }
        }
    }
}