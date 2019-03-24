namespace Myelin.Event
{
    public static class EventExtensions
    {
        public static bool IsApplicableTo(this IPersistedEvent @event, EventSourced eventSourced)
        {
            return
                eventSourced.Id == @event.AggregateRootId
                &&
                eventSourced.Version == @event.Version - 1;
        }
    }
}
