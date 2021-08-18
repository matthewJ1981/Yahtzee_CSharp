using System;
using System.Collections.Generic;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> list = new List<Category>();
            list.Add(new Ones());
            list.Add(new Twos());

            foreach (Category c in list)
                Console.WriteLine(c);

        }
    }
}
