using System;
using System.Threading.Tasks;
using PulseAudio.Client.Internal.Subscription;

namespace PulseAudio.Client.Internal.Introspection
{
    internal class SingleUpdateable<T> : BaseResource, IUpdateable<T>
        where T : class
    {
        private readonly Context _context;
        private readonly EventFacility _facility;
        private readonly uint _index;
        private readonly Func<uint, Task<T>> _getByIndex;
        private Promise<UpdateAction> _updateAvailablePromise;
        private EventSubscription.UnsubscribeAction _unsubscribeAction;

        public T Value { get; private set; }

        public SingleUpdateable(Context context, EventFacility facility, uint index, Func<uint, Task<T>> getByIndex)
        {
            _context = context;
            _facility = facility;
            _index = index;
            _getByIndex = getByIndex;
        }

        public async Task Start()
        {
            await FetchNewValue().ConfigureAwait(false);
            _unsubscribeAction =
                await
                    _context.EventSubscription.SubscribeToIndex(_facility, _index, OnUpdateAvailable)
                        .ConfigureAwait(false);
            Reset();
        }

        public async Task Update()
        {
            var actionToPerform = await _updateAvailablePromise.Task.ConfigureAwait(false);
            if (actionToPerform == UpdateAction.HasNewData)
            {
                await FetchNewValue().ConfigureAwait(false);
            }
            else
            {
                Value = null;
            }
            Reset();
        }

        public Task WaitUntilUpdateAvailable()
        {
            return _updateAvailablePromise.Task;
        }

        private void OnUpdateAvailable(EventType type, uint index)
        {
            _updateAvailablePromise.SetResult(type == EventType.Remove
                ? UpdateAction.WasRemoved
                : UpdateAction.HasNewData);
        }

        private void Reset()
        {
            _updateAvailablePromise = new Promise<UpdateAction>();
        }

        private async Task FetchNewValue()
        {
            Value = await _getByIndex(_index).ConfigureAwait(false);
        }

        protected override void DisposeSelf()
        {
            if (_unsubscribeAction != null)
            {
                _unsubscribeAction();
            }
        }

        private enum UpdateAction
        {
            HasNewData,
            WasRemoved
        }
    }
}