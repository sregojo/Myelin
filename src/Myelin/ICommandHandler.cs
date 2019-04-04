using PheroMonads;
using System.Collections.Generic;

namespace Myelin
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(TCommand command);
    }
}
