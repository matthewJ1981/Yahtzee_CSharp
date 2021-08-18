using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
	class Ones : Category
	{
		public Ones() : base("Ones")
		{ }
		public override int CheckScore(Dice dice)
        {
			return UpperCheckscore(dice, 1);
        }
	};

    class Twos : Category
    {
        public Twos() : base("Twos")
        { }
        public override int CheckScore(Dice dice)
        {
            return UpperCheckscore(dice, 2);
        }
    };

    class Threes : Category
    {
        public Threes() : base("Threes")
        { }
        public override int CheckScore(Dice dice)
        {
            return UpperCheckscore(dice, 3);
        }
    };
    class Fours : Category
    {
        public Fours() : base("Fours")
        { }
        public override int CheckScore(Dice dice)
        {
            return UpperCheckscore(dice, 4);
        }
    };
    class Fives : Category
    {
        public Fives() : base("Fives")
        { }
        public override int CheckScore(Dice dice)
        {
            return UpperCheckscore(dice, 5);
        }
    };
    class Sixes : Category
    {
        public Sixes() : base("Sixes")
        { }
        public override int CheckScore(Dice dice)
        {
            return UpperCheckscore(dice, 6);
        }
    };
}
