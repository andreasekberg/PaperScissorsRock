using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock.Domain.Tests.Unit
{
	[Subject(typeof (EventStore))]
	public class when_append_to_new_stream
	{
		static EventStore eventStore;
		static IEnumerable<IEvent> events;
		static Guid streamId = new Guid("CF9D8E67-F639-4392-90D8-BED180918F3B");

		Establish context = () =>
			{
				eventStore = new EventStore();
				events = new List<IEvent> {new GameCreated(streamId, 1, "niklas", "andreas", "pizza")};
			};

		Because of = () => eventStore.AppendToStream(streamId, 0, events);

		It should_return_the_stream_with_the_same_number_of_events =
			() => eventStore.LoadEventStream(streamId).Count().ShouldEqual(events.Count());


		It should_return_version_1 = () => eventStore.LoadEventStream(streamId).Version().ShouldEqual(1);
	}
}