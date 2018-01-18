namespace ReverseNumbers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            var input = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var numbers = new Stack<int>(input);

            Console.WriteLine(string.Join(" ",numbers));
        }
    }
}
