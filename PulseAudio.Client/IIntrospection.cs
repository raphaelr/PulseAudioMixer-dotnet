using System.Collections.Generic;
using System.Threading.Tasks;

namespace PulseAudio.Client
{
    public interface IIntrospection<TInfo>
    {
        string ObjectType { get; }
        Task<IReadOnlyList<TInfo>> GetAll();
        Task<TInfo> GetByIndex(uint index);
        Task<IUpdateable<TInfo>> GetByIndexUpdateable(uint index);
    }
}