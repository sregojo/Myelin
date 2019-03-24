using System;
using System.Collections.Generic;

namespace Myelin
{
    public interface IEventSourced
    {
        Guid Id { get; }

        long Version { get; }

        IEventSourced Apply(IEnumerable<IPersistedEvent> events);
    }
}
