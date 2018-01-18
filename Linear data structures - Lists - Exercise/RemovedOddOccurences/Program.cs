namespace RemovedOddOccurences
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
                .ToList();

            var uniqueNumbers = numbers.Distinct();

            foreach (var num in uniqueNumbers.ToList())
            {
                var occurences = numbers.Count(n => n == num);

                if (occurences % 2 != 0)
                {
                    numbers.RemoveAll(n=>n == num);
                }
            }

            Console.WriteLine(string.Join(" ",numbers));
        }
    }
}
