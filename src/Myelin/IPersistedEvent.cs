using System;

namespace Myelin
{
    public interface IPersistedEvent
    {
        Guid AggregateRootId { get; }
        long Version { get; }
        IEvent Instance { get; }
    }
}
