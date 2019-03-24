using System;
using System.Collections.Generic;
using System.Linq;

namespace Myelin.Event
{
    public class EventSourced : IEventSourced
    {
        protected EventSourced(Guid id) : this(id, 0) { }

        protected EventSourced(Guid id, long version)
        {
            this.Id = id;
            this.Version = version;
        }

        public Guid Id { get; protected set; }

        public long Version { get; protected set; }

        public IEventSourced Apply(IEnumerable<IPersistedEvent> events)
        {
            var applyMethod = this.GetType().GetMethod(nameof(this.ApplyEvent));
            foreach (var @event in events.OrderBy(e => e.Version))
            {
                if (!@event.IsApplicableTo(this))
                    throw new Exception(
                        $"Invalid event configuration. Cannot apply event version: {@event.Version} on aggregateRoot version {this.Version}");

                applyMethod
                    .MakeGenericMethod(@event.Instance.GetType())
                    .Invoke(this, new object[] { @event.Instance });

                this.Version++;
            }

            return this;
        }

        public void ApplyEvent<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            var eventHandler = this as IEventHandler<TEvent>;
            if (eventHandler == null)
                throw new InvalidOperationException(
                    $"Aggregate {this.GetType().Name} does not know how to apply event {@event.GetType().Name}");
            eventHandler.Handle(@event);
        }
    }
}
