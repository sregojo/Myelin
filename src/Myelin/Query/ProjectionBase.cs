using Myelin.Event;
using PheroMonads;
using System;
using System.Collections.Generic;

namespace Myelin.Query
{
    public class ProjectionBase : EventSourcedBase, IProjection
    {
        protected IDictionary<Type, IEventSubscription> EventSubscriptions = new Dictionary<Type, IEventSubscription>();

        public ProjectionBase(Guid id) : base(id) { }

        public ProjectionBase(Guid id, long version) : base(id, version) { }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(IQuery query)
        {
            var handler = this.GetType().GetMethod(nameof(ICommandHandler<ICommand>.Handle), new[] { query.GetType() });
            if (handler == null)
                throw new InvalidOperationException(
                    $"Projection {this.GetType().Name} does not provide a handler for query {query.GetType().Name}");

            return (IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>>)
                handler.Invoke(this, new object[] { query });
        }

        public virtual IEventSubscription<TPersistedEvent> Subscribe<TPersistedEvent>() where TPersistedEvent : IPersistedEvent
        {
            if (!this.EventSubscriptions.ContainsKey(typeof(TPersistedEvent)))
                this.EventSubscriptions.Add(typeof(TPersistedEvent), new EventSubscription<TPersistedEvent>());

            return (EventSubscription<TPersistedEvent>)this.EventSubscriptions[typeof(TPersistedEvent)];
        }

        public override bool ApplyEvent<TEvent>(IPersistedEvent @event)
        {
            if (this.EventSubscriptions.ContainsKey(typeof(TEvent)))
            {
                return this.EventSubscriptions[typeof(TEvent)].Match(@event) ? base.ApplyEvent<TEvent>(@event) : false;
            }

            return false;
        }
    }

    public class EventSubscription<TPersistedEvent> : IEventSubscription<TPersistedEvent> where TPersistedEvent : IPersistedEvent
    {
        private Func<TPersistedEvent, bool> eventFilterFunction = null;

        public bool Match(IPersistedEvent @event)
        {
            if (@event.GetType() == typeof(TPersistedEvent))
            {
                return eventFilterFunction == null ? true : eventFilterFunction((TPersistedEvent)@event);
            }

            return false;
        }

        public void Where(Func<TPersistedEvent, bool> eventFilterFunction) => this.eventFilterFunction = eventFilterFunction;
    }
}
