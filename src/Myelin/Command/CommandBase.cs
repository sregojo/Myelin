using System;

namespace Myelin.Command
{
    public abstract class CommandBase<TAggregate> : ICommand<TAggregate>
        where TAggregate : IAggregateRoot
    {
        public Guid AggregateId
        {
            get
            {
                try
                {
                    return this.GetAggregateId;
                }
                catch (Exception ex)
                {
                    throw new InvalidCommandException("Cannot read aggregate Id from command", ex);
                }
            }
        }

        protected abstract Guid GetAggregateId { get; }
    }

    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message, Exception ex) : base(message, ex) { }
    }
}
