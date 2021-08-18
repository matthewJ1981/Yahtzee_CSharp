using System;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            Dice dice = Dice.YahtzeeDice();
            dice.Roll();
            Console.WriteLine(dice);
            dice.Roll();
            Console.WriteLine(dice);
            dice.Roll();
            Console.WriteLine(dice);

            Dice dice2 = Dice.YahtzeeDice();
            dice2.Roll();
            Console.WriteLine(dice2);
            Console.WriteLine(dice + dice2);
        }
    }
}
