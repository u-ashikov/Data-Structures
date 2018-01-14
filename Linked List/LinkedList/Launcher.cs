public class Launcher
{
    public static void Main()
    {
        var list = new LinkedList<int>();

        list.AddFirst(1);
        list.AddFirst(2);
        list.AddFirst(3);

        list.RemoveLast();
        list.RemoveLast();
        list.RemoveLast();

        System.Console.WriteLine();
    }
}
