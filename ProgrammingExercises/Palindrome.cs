using System;
using System.Linq;

namespace ProgrammingExercises
{
	public static class Palindrome
	{
		public static void Execute(string word, bool fast = false)
		{
			bool isPalindrome = fast ? TestFast(word) : TestSlow(word);
			Console.WriteLine("\"{0}\" {1} a palindrome.", word, isPalindrome ? "is" : "is not");
		}

		private static bool TestFast(string word)
		{
			for(int i = 0; i < word.Length / 2; i++)
			{
				if (word[i] != word[word.Length - 1 - i])
					return false;
			}
			return true;
		}

		private static bool TestSlow(string word)
		{
			return word == new string(word.Reverse().ToArray());
		}
	}
}