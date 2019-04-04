using System;
using System.Collections.Generic;
using System.Linq;

namespace Myelin.Event
{
    public class EventSourcedBase : IEventSourced
    {
        protected EventSourcedBase(Guid id) : this(id, 0) { }

        protected EventSourcedBase(Guid id, long version)
        {
            this.Id = id;
            this.Version = version;
        }

        public Guid Id { get; protected set; }

        public long Version { get; private set; }

        public IEventSourced Apply(IEnumerable<IPersistedEvent> events)
        {
            var applyMethod = this.GetType().GetMethod(nameof(this.ApplyEvent));
            foreach (var @event in events.OrderBy(e => e.Version))
            {
                applyMethod
                    .MakeGenericMethod(@event.Instance.GetType())
                    .Invoke(this, new object[] { @event.Instance });
            }

            return this;
        }

        public virtual bool ApplyEvent<TEvent>(IPersistedEvent @event)
            where TEvent : IEvent
        {
            if (this is IEventHandler<TEvent> eventHandler)
            {
                eventHandler.Handle((TEvent)@event.Instance);

                this.Version++;

                return true;
            }

            return false;
        }
    }
}
