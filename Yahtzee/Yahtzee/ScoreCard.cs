using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
	class ScoreCard
	{
		private Upper _upper;
		private Lower _lower;
		private int _total;
		public ScoreCard() 
		{
			_upper = new Upper();
			_lower = new Lower();
			_total = 0;
		}
		public List<Tuple<string, int>> CheckScore(Dice dice)
        {
			// Check scoring options
			List<Tuple<string, int>> upperScoringCategories = _upper.CheckScores(dice);
			List<Tuple<string, int>> lowerScoringCategories = _lower.CheckScores(dice, upperScoringCategories);

			upperScoringCategories.AddRange(lowerScoringCategories);

			return upperScoringCategories;
		}
		public List<int> GetScores()
        {
			List<int> upperScores = _upper.GetScores();
			List<int> lowerScores = _lower.GetScores();

			upperScores.AddRange(lowerScores);

			return upperScores;
		}

		public bool SetScore(int index, int score)
        {
			if (index < (int)ALL.THREEOFAKIND)
				return _upper.SetScore(index, score);
			else
				return _lower.SetScore(index - (int)ALL.THREEOFAKIND, score);
		}
		public int Tally()
        {
			return _upper.Tally() + _lower.Tally();
		}

        public override string ToString()
		{
			string result = "";
            result += _upper + "\n" + _lower  +"\n";
			result += "TOTAL - UPPER: " + _upper.Total + "\n";
			result += "GRAND TOTAL: " + _upper.Total + _lower.Total;

			return result;
		}

	}}
