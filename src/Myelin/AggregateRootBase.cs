using Myelin.Event;
using PheroMonads;
using System;
using System.Collections.Generic;

namespace Myelin
{
    public class AggregateRootBase : EventSourcedBase, IAggregateRoot
    {
        public AggregateRootBase(Guid id) : base(id) { }

        public AggregateRootBase(Guid id, long version) : base(id, version) { }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(ICommand command)
        {
            var handler = this.GetType().GetMethod(nameof(ICommandHandler<ICommand>.Handle), new[] { command.GetType() });
            if (handler == null)
                throw new InvalidOperationException(
                    $"Aggregate {this.GetType().Name} does not provide a handler for command {command.GetType().Name}");

            return (IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>>)
                handler.Invoke(this, new object[] { command });
        }

        public override bool ApplyEvent<TEvent>(IPersistedEvent @event)
        {
            if (this.Id != @event.AggregateRootId)
                throw new Exception(
                    $"Invalid event configuration. Cannot apply event with Aggregate Root id: {@event.AggregateRootId} on Aggregate Root id: {this.Id}");

            if (this.Version != @event.Version - 1)
                throw new Exception(
                    $"Invalid event configuration. Cannot apply event version: {@event.Version} on Aggregate Root version {this.Version}");

            if (!base.ApplyEvent<TEvent>(@event))
                throw new Exception(
                    $"Aggregate {this.GetType().Name} does not provide a handler for event {@event.GetType().Name}");

            return true;
        }
    }
}
