using System;
using System.Linq;
using Machine.Specifications;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock.Domain.Tests.Unit
{
	[Subject(typeof (ApplicationService))]
	public class when_Specification
	{
		static ApplicationService service;

		static Guid gameId;
		static string player1;
		static string player2;
		static EventStore eventStore;

		Establish context = () =>
			{
				eventStore = new EventStore();
				service = new ApplicationService(eventStore);

				gameId = new Guid("F47F2AD4-C971-4252-A810-5E84A420E3CA");
				player1 = "niklas.lundberg@jayway.com";
				player2 = "andreas.ekberg@jayway.com";

				service.Handle(new CreateGame(gameId, 2, player1, player2, "Pizza"));
				service.Handle(new MakeMove(gameId, player1, Move.Rock));
			};

		Because of = () => service.Handle(new MakeMove(gameId, player2, Move.Paper));

		It should_Behaviour = () => eventStore.LoadEventStream(gameId).Any(@event => @event is RoundWon).ShouldBeTrue();

		It should_Behaviour2 =
			() => eventStore.LoadEventStream(gameId).OfType<RoundWon>().Single().Player.ShouldEqual(player2);
	}

	[Subject(typeof (ApplicationService))]
	public class when_Specification2
	{
		static ApplicationService service;

		static Guid gameId;
		static string player1;
		static string player2;
		static EventStore eventStore;

		Establish context = () =>
			{
				eventStore = new EventStore();
				service = new ApplicationService(eventStore);

				gameId = new Guid("F47F2AD4-C971-4252-A810-5E84A420E3CA");
				player1 = "niklas.lundberg@jayway.com";
				player2 = "andreas.ekberg@jayway.com";

				service.Handle(new CreateGame(gameId, 2, player1, player2, "Pizza"));
				service.Handle(new MakeMove(gameId, player1, Move.Rock));
				service.Handle(new MakeMove(gameId, player2, Move.Scissors));
				service.Handle(new MakeMove(gameId, player1, Move.Scissors));
				service.Handle(new MakeMove(gameId, player2, Move.Rock));
				service.Handle(new MakeMove(gameId, player1, Move.Scissors));
				service.Handle(new MakeMove(gameId, player2, Move.Scissors));
				service.Handle(new MakeMove(gameId, player1, Move.Rock));
			};

		Because of = () => service.Handle(new MakeMove(gameId, player2, Move.Scissors));

		It should_Behaviour2 =
			() =>
				{
					eventStore.LoadEventStream(gameId).OfType<GameWon>().Single().Winner.ShouldEqual(player1);
					eventStore.Print(gameId);
				};
	}
}