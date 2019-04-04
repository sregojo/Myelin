using System;

namespace Myelin
{
    public interface IQuery<T> : IQuery where T : IProjection
    {
        Guid ProjectionId { get; }
    }

    public interface IQuery
    {
        Guid QueryId { get; }
    }
}
