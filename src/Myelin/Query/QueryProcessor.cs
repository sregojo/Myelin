using PheroMonads;
using System;
using System.Collections.Generic;

namespace Myelin.Query
{
    public class QueryProcessor : IQueryProcessor
    {
        protected readonly IEventStorage eventStorage;

        public QueryProcessor(IEventStorage aggreagateStorage)
        {
            if (aggreagateStorage == null)
                throw new ArgumentException("Value cannot be null", nameof(aggreagateStorage));

            this.eventStorage = aggreagateStorage;
        }

        public IEither<IEnumerable<ICommandError>, TProjection> Process<TProjection>(IQuery<TProjection> query)
            where TProjection : IProjection
        {
            var projection = this.eventStorage.Load<TProjection>(query.QueryId);

            return projection.Handle(query).Right(() => projection); ;
        }
    }
}
