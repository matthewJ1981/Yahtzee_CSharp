using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
	class Upper : Section
	{
		private readonly static int _bonusThreshold = 63;
		private readonly static int _bonusValue = 35;
		public Upper()
        {
			_categories.Add(new Ones());
			_categories.Add(new Twos());
			_categories.Add(new Threes());
			_categories.Add(new Fours());
			_categories.Add(new Fives());
			_categories.Add(new Sixes());
		}

		public override  int Tally()
        {
			CalcSubTotal();
			if (_subtotal >= _bonusThreshold)
				_bonus = _bonusValue;

			_total = _subtotal + _bonus;
			return _total;
		}

        public override string ToString()
		{
			string result = "";
            result += "UPPER SECTION\n\n";
			foreach (Category category in _categories)
				result += category.ToString() + "\n";

			
			result += "TOTAL SCORE: " + _total + "\n";
			result += "Bonus: " + _bonus + "\n";
			result += "TOTAL: " + _total;

			return result;
		}
	};

	class Lower : Section
	{
		private bool _yahtzee = false;
		private static readonly int _bonusValue = 100;
		public	Lower()
		{
			_categories.Add(new ThreeOfKind());
			_categories.Add(new FourOfKind());
			_categories.Add(new FullHouse());
			_categories.Add(new SmallStraight());
			_categories.Add(new LargeStraight());
			_categories.Add(new Yahtzee());
			_categories.Add(new Chance());
		}
		public void IncrementBonus()
		{
			_bonus += _bonusValue;
		}

		public bool HasYahtzee
        {
			get { return _yahtzee; }
			set { _yahtzee = value; }
        }
		public override int Tally()
        {
			CalcSubTotal();
			_total = _subtotal + _bonus;
			return _total;
		}
		public bool BonusEligible() 
		{
			return _categories[(int)LOWER.YAHTZEE].Score == Yahtzee.ScoreValue();
		}
		List<Tuple<string, int>> CheckScores(Dice dice, List<Tuple<string, int>> upperScores)
        {
			//Check scores as normal, then check for yahtzee
			List<Tuple<string, int>> scores = base.CheckScores(dice);

			//Check if a yahtzee was rolled
			HasYahtzee = scores[(int)LOWER.YAHTZEE].Item2 == Yahtzee.ScoreValue();

			//Set yahtzee category as unscorable if it has already been scored
			if (_categories[(int)LOWER.YAHTZEE].Scored)
			{
				string tempName = scores[(int)LOWER.YAHTZEE].Item1;
				int tempScore = Category.Unscorable;
				scores[(int)LOWER.YAHTZEE] = new Tuple<string, int>(tempName, tempScore);
			}

			//See if bonus and joker rules apply
			if (HasYahtzee)
			{
				//Get bonus for subsequent yahtzees
				if (BonusEligible())
				{
					scores.Add(new Tuple<string, int>("Bonus", _bonusValue));
					IncrementBonus();
				}

				//Force scoring of upper section or allow options of lower per joker rules
				if (_categories[(int)LOWER.YAHTZEE].Scored)
				{
					//Value of first dice - 1(They are currently all the same value) 
					//to access the index of the appropriate upper category
					int upperIndex = dice[0].Value - 1;

					//If the upper value has already been scored, the player can choose one of the lower sections to score
					if (upperScores[upperIndex].Item2 == Category.Unscorable)
					{
						if (scores[(int)LOWER.FULLHOUSE].Item2 != Category.Unscorable)
							scores[(int)LOWER.FULLHOUSE] = new Tuple<string, int>(scores[(int)LOWER.FULLHOUSE].Item1, FullHouse.ScoreValue());
											 
						if (scores[(int)LOWER.SMALLSTRAIGHT].Item2 != Category.Unscorable)
							scores[(int)LOWER.SMALLSTRAIGHT] = new Tuple<string, int>(scores[(int)LOWER.SMALLSTRAIGHT].Item1, SmallStraight.ScoreValue());
											 
						if (scores[(int)LOWER.LARGESTRAIGHT].Item2 != Category.Unscorable)
							scores[(int)LOWER.LARGESTRAIGHT] = new Tuple<string, int>(scores[(int)LOWER.LARGESTRAIGHT].Item1, LargeStraight.ScoreValue());
					}
					//Otherwise the player must score in the upper section
					else
					{
						scores[(int)LOWER.THREEOFAKIND] = new Tuple<string, int>(scores[(int)LOWER.THREEOFAKIND].Item1, Category.Unscorable);
						scores[(int)LOWER.FOUROFAKIND] = new Tuple<string, int>(scores[(int)LOWER.FOUROFAKIND].Item1, Category.Unscorable);
						scores[(int)LOWER.FULLHOUSE] = new Tuple<string, int>(scores[(int)LOWER.FULLHOUSE].Item1, Category.Unscorable);
						scores[(int)LOWER.SMALLSTRAIGHT] = new Tuple<string, int>(scores[(int)LOWER.SMALLSTRAIGHT].Item1, Category.Unscorable);
						scores[(int)LOWER.LARGESTRAIGHT] = new Tuple<string, int>(scores[(int)LOWER.LARGESTRAIGHT].Item1, Category.Unscorable);
						scores[(int)LOWER.YAHTZEE] = new Tuple<string, int>(scores[(int)LOWER.YAHTZEE].Item1, Category.Unscorable);
						scores[(int)LOWER.CHANCE] = new Tuple<string, int>(scores[(int)LOWER.CHANCE].Item1, Category.Unscorable);
					}
				}
			}

			return scores;
		}
		public static int BonusValue()
		{
			return _bonusValue;
		}

        public override string ToString()
		{
			string result = "";
            result += "LOWER SECTION\n\n";
			foreach (Category cat in _categories)
				result += cat.ToString() + "\n";

			result += "BONUS: " + _bonus + "\n";
			result += "TOTAL - LOWER: " + _total;

			return result;
		}
    }
}
