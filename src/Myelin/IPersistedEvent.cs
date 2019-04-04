using System;

namespace Myelin
{
    public interface IPersistedEvent
    {
        Guid CommandId { get; }

        Guid AggregateRootId { get; }
        long Version { get; }

        IEvent Instance { get; }
    }
}
