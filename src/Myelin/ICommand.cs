using System;

namespace Myelin
{
    public interface ICommand<T> : ICommand where T : IAggregateRoot
    {
        Guid AggregateId { get; }
    }

    public interface ICommand
    {
        Guid CommandId { get; }
    }
}
