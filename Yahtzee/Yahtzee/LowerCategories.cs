using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
	class ThreeOfKind : Category
	{
		public ThreeOfKind() : base("Three of a kind")
		{ }
		public override int CheckScore(Dice dice)
        {
			return KindScore(dice, 3);
		}
	};

	class FourOfKind : Category
	{
		public FourOfKind() : base("Four of a kind")
		{ }
		public override int CheckScore(Dice dice)
		{
			return KindScore(dice, 4);
		}
	};
	class FullHouse : Category
	{
		public FullHouse() : base("Full house")
		{ }
		public static int ScoreValue() { return 25; }
		public override int CheckScore(Dice dice)
		{
			if (Scored)
				return Unscorable;

			bool twoKind = false;
			bool threeKind = false;

			int[] values = new int[7];
			foreach (Die die in dice)
				++values[die.Value];

			for (int i = 1; i < 7; ++i)
			{
				if (values[i] >= 3)
					threeKind = true;
				else if (values[i] >= 2)
					twoKind = true;
			}

			if (twoKind && threeKind)
				return ScoreValue();
			else
				return 0;
		}
	};

	class SmallStraight : Category
	{
		public	SmallStraight() : base("Small straight")
		{ }
		public static int ScoreValue() { return 30; }
		public override int CheckScore(Dice dice)
		{
			if (Scored)
				return Unscorable;

			if (StraightScore(dice, 4))
				return ScoreValue();

			return 0;
		}
	};

	class LargeStraight : Category
	{
		public LargeStraight() : base("Large straight")
		{ }
		public static int ScoreValue() { return 40; }
		public override int CheckScore(Dice dice)
		{
			if (Scored)
				return Unscorable;

			if (StraightScore(dice, 5))
				return ScoreValue();

			return 0;
		}
	};

	class Yahtzee : Category
		{
		public	Yahtzee() : base("Yahtzee")
		{ }
		public static int ScoreValue() { return 50; }
		public override int CheckScore(Dice dice)
        {
			int value = dice[0].Value;
			foreach (Die d in dice)
			if (d.Value != value)
				return 0;

			return 50;
		}
	};

	class Chance : Category
	{
		public	Chance() : base("Chance")
		{ }
		public override int CheckScore(Dice dice)
        {
			if (Scored)
				return Unscorable;

			int temp = 0;
			foreach (Die d in dice)
			temp += d.Value;

			return temp;
		}
	};
}
