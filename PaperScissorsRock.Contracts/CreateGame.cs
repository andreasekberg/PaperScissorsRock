using System;

namespace PaperScissorsRock.Contracts
{
	public class CreateGame : ICommand
	{
		public CreateGame(Guid gameId, int firstTo, string createdBy, string opponent, string reason)
		{
			//	Contract.Requires(gameId != Guid.Empty, "Game ID cannot be empty GUID");
			//	Contract.Requires(!string.IsNullOrWhiteSpace(createdBy), "Created by cannot be null or empty");
			//	Contract.Requires(!string.IsNullOrWhiteSpace(opponent), "Opponent by cannot be null or empty");

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
	}
}