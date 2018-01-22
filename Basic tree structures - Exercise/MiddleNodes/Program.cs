using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static IDictionary<int, Tree<int>> nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        Tree<int> root = nodes.Values.FirstOrDefault(n => n.Parent == null);
        IList<int> middleNodes = new List<int>();
        PrintMiddleNodes();
    }

    private static void PrintMiddleNodes()
    {
        var middleNodes = nodes.Values
            .Where(n => n.Parent != null && n.Children.Count != 0)
            .Select(n => n.Value)
            .OrderBy(n => n);

        Console.WriteLine($"Middle nodes: {string.Join(" ", middleNodes)}");
    }

    private static void ReadTree()
    {
        var lines = int.Parse(Console.ReadLine());

        for (int i = 0; i < lines - 1; i++)
        {
            var pairs = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var parent = GetNodeValue(pairs[0]);
            var child = GetNodeValue(pairs[1]);

            parent.Children.Add(child);
            child.Parent = parent;
        }
    }

    private static Tree<int> GetNodeValue(int value)
    {
        if (!nodes.ContainsKey(value))
        {
            nodes.Add(value, new Tree<int>(value));
        }

        return nodes[value];
    }
}
