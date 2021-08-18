using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee
{
	static class Util
	{
		static public string InputString(string msg)
		{
			Console.WriteLine(msg);
			return Console.ReadLine();
		}
		static public int InputInt(string msg)
		{
			Console.WriteLine(msg);
			return Convert.ToInt32(Console.Read());
		}
		static public char InputChar(string msg)
		{
			Console.WriteLine(msg);
			return Convert.ToChar(Console.Read());
		}
		static public bool InputBool(string msg)
		{
			Console.WriteLine(msg);
			return Convert.ToBoolean(Console.Read());
		}

		/**
		* Common functionality of Input functions
		 */
		static public T InputHelper<T>(string msg, T selection)
		{
			Console.Write(msg);
			string input = Console.ReadLine();
			return (T)Convert.ChangeType(input, typeof(T));
		}

		/**
		 *	Perform some input validation for yes/no input
		 */
		static public char Input(string msg)
        {
			char selection = '\0';

			do
			{
				selection = Char.ToUpper(InputHelper(msg, selection));
			} while (selection != 'Y' && selection != 'N');

			return selection;
		}

		/**
		 *	Perform some input validation for selection within a range
		 */

		static public T Input<T>(string msg, T low, T high)
		{
			T selection = default;

			do
			{
				selection = InputHelper(msg, selection);
			} while (Comparer<T>.Default.Compare(selection,low) < 0 || Comparer<T>.Default.Compare(selection, high) > 0);

			return selection;
		}
	};
}
