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
        IList<int> leaves = new List<int>();
        GetLeaves(leaves,root);
        Console.WriteLine($"Leaf nodes: {string.Join(" ",leaves.OrderBy(l=>l))}");
    }

    private static void GetLeaves(IList<int> leaves, Tree<int> node)
    {
        foreach (var child in node.Children)
        {
            if (child.Children.Count == 0)
            {
                leaves.Add(child.Value);
            }
            else
            {
                GetLeaves(leaves, child);
            }
        }
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
