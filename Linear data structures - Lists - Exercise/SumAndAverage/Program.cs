namespace SumAndAverage
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => int.Parse(n))
                .ToList();

            if (numbers.Count == 0)
            {
                Console.WriteLine("Sum=0; Average=0.00");
                return;
            }

            Console.WriteLine($"Sum={numbers.Sum()}; Average={numbers.Average().ToString("0.00")}");
        }
    }
}
