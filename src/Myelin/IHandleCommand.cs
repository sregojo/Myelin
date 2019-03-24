using PheroMonads;
using System.Collections.Generic;

namespace Myelin
{
    public interface IHandleCommand<TModel, ICommand>
    {
        IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(ICommand command);
    }
}
