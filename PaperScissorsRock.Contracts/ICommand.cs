using System;

namespace PaperScissorsRock.Contracts
{
	public interface ICommand
	{
		Guid AggregateId { get; }
	}
}