using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var queue = new LinkedQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);

        //Console.WriteLine(queue.Dequeue());
        //Console.WriteLine(queue.Dequeue());
        //Console.WriteLine(queue.Dequeue());
        //Console.WriteLine(queue.Dequeue());

        Console.WriteLine(string.Join(" ",queue.ToArray()));

        //var realQueue = new Queue<int>();

        //realQueue.Enqueue(1);
        //realQueue.Enqueue(2);
        //realQueue.Enqueue(3);
        //realQueue.Enqueue(4);

        //Console.WriteLine(string.Join(" ", realQueue.ToArray()));
    }
}
