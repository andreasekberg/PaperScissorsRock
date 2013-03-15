using System;

namespace PaperScissorsRock.Contracts
{
	public class PlayerLeftGame : IEvent
	{
		public PlayerLeftGame(Guid gameId, string player)
		{
			GameId = gameId;
			Player = player;
		}

		public Guid GameId { get; set; }
		public string Player { get; set; }
	}
}