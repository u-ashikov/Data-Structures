namespace CountOfOccurences
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderBy(n=>n)
                .ToList();

            var uniqueNumbers = numbers.Distinct();

            foreach (var num in uniqueNumbers)
            {
                Console.WriteLine($"{num} -> {numbers.Count(n=>n==num)} times");
            }
        }
    }
}
