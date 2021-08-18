using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Yahtzee
{
	class Player
	{
		private string _name;
		private bool _isComputer;
		private bool _rolled;
		private ScoreCard _scoreCard;
		private Dice _rollable;
		private Dice _held;
		public Player(string name, bool isComputer)
		{
			_name = name;
			_isComputer = isComputer;
			_rolled = false;
			_scoreCard = new ScoreCard();
			_rollable = new Dice();
			_held = new Dice();
		}
		public void TakeTurn()
		{
			bool playerScored = false;
			_rolled = false;
			int currentRoll = 1;

			while (currentRoll <= 3 && playerScored == false)
			{
				Console.Write("Player: " + _name +  "\n");
				Console.Write("You have " + Convert.ToString( 4 - currentRoll) + " rolls remaining\n\n");
				int choice = -1;

				if (_rolled)
				{
					Console.Write("ReadyDice: " + _rollable + "\n");
					Console.Write("HeldDice: " + _held + "\n\n");

					choice = Util.Input("Roll(1), Hold(2), UnHold(3) Score(4), Show scorecard(5), Quit(6): ", 1, 6);
				}
				else
				{
					choice = Util.Input("Roll(1), Show scorecard(2), Quit(3): ", 1, 3);
					if (choice == 2)
						choice = 5;
					if (choice == 3)
						choice = 6;
				}

				if (currentRoll == 3 && choice != 4)
					choice = 4;

				switch (choice)
				{
					case 1:
						Roll();
						++currentRoll;
						break;
					case 2:
						Hold();
						break;
					case 3:
						Unhold();
						break;
					case 4:
						playerScored = Score();
						break;
					case 5:
						Print();
						break;
					case 6:
						Console.Write("Need to implement\n");
						break;
					default:
						throw new ArgumentException("Not supposed to get here");
				}
			}
		}
		public void MoveDice(Dice lhs, Dice rhs, List<int> indices)
        {
			if (lhs.Empty())
				return;

			for (int i = 0; i < indices.Count; ++i)
			{
				int adjustedIndex = indices[i] - i;
				rhs.AddDice(lhs[adjustedIndex]);
				lhs.RemoveDice(adjustedIndex);
			}
		}

		public int Roll()
        {
			_rollable.Roll();
			_rolled = true;

			int total = 0;
			foreach (Die die in _rollable)
				total += die.Value;
			return total;
		}
		public void Hold()
        {
			if (!_rolled)
			{
				Console.Write("You need to roll the dice first\n");
				return;
			}

			if (!_rollable.Empty())
			{
				List<int> diceToMove = GetDiceToMove(_rollable);
				MoveDice(_rollable, _held, diceToMove);
			}
		}
		public void Unhold()
        {
			if (!_rolled)
			{
				Console.Write("You need to roll the dice first\n");
				return;
			}

			if (!_held.Empty())
			{
				List<int> diceToMove = GetDiceToMove(_held);
				MoveDice(_held, _rollable, diceToMove);
			}
		}
		public List<int> GetDiceToMove(Dice dice)
        {
			List<int> diceToMove = new List<int>();

			string msg = "Select which dice to move: \n";
			for (int i = 0; i < dice.Size(); ++i)
			{
				msg += Convert.ToString(dice[i].Value) + " (" + Convert.ToString(i) + ")\n";
			}
			msg += "\n";

			bool inputting = true;
			while (inputting)
			{
				inputting = false;
				string input = Util.InputString(msg);
				string[] indices = input.Split(' ');
				int diceNum = -1;

				foreach (string str in indices)
                {
					int index = Convert.ToInt32(str);

					bool duplicate = false;
					foreach (int i in diceToMove)
						if (diceNum == i)
							duplicate = true;

					if (diceNum >= 0 && diceNum < dice.Size() && duplicate == false)
					{
						diceToMove.Add(diceNum);
					}
					else
					{
						diceToMove.Clear();
						inputting = true;
					}
				}
			}

			diceToMove.Sort();

			return diceToMove;
		}
		public bool Score()
        {
			if (!_rolled)
			{
				Console.Write("You need to roll the dice first\n");
				return false;
			}


			Console.Write("\n*** Scoring options*** \n\n");

			List<Tuple<string, int>> scores = _scoreCard.CheckScore(_rollable + _held);

			for (int i = 0; i < scores.Count; ++i)
				if (scores[i].Item2 != Category.Unscorable)
					Console.Write(Convert.ToString(i) + ": " + scores[i].Item1 + ": " + Convert.ToString(scores[i].Item2) + "\n");

			Console.WriteLine();

			bool playerScored = false;
			do
			{
				int index = Util.Input("Select category to score: ", 0, 12);
				playerScored = _scoreCard.SetScore(index, scores[index].Item2);
				if (!playerScored)
					Console.Write("Category already scored, try again\n\n");
			} while (playerScored == false);

			//FIX - Give option to change mind
			return true;
		}

		public string Name 
		{
			get { return _name; }
		}
		public bool Iscomputer
		{
			get { return _isComputer; }
		}

		public int Tally()
		{
			return _scoreCard.Tally(); 
		}

		public void Print()
        {
			Console.Write("Name: " + _name + "\n");
			Console.Write(_scoreCard + "\n\n");
		}
        public override string ToString()
		{
           	return "Name: " + _name + " Score: " + _scoreCard.Tally();
		}
    };
}
