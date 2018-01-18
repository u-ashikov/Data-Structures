using System;

public class Program
{
    public static void Main()
    {
        var stack = new LinkedStack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
    }
}
