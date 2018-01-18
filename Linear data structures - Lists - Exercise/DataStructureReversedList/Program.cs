using System;

public class Program
{
    public static void Main()
    {
        var reversedList = new ReversedList<int>();

        reversedList.Add(1);
        reversedList.Add(2);
        reversedList.Add(3);
        reversedList.Add(4);
        reversedList.Add(5);

        Console.WriteLine(reversedList[0]);
        Console.WriteLine(reversedList.Count);

        var removedElement = reversedList.RemoveAt(0);

        Console.WriteLine($"Removed element = {removedElement}");
        Console.WriteLine(reversedList.Count);

        foreach (var el in reversedList)
        {
            Console.WriteLine(el);
        }
    }
}
