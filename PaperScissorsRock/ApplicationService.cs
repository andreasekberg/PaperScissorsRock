using PaperScissorsRock.Contracts;

namespace PaperScissorsRock
{
	public class ApplicationService : IApplicationService
	{
		readonly IEventStore _eventStore;

		public ApplicationService(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public void Handle(ICommand command)
		{
			var stream = _eventStore.LoadEventStream(command.AggregateId);

			var aggregate = (dynamic) new Game(command.AggregateId);

			foreach (var @event in stream)
			{
				aggregate.Handle((dynamic)@event);
			}

			var events = aggregate.Handle((dynamic) command);

			_eventStore.AppendToStream(command.AggregateId, stream.Version(), events);
		}
	}
}