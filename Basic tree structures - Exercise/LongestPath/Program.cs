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
        Tree<int> deepestNode = GetDeepestNode();
        PrintLongestPath(deepestNode);
    }

    private static void PrintLongestPath(Tree<int> deepestNode)
    {
        var result = new Stack<int>();

        var node = deepestNode;
        while (node.Parent != null)
        {
            result.Push(node.Value);
            node = node.Parent;
        }

        result.Push(node.Value);

        Console.WriteLine($"Longest path: {string.Join(" ",result)}");
    }

    private static Tree<int> GetDeepestNode()
    {
        int deepestNode = 0;
        int maxDepth = 0;
        Tree<int> searchedNode = null;

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
                searchedNode = node;
                maxDepth = depth;
            }
        }

        return searchedNode;
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