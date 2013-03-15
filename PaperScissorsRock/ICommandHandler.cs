namespace PaperScissorsRock
{
	public interface ICommandHandler<T>
	{
		void Handle(T command);
	}
}