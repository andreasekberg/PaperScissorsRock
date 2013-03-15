using System;

namespace PaperScissorsRock.Contracts
{
	public class MakeMove : ICommand
	{
		public MakeMove(Guid gameId, string playerId, Move move)
		{
			AggregateId = gameId;
			PlayerId = playerId;
			Move = move;
		}

		public Guid AggregateId { get; private set; }
		public string PlayerId { get; private set; }
		public Move Move { get; private set; }
	}
}