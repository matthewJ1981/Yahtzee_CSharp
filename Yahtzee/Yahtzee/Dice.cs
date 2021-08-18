using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
    class Dice : IEnumerable<Die>
    {
        private List<Die> _dice;
        public Dice()
        {
            _dice = new List<Die>();
        }

        public void AddDice(Die d)
        {
            _dice.Add(d);
        }

        public void RemoveDice(int index)
        {
            _dice.RemoveAt(index);
        }

        public void Roll()
        {
            foreach (Die d in _dice)
                d.Roll();
        }

        public int Size()
        {
            return _dice.Count;
        }

        public bool Empty()
        {
            return Size() == 0;
        }

        public Die this[int index]
        {
            get => _dice[index];
            set => _dice[index] = value;
        }


        public static Dice YahtzeeDice()
        {
            Dice dice = new Dice();
            for (int i = 0; i < 5; ++i)
                dice._dice.Add(new Die());

            return dice;
        }

        public static Dice operator + (Dice lhs, Dice rhs)
        {
            Dice dice = lhs;
	        for (int i = 0; i < rhs.Size(); ++i)
		        dice.AddDice(rhs[i]);

	        return dice;
        }

    public override string ToString()
        {
            string result = "";
            for(int i = 0; i < _dice.Count; ++i)
            {
                result += _dice[i].ToString();
                if (i < _dice.Count - 1)
                    result += ", ";
            }

            return result;
        }

        public IEnumerator<Die> GetEnumerator()
        {
            for (int i = 0; i < _dice.Count; ++i)
                yield return _dice[i];

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dice).GetEnumerator();
        }
    }
    

}
