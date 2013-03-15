using System.Collections;
using System.Collections.Generic;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock
{
	public class EventStream : IEventStream
	{
		readonly IEnumerable<IEvent> _events;
		readonly long _version;

		public EventStream(long version, IEnumerable<IEvent> events)
		{
			_version = version;
			_events = events;
		}

		public IEnumerator<IEvent> GetEnumerator()
		{
			return _events.GetEnumerator();
		}

		public long Version()
		{
			return _version;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}