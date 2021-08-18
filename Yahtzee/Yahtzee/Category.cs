using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
    abstract class Category
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
            set { _score = value; }
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
            return 0;
        }

        public bool StraightScore(Dice dice, int numConsec)
        {
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
