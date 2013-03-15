using System;

namespace PaperScissorsRock.Contracts
{
	public class RageQuit : ICommand
	{
		public RageQuit(string player, Guid gameId)
		{
			Player = player;
			GameId = gameId;
		}

		public string Player { get; private set; }
		public Guid GameId { get; private set; }
		public Guid AggregateId { get; private set; }
	}
}