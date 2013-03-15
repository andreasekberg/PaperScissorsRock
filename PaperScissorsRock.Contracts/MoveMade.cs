using System;

namespace PaperScissorsRock.Contracts
{
	public class MoveMade : IEvent
	{
		public MoveMade(Guid gameId, string playerId, Move move)
		{
			AggregateId = gameId;
			PlayerId = playerId;
			Move = move;
		}

		public string PlayerId { get; private set; }
		public Move Move { get; private set; }
		public Guid AggregateId { get; private set; }

		public override string ToString()
		{
			return "Player " + PlayerId + " made move " + Move;
		}
	}
}