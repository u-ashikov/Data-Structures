using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static IDictionary<int, Tree<int>> nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = nodes.Values.FirstOrDefault(n => n.Parent == null);
        PrintTree(root);
    }

    private static void PrintTree(Tree<int> root,int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}{root.Value}");

        foreach (var child in root.Children)
        {
            PrintTree(child, indent+2);
        }
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

            var parent = GetTreeNode(pairs[0]);
            var child = GetTreeNode(pairs[1]);

            parent.Children.Add(child);
            child.Parent = parent;
        }
    }

    private static Tree<int> GetTreeNode(int value)
    {
        if (!nodes.ContainsKey(value))
        {
            nodes.Add(value, new Tree<int>(value));
        }

        return nodes[value];
    }
}
