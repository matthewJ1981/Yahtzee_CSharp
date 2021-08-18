using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
    abstract class Section
    {
		protected List<Category> _categories;
		protected int _subtotal;
		protected int _bonus;
		protected int _total;

		public Section() 
		{
			_subtotal = 0;
			_bonus = 0;
			_total = 0;
			_categories = new List<Category>();
		}

		public int Subtotal
		{
			get { return _subtotal; }
		}

		public int Bonus
		{
			get	{ return _bonus; }
		}

		public int Total
		{
			get	{ return _total; }
		}

		public int Size()
		{
			return _categories.Count; 
		}
		public bool SetScore(int index, int score)
        {

			if (_categories[index].Scored)
				return false;

			_categories[index].Score = score;
			return true;
		}

		public List<Tuple<string, int>> CheckScores(Dice dice)
        {
			List<Tuple<string, int>> scoringCategories = new List<Tuple<string, int>>();

			for (int i = 0; i < Size(); ++i)
				scoringCategories.Add(new Tuple<string, int>(_categories[i].Name, _categories[i].CheckScore(dice)));

			return scoringCategories;
		}
		public List<int> GetScores() 
		{
			List<int> scoringCategories = new List<int>();

			for (int i = 0; i < Size(); ++i)
				scoringCategories.Add(_categories[i].Score);

			return scoringCategories;
		}

		public void CalcSubTotal()
        {
			int temp = 0;

			foreach(Category category in _categories) 
				if (category.Score > -1) temp += category.Score;

			_subtotal = temp;
		}
		public abstract int Tally();
    };
}
