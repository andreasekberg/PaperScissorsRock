using System;
using System.Collections.Generic;
using PaperScissorsRock.Contracts;

namespace PaperScissorsRock
{
	public interface IApplicationService
	{
		void Handle(ICommand command);
	}

	public class Game
	{
		readonly Guid _gameId;
		readonly Dictionary<string, int> _scores = new Dictionary<string, int>();
		string _createdBy;
		int _firstTo;
		Tuple<Move, string> _latestMove;
		string _opponent;
		string _reason;
		GameState _state = GameState.Undecided;

		public Game(Guid gameId)
		{
			_gameId = gameId;
			_state = GameState.NotCreated;
		}

		public void Handle(MoveMade moveMade)
		{
			_latestMove = new Tuple<Move, string>(moveMade.Move, moveMade.PlayerId);
			_state = GameState.WaitingForMove;
		}

		public IEnumerable<IEvent> Handle(MakeMove move)
		{
			CanMakeMove(move);

			yield return new MoveMade(move.AggregateId, move.PlayerId, move.Move);

			if (_state == GameState.WaitingForMove)
			{
				if (_latestMove.Item1 == move.Move)
				{
					yield return new RoundTied(_gameId);
				}
				else
				{
					var winningPlayer = GetWinning(move);

					yield return new RoundWon(_gameId, winningPlayer);

					if (_scores[winningPlayer] + 1 == _firstTo)
					{
						yield return new GameWon(_gameId, winningPlayer, OtherPlayer(winningPlayer));
					}
				}
			}
		}

		string GetWinning(MakeMove move)
		{
			string winningPlayer = "";

			switch (move.Move)
			{
				case Move.Paper:
					winningPlayer = _latestMove.Item1 == Move.Scissors ? _latestMove.Item2 : OtherPlayer(_latestMove.Item2);
					break;
				case Move.Scissors:
					winningPlayer = _latestMove.Item1 == Move.Rock ? _latestMove.Item2 : OtherPlayer(_latestMove.Item2);
					break;
				case Move.Rock:
					winningPlayer = _latestMove.Item1 == Move.Paper ? _latestMove.Item2 : OtherPlayer(_latestMove.Item2);
					break;
			}
			return winningPlayer;
		}

		string OtherPlayer(string player)
		{
			return player.Equals(_createdBy, StringComparison.InvariantCultureIgnoreCase) ? _opponent : _createdBy;
		}

		void CanMakeMove(MakeMove move)
		{
			if (_state == GameState.NotCreated || _state == GameState.GameWon)
			{
				throw new InvalidOperationException("Invalid state");
			}

			if (_state == GameState.WaitingForMove)
			{
				if (_latestMove.Item2 == move.PlayerId)
				{
					throw new InvalidOperationException("Cannot make two moves...");
				}
			}
		}

		public void Handle(GameWon gameWon)
		{
			_state = GameState.GameWon;
		}

		public void Handle(RoundTied roundTied)
		{
			_state = GameState.Undecided;
		}

		public void Handle(RoundWon roundWon)
		{
			_scores[roundWon.Player]++;
			_state = GameState.Undecided;
		}

		public IEnumerable<IEvent> Handle(RageQuit rageQuit)
		{
			CheckCanQuit();

			yield return new PlayerLeftGame(_gameId, rageQuit.Player);
		}

		public void Handle(PlayerLeftGame playerLeftGame)
		{
			_state = GameState.GameWon;
		}

		void CheckCanQuit()
		{
			if (_state == GameState.NotCreated || _state == GameState.GameWon)
			{
				throw new InvalidOperationException("Invalid state");
			}
		}

		public IEnumerable<IEvent> Handle(CreateGame createGame)
		{
			CheckCanCreate();

			yield return
				new GameCreated(createGame.AggregateId, createGame.FirstTo, createGame.CreatedBy, createGame.Opponent,
				                createGame.Reason);
		}

		public void Handle(GameCreated @event)
		{
			_firstTo = @event.FirstTo;
			_createdBy = @event.CreatedBy;
			_opponent = @event.Opponent;
			_reason = @event.Reason;
			_state = GameState.Undecided;

			_scores.Add(_createdBy, 0);
			_scores.Add(_opponent, 0);
		}

		void CheckCanCreate()
		{
			if (_state != GameState.NotCreated)
			{
				throw new InvalidOperationException("Not in state " + GameState.NotCreated);
			}
		}
	}
}