using System;

namespace PaperScissorsRock.Contracts
{
	public class CreatedGame : IEvent
	{
		public CreatedGame(Guid gameId, int firstTo, string createdBy, string opponent)
		{
			AggregateId = gameId;
			FirstTo = firstTo;
			CreatedBy = createdBy;
			Opponent = opponent;
		}

		public string Opponent { get; private set; }

		public string CreatedBy { get; private set; }

		public int FirstTo { get; private set; }
		public Guid AggregateId { get; private set; }
	}
}