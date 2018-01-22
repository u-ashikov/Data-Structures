using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static IDictionary<int, Tree<int>> nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        int sum = int.Parse(Console.ReadLine());
        Tree<int> root = nodes.Values.FirstOrDefault(n => n.Parent == null);
        IList<Tree<int>> leaves = GetPathWithGivenSum(root, sum);
        PrintPaths(leaves, sum);
    }

    private static void PrintPaths(IList<Tree<int>> leaves, int sum)
    {
        Console.WriteLine($"Paths of sum {sum}:");

        foreach (var leaf in leaves)
        {
            var result = new Stack<int>();

            var node = leaf;
            while (node.Parent != null)
            {
                result.Push(node.Value);
                node = node.Parent;
            }

            result.Push(node.Value);

            Console.WriteLine(string.Join(" ", result));
        }
    }

    private static IList<Tree<int>> GetPathWithGivenSum(Tree<int> node, int sum)
    {
        var leaves = new List<Tree<int>>();

        GetLeaves(leaves, node, 0, sum);

        return leaves;
    }

    private static void GetLeaves(List<Tree<int>> leaves, Tree<int> node, int currentSum, int searchedSum)
    {
        if (node == null)
        {
            return;
        }

        currentSum += node.Value;

        foreach (var child in node.Children)
        {
            GetLeaves(leaves, child, currentSum, searchedSum);
        }

        if (currentSum == searchedSum && !node.Children.Any())
        {
            leaves.Add(node);
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