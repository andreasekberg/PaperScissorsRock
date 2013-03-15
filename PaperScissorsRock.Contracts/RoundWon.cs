using System;

namespace PaperScissorsRock.Contracts
{
	public class RoundWon : IEvent
	{
		public RoundWon(Guid gameId, string player)
		{
			GameId = gameId;
			Player = player;
		}

		public Guid GameId { get; set; }
		public override string ToString()
		{
			return "Player " + Player + " won";
		}
		public string Player { get; set; }
	}
}