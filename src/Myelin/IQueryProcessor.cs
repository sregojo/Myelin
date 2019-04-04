using PheroMonads;
using System.Collections.Generic;

namespace Myelin
{
    public interface IQueryProcessor
    {
        IEither<IEnumerable<ICommandError>, TProjection> Process<TProjection>(IQuery<TProjection> query)
            where TProjection : IProjection;
    }
}
