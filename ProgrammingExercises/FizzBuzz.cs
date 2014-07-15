using System;
namespace ProgrammingExercises
{
	public static class FizzBuzz
	{
		public static void Execute(int min = 1, int max = 100)
		{
			for (int i = min; i <= max; i++)
			{
				if (i % 5 == 0 && i % 3 == 0)
					Console.WriteLine("FizzBuzz");
				else if (i % 5 == 0)
					Console.WriteLine("Buzz");
				else if (i % 3 == 0)
					Console.WriteLine("Fizz");
				else
					Console.WriteLine(i);
			}
		}
	}
}