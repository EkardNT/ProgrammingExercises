using System;

namespace ProgrammingExercises
{
	public class BinarySearch
	{
		public static void Execute(int target, params int[] array)
		{
			int index, iterations;
			if (Search(target, array, out index, out iterations))
				Console.WriteLine("Element {0} was found at index {1} after {2} iterations.", target, index, iterations);
			else
				Console.WriteLine("Element {0} was not found in the array after an exhaustive {1} iterations.", target, iterations);
		}

		private static bool Search(int target, int[] array, out int index, out int iterations)
		{
			index = iterations = 0;
			int start = 0, end = array.Length - 1;
			while (start <= end)
			{
				iterations++;
				index = (start + end) / 2;
				if (array[index] == target)
					return true;
				if (array[index] < target)
					start = index + 1;
				else
					end = index - 1;
			}
			return false;
		}
	}
}