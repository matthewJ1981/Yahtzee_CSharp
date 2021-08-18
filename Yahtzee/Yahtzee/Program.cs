using System;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            Dice dice = Dice.YahtzeeDice();
            dice.Roll();

            foreach (Die die in dice)
                Console.WriteLine(die);
        }
    }
}
