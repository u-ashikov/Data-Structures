namespace CalculateSequence
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            var inputNumber = int.Parse(Console.ReadLine());

            var sequence = new Queue<int>();
            sequence.Enqueue(inputNumber);

            var progress = new Queue<int>();
            progress.Enqueue(inputNumber);

            for (int i = 0; i <= 16; i++)
            {
                var s = progress.Dequeue();

                sequence.Enqueue(s + 1);
                progress.Enqueue(s + 1);

                sequence.Enqueue(2 * s + 1);
                progress.Enqueue(2 * s + 1);

                sequence.Enqueue(s + 2);
                progress.Enqueue(s + 2);
            }

            Console.WriteLine(string.Join(", ",sequence.Take(50)));
        }
    }
}
