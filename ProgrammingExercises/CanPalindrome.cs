using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingExercises
{
	/// <summary>
	/// Determines whether there exists a permutation of the letters in the provided
	/// word which form a palindrome.
	/// </summary>
	public static class CanPalindrome
	{
		private class LetterFrequency
		{
			public char Letter { get; private set; }
			public int Frequency { get; private set; }

			public LetterFrequency(char letter, int frequency)
			{
				Letter = letter;
				Frequency = frequency;
			}
		}

		// Note: this doesn't properly handle UTF-16 surrogate pairs or
		// Unicode combining characters.
		public static void Execute(string word)
		{
			var letters = word
				.GroupBy(character => character)
				.Select(grouping => new LetterFrequency(grouping.Key, grouping.Count()));
			int numberOfOddFrequencyLetters = letters
				.Count(letter => letter.Frequency % 2 == 1);
			if (numberOfOddFrequencyLetters > 1)
				Console.WriteLine("Cannot create a palindrome from these letters.");
			else
			{
				Console.WriteLine("Can create a palindrome from these letters, for example \"{0}\".", CreatePalindrome(letters));
			}
		}

		private static string CreatePalindrome(IEnumerable<LetterFrequency> letters)
		{
			var builder = new StringBuilder();
			foreach (var letter in letters)
			{
				if (letter.Frequency % 2 == 0)
					builder.Append(letter.Letter, letter.Frequency / 2);
			}
			var middleLetter = letters.Where(ltr => ltr.Frequency % 2 == 1).FirstOrDefault();
			if (middleLetter != null)
				builder.Append(middleLetter.Letter, middleLetter.Frequency);
			foreach (var letter in letters.Reverse())
			{
				if (letter.Frequency % 2 == 0)
					builder.Append(letter.Letter, letter.Frequency / 2);
			}
			return builder.ToString();
		}
	}
}