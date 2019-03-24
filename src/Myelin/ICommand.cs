using System;

namespace Myelin
{
    public interface ICommand<T> : ICommand where T : IAggregateRoot
    {
    }

    public interface ICommand
    {
        Guid AggregateId { get; }
    }
}
