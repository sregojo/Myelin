using System;
using System.Collections.Generic;

namespace Myelin
{
    public interface IEventStorage
    {
        T Load<T>(Guid eventSourcedId)
            where T : IEventSourced;

        IEnumerable<IPersistedEvent> Store<T>(ICommand<T> command, IEnumerable<IEvent> events)
            where T : IAggregateRoot;
    }
}
