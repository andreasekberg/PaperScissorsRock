using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock
{
	public class EventStore : IEventStore
	{
		readonly ConcurrentDictionary<Guid, LinkedList<IEvent>> _eventStream =
			new ConcurrentDictionary<Guid, LinkedList<IEvent>>();

		public IEventStream LoadEventStream(Guid streamId)
		{
			CreateStreamIfNotExists(streamId, new List<IEvent>());

			var events = _eventStream[streamId];

			lock (events)
			{
				var eventList = events.ToArray();

				return new EventStream(eventList.Length, eventList);
			}
		}

		public void AppendToStream(Guid streamId, long version, IEnumerable<IEvent> events)
		{
			CreateStreamIfNotExists(streamId, new List<IEvent>());

			var eventList = _eventStream[streamId];

			lock (eventList)
			{
				var eventsToAdd = events.ToList();

				var actualVersion = eventList.Count;

				var expectedVersion = version;

				if (expectedVersion != version)
				{
					throw new InvalidOperationException("Optimistic concurrency, expected version " + expectedVersion + " but was " +
					                                    actualVersion);
				}

				foreach (var @event in eventsToAdd)
				{
					_eventStream[streamId].AddLast(@event);
				}
			}
		}

		void CreateStreamIfNotExists(Guid streamId, IEnumerable<IEvent> events)
		{
			if (_eventStream.ContainsKey(streamId))
			{
				return;
			}

			while (!_eventStream.ContainsKey(streamId))
			{
				var added = _eventStream.TryAdd(streamId, new LinkedList<IEvent>(events));

				if (added)
				{
					return;
				}
			}
		}
	}

	public static class EventStoreExtensions
	{
		public static void Print(this IEventStore eventStore, Guid streamId)
		{
			var @events = eventStore.LoadEventStream(streamId);

			foreach (var @event in events.ToList())
			{
				Console.WriteLine(@event);
			}
		}
	}
}