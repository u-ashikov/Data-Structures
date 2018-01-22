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
        int deepestNode = GetDeepestNode();
        Console.WriteLine($"Deepest node: {deepestNode}");
    }

    private static int GetDeepestNode()
    {
        int deepestNode = 0;
        int maxDepth = 0;

        var deepestNodes = nodes.Values
            .Where(n => n.Children.Count == 0);

        foreach (var node in deepestNodes)
        {
            int depth = 1;
            var lookedUpNode = node;
            while (lookedUpNode.Parent != null)
            {
                depth++;
                lookedUpNode = lookedUpNode.Parent;
            }

            if (depth > maxDepth)
            {
                deepestNode = node.Value;
                maxDepth = depth;
            }
        }

        return deepestNode;
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
