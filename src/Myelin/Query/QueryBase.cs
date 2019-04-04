using System;

namespace Myelin.Query
{
    public abstract class QueryBase<TProjection> : IQuery<TProjection>
        where TProjection : IProjection
    {
        public abstract Guid ProjectionId { get; }
        public abstract Guid QueryId { get; }
    }
}
