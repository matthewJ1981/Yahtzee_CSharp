using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
	class Die
	{
		private int _value;
		private int _sides;
		private Random _random;

		public Die(int sides = 6, int value = 1)
		{
			_value = value;
			_sides = sides;
			_random = new Random();
		}

		public int Value
        {
            get { return _value; }
			set { _value = value; }
        }

		public int Sides
        {
			get { return _sides; }
			set { _sides = value; }
        }

		public int Roll()
        {
			_value = _random.Next(1, _sides + 1);
			return _value;
        }

		public static bool operator < (Die lhs, Die rhs)
        {
			return lhs.Value < rhs.Value;
        }

		public static bool operator > (Die lhs, Die rhs)
		{
			return lhs.Value > rhs.Value;
		}

		public override string ToString()
        {
			return Convert.ToString(_value);
        }
    }
}
