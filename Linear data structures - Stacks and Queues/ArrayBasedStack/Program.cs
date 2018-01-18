using System;

public class Program
{
    public static void Main(string[] args)
    {
        var stack = new ArrayStack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        var arr = stack.ToArray();

        stack.Pop();
        stack.Pop();

        arr = stack.ToArray();

        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
    }
}
