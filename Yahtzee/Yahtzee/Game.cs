using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
    static class Game
    {
		static private void GetWinner()
        {
			int max = 0;
			int index = -1;

			for (int i = 0; i < _players.Count; ++i)
			{
				int score = _players[i].Tally();
				Console.Write(_players[i] + "\n");
				if (score > max)
				{
					max = score;
					index = i;
				}
			}

			Console.Write(_players[index].Name + " is the winner.\n");
		}
		static private void GetPlayers()
        {
			int numPlayers = Util.Input("How many players? ", 1, 10);

			for (int i = 0; i < numPlayers; ++i)
			{
				string name = Util.InputString("Enter player " + Convert.ToString(i + 1) + "'s name: ");

				//  NEED TO IMPLEMENT COMPUTER
				bool isComputer = Util.Input("Is " + name + " a computer? ") == 'Y';
				_players.Add(new Player(name, isComputer));
			}
		}
		static private void GetStartingPlayer()
        {
			int max = 0;
			int first = 0;

			Console.Write("\n** Automatically rolling to see who goes first **\n\n");
			for (int i = 0; i < _players.Count; ++i)
			{
				int score = _players[i].Roll();

				Console.Write(_players[i].Name + " rolled  " + score + "\n");
				if (score > max)
				{
					max = score;
					first = i;
				}
			}

			Console.Write("\n" + _players[first].Name + " goes first\n");

			Player temp = _players[0];
			_players[0] = _players[first];
			_players[first] = temp;
		}

		static private List<Player> _players = new List<Player>();

		static private int _currentRound = 1;
		//static private int _currentPlayer = 0;
		static private readonly int _totalRounds = 13;

		public static void Go()
        {
			Console.Write("**** Welcome to Yahtzee ****\n\n");
			GetPlayers();
			GetStartingPlayer();

			while (_currentRound <= _totalRounds)
			{
				Console.Write("\n** Round: " + _currentRound + " **\n\n");
				foreach (Player player in _players)
					player.TakeTurn();

				++_currentRound;
			}

			GetWinner();
			Console.Write("\n**** Thanks for playing. ****\n");
		}
	}
}
