using Myelin.Event;
using PheroMonads;
using System;
using System.Collections.Generic;

namespace Myelin
{
    public class AggregateRoot : EventSourced, IAggregateRoot
    {
        public AggregateRoot(Guid id) : base(id) { }
        public AggregateRoot(Guid id, long version) : base(id, version) { }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(ICommand command)
        {
            var handler = this.GetType().GetMethod("Handle", new[] { command.GetType() });
            if (handler == null)
                throw new InvalidOperationException(
                    $"Aggregate {this.GetType().Name} does not know how to handle {command.GetType().Name}");

            return (IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>>)
                handler.Invoke(this, new object[] { command });
        }
    }
}
