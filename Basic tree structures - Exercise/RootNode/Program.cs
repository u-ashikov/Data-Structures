using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static IDictionary<int, Tree<int>> nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = GetRootNode();
        Console.WriteLine($"Root node: {root.Value}");
    }

    private static void ReadTree()
    {
        var lines = int.Parse(Console.ReadLine());

        for (int i = 0; i < lines-1; i++)
        {
            var pairs = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var parent = GetTreeNodeByValue(pairs[0]);
            var child = GetTreeNodeByValue(pairs[1]);

            parent.Children.Add(child);
            child.Parent = parent;
        }
    }

    private static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodes.ContainsKey(value))
        {
            nodes.Add(value, new Tree<int>(value));
        }

        return nodes[value];
    }

    private static Tree<int> GetRootNode()
    {
        return nodes.Values.FirstOrDefault(n => n.Parent == null);
    }
}
