using System;

namespace Myelin.Command
{
    public abstract class CommandBase<TAggregate> : ICommand<TAggregate>
        where TAggregate : IAggregateRoot
    {
        public abstract Guid AggregateId { get; }
        public abstract Guid CommandId { get; }
    }
}
