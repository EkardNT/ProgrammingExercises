using System;

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
			return word == word.Reverse();
		}

		private static string Reverse(this string @this)
		{
			var chars = new char[@this.Length];
			for (int i = 0; i < @this.Length; i++)
				chars[i] = @this[@this.Length - 1 - i];
			return new string(chars);
		}
	}
}