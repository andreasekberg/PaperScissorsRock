using System;

namespace PaperScissorsRock.Contracts
{
	public class GameCreated : IEvent
	{
		public GameCreated(Guid gameId, int firstTo, string createdBy, string opponent, string reason)
		{
			AggregateId = gameId;
			FirstTo = firstTo;
			CreatedBy = createdBy;
			Opponent = opponent;
			Reason = reason;
		}

		public string Opponent { get; private set; }

		public string CreatedBy { get; private set; }

		public int FirstTo { get; private set; }
		public Guid AggregateId { get; private set; }

		public string Reason { get; private set; }

		public override string ToString()
		{
			return "Created by " + CreatedBy + ", opponent " + Opponent + ", reason " + Reason + ", first to " + FirstTo;
		}
	}
}