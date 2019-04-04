using PheroMonads;
using System;
using System.Collections.Generic;

namespace Myelin
{
    public interface IProjection : IEventSourced
    {
        IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(IQuery query);

        IEventSubscription<TPersistedEvent> Subscribe<TPersistedEvent>() where TPersistedEvent : IPersistedEvent;
    }

    public interface IEventSubscription<TPersistedEvent> : IEventSubscription where TPersistedEvent : IPersistedEvent
    {
        void Where(Func<TPersistedEvent, bool> eventFilterFunction);
    }

    public interface IEventSubscription
    {
        bool Match(IPersistedEvent @event);
    }
}
