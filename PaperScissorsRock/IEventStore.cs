using System;
using System.Collections.Generic;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock
{
	public interface IEventStore
	{
		IEventStream LoadEventStream(Guid streamId);
		void AppendToStream(Guid streamId, long version, IEnumerable<IEvent> events);
	}
}