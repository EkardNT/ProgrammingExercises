using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingExercises
{
	/// <summary>
	/// Checks to see whether a string consists of matching
	/// and appropriately nested grouping symbols: (), {}, and [].
	/// For example, "()]" and "{[}]" do not pass, whereas "{([])}" does.
	/// </summary>
	public static class NestingCheck
	{
		private static ISet<char> openSymbols = new HashSet<char> { '(', '{', '[', };
		private static ISet<char> closeSymbols = new HashSet<char> { ')', '}', ']' };
		private static IDictionary<char, char> openToClose = new Dictionary<char, char> { { '(', ')' }, { '{', '}' }, { '[', ']' } };

		public static void Execute(params string[] str)
		{
			if (NestsCorrectly(string.Join("", str)))
				Console.WriteLine("The string is correctly nested.");
			else
				Console.WriteLine("This string is incorrectly nested.");
		}

		private static bool NestsCorrectly(string str)
		{
			var stack = new Stack<char>();
			foreach (var c in str)
			{
				if (openSymbols.Contains(c))
					stack.Push(c);
				else if (closeSymbols.Contains(c))
				{
					if (stack.Count == 0)
						return false;
					if (openToClose[stack.Pop()] != c)
						return false;
				}
			}
			return stack.Count == 0;
		}
	}
}