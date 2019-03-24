using PheroMonads;
using System.Collections.Generic;

namespace Myelin
{
    public interface ICommandProcessor
    {
        IEither<IEnumerable<ICommandError>, TAggregate> Process<TAggregate>(ICommand<TAggregate> command)
            where TAggregate : IAggregateRoot;
    }
}
