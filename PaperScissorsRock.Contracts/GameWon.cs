using System;

namespace PaperScissorsRock.Contracts
{
	public class GameWon : IEvent
	{
		public GameWon(Guid gameId, string winner, string loser)
		{
			GameId = gameId;
			Winner = winner;
			Loser = loser;
		}

		public Guid GameId { get; private set; }
		public string Winner { get; private set; }
		public string Loser { get; private set; }

		public override string ToString()
		{
			return "Player " + Winner + " won and player " + Loser + " lost";
		}
	}
}