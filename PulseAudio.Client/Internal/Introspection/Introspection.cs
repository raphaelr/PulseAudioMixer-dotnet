using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PulseAudio.Client.Ffi;
using PulseAudio.Client.Internal.Subscription;

namespace PulseAudio.Client.Internal.Introspection
{
    internal abstract class Introspection<TManaged, TNative> : IIntrospection<TManaged>
        where TManaged : class
    {
        protected Context Context { get; private set; }

        protected Introspection(Context context)
        {
            Context = context;
        }

        public abstract string ObjectType { get; }
        protected abstract EventFacility EventFacility { get; }

        protected abstract TManaged MakeManagedFromNative(TNative obj);
        protected abstract IntPtr GetAllNative(PaIntrospection.EnumCallback callback);
        protected abstract IntPtr GetByIndexNative(uint index, PaIntrospection.EnumCallback callback);

        public Task<IReadOnlyList<TManaged>> GetAll()
        {
            using (Context.MainLoop.Lock())
            {
                return Task.Factory.FromPulseEnumeration<TManaged, TNative>("GetAll" + ObjectType, MakeManagedFromNative,
                    GetAllNative);
            }
        }

        public async Task<TManaged> GetByIndex(uint index)
        {
            Task<IReadOnlyList<TManaged>> task;
            using (Context.MainLoop.Lock())
            {
                task = Task.Factory.FromPulseEnumeration<TManaged, TNative>("GetByIndex" + ObjectType,
                    MakeManagedFromNative, cb => GetByIndexNative(index, cb));
            }
            
            var items = await task.ConfigureAwait(false);
            return items.FirstOrDefault();
        }

        public async Task<IUpdateable<TManaged>> GetByIndexUpdateable(uint index)
        {
            var updateable = new SingleUpdateable<TManaged>(Context, EventFacility, index, GetByIndex);
            await updateable.Start().ConfigureAwait(false);
            return updateable;
        }
    }
}