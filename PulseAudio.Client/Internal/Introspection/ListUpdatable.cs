using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PulseAudio.Client.Internal.Subscription;

namespace PulseAudio.Client.Internal.Introspection
{
    internal class ListUpdatable<T> : BaseResource, IUpdateableList<T>
    {
        private readonly Context _context;
        private readonly EventFacility _facility;
        private readonly Func<Task<IReadOnlyList<T>>> _getAll;
        private readonly List<ItemUpdatable<T>> _list;
        private EventSubscription.UnsubscribeAction _unsubscribeAction;

        public IReadOnlyList<IUpdateable<T>> Value
        {
            get { return _list; }
        }

        public ListUpdatable(Context context, EventFacility facility, Func<Task<IReadOnlyList<T>>> getAll)
        {
            _context = context;
            _facility = facility;
            _getAll = getAll;
            _list = new List<ItemUpdatable<T>>();
        }

        public async Task Start()
        {
            await FetchInitialList().ConfigureAwait(false);
            _unsubscribeAction =
                await _context.EventSubscription.Subscribe(_facility, OnUpdateAvailable).ConfigureAwait(false);
            Reset();
        }

        public Task Update()
        {
            throw new System.NotImplementedException();
        }

        public Task WaitUntilUpdateAvailable()
        {
            throw new System.NotImplementedException();
        }

        private void OnUpdateAvailable(EventType type, uint index)
        {
        }

        private async Task FetchInitialList()
        {
            var allItems = await _getAll().ConfigureAwait(true);
            _list.AddRange(allItems.Select(CreateItemUpdatable));
        }

        protected override void DisposeSelf()
        {
            if (_unsubscribeAction != null)
            {
                _unsubscribeAction();
            }
        }

        private class ItemUpdatable<T> : BaseResource, IUpdateable<T>
        {
            public T Value { get; private set; }
            public Task Update()
            {
                throw new System.NotImplementedException();
            }

            public Task WaitUntilUpdateAvailable()
            {
                throw new System.NotImplementedException();
            }

            protected override void DisposeSelf()
            {
            }
        }
    }
}