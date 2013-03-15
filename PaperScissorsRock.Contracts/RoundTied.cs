using System;

namespace PaperScissorsRock.Contracts
{
	public class RoundTied : IEvent
	{
		public RoundTied(Guid gameId)
		{
			GameId = gameId;
		}

		public override string ToString()
		{
			return "Round was tied";
		}

		public Guid GameId { get; set; }
	}
}