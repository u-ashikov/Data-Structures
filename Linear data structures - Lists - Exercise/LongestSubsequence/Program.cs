namespace LongestSubsequence
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            List<int> numbers = ParseNumbers();

            int startIndex = 0;
            int maxCount = 1;

            FindLongestSequence(numbers, ref startIndex, ref maxCount);

            PrintResult(startIndex,maxCount,numbers);
        }

        private static List<int> ParseNumbers()
        {
            return Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        private static void FindLongestSequence(List<int> numbers, ref int startIndex, ref int maxCount)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                var num = numbers[i];
                int occurences = 1;

                for (int p = i + 1; p < numbers.Count; p++)
                {
                    var nextNum = numbers[p];

                    if (nextNum == num)
                    {
                        occurences++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (occurences > maxCount)
                {
                    maxCount = occurences;
                    startIndex = i;
                }
            }
        }

        private static void PrintResult(int startIndex, int maxCount, List<int> numbers)
        {
            var result = new List<int>();

            for (int i = startIndex; i < startIndex + maxCount; i++)
            {
                result.Add(numbers[i]);
            }

            Console.WriteLine(string.Join(" ",result));
        }
    }
}
