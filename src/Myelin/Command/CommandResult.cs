using Myelin.Event;
using PheroMonads;
using System.Collections.Generic;

namespace Myelin.Command
{
    public class CommandResult
    {
        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handled(IEvent @event) 
            => CommandResult.Handled(new[] { @event });

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handled(IEnumerable<IEvent> events)
            => Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(events);

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Failed(ICommandError error)
            => CommandResult.Failed(new[] { error });

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Failed(IEnumerable<ICommandError> errors)
            => Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(errors);

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Failed(string message)
            => Failed(new MessageError(message));

        private class MessageError : ICommandError
        {
            public MessageError(string message)
            {
                this.Message = message;
            }

            public string Message { get; }
        }
    }
}
