using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
    abstract class Category //: ICloneable
    {
        protected string _name;
        protected int _score;
        protected bool _scored;
        protected const int _unscorable = -1;
        public Category(string name)
        {
            _name = name;
            _score = 0;
            _scored = false;
        }

        public string Name
        {
            get { return _name; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; _scored = true; }
        }

        public bool Scored
        {
            get { return _scored; }
            set { _scored = value; }
        }

        public int Unscorable
        {
            get { return _unscorable; }
        }

        public abstract int CheckScore(Dice dice);

        public int UpperCheckscore(Dice dice, int value)
        {
            if (Scored)
                return Unscorable;

            int newScore = 0;

            foreach(Die die in dice)
                if (die.Value == value) 
                    newScore += value;

            return newScore;
        }

        public int KindScore(Dice dice, int numSame)
        {
            if (Scored)
                return Unscorable;

            int newScore = 0;

            int[] values = new int[7];
            foreach (Die die in dice)
	{
                ++values[die.Value];
                newScore += die.Value;
            }

            for (int i = 1; i < 7; ++i)
                if (values[i] >= numSame)
                    return newScore;

            return 0;
        }

        public bool StraightScore(Dice dice, int numConsec)
        {
            int numConsecutive = 0;
            int maxConsecutive = 0;
            int[] values = new int[7];
            foreach (Die die in dice)
		        ++values[die.Value];

            for (int i = 1; i < 7; ++i)
            {
                if (values[i] > 0)
                {
                    ++numConsecutive;
                }
                else
                {
                    if (numConsecutive > maxConsecutive)
                        maxConsecutive = numConsecutive;
                    numConsecutive = 0;
                }
            }

            return maxConsecutive >= numConsec;
        }

        public override string ToString()
        {
            string result = "";
            string temp = _name;
            temp = temp.PadRight(15);
            result += temp;

            temp = Convert.ToString(_score);
            temp.PadLeft(8);
            result += temp;

            return result;
        }

        //public object Clone()
        //{
        //    return this.MemberwiseClone();
        //}
    }
}
