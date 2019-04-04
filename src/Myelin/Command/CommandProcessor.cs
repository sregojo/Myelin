using PheroMonads;
using System;
using System.Collections.Generic;

namespace Myelin.Command
{
    public class CommandProcessor : ICommandProcessor
    {
        protected readonly IEventStorage eventStorage;

        public CommandProcessor(IEventStorage aggreagateStorage)
        {
            if (aggreagateStorage == null)
                throw new ArgumentException("Value cannot be null", nameof(aggreagateStorage));

            this.eventStorage = aggreagateStorage;
        }

        public IEither<IEnumerable<ICommandError>, TAggregate> Process<TAggregate>(ICommand<TAggregate> command)
            where TAggregate : IAggregateRoot
        {
            var aggregateRoot = this.eventStorage.Load<TAggregate>(command.AggregateId);

            return aggregateRoot
                .Handle(command)
                .Right(events =>
                    (TAggregate)aggregateRoot.Apply(
                        this.eventStorage.Store(command, events)));
        }
    }
}
