using System.Collections.Generic;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock
{
	public interface IEventStream : IEnumerable<IEvent>
	{
		long Version();
	}
}