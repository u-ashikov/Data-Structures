namespace SortWords
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var words = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .OrderBy(w=>w)
                .ToList();

            Console.WriteLine(string.Join(" ", words));
        }
    }
}
