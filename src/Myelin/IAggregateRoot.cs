﻿using PheroMonads;
using System.Collections.Generic;

namespace Myelin
{
    public interface IAggregateRoot : IEventSourced
    {
        IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(ICommand command);
    }
}
