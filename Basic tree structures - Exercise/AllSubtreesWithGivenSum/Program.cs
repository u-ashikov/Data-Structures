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
        IList<Tree<int>> subTrees = new List<Tree<int>>();
        GetSubTrees(root, subTrees);
        PrintPreOrderSubTrees(sum, subTrees);
    }

    private static void PrintPreOrderSubTrees(int sum, IList<Tree<int>> subTrees)
    {
        Console.WriteLine($"Subtrees of sum {sum}:");

        foreach (var tree in subTrees)
        {
            var treeSum = tree.Value;
            var result = new Queue<int>();
            result.Enqueue(tree.Value);
            GetSubTreeSum(tree, result);

            if (result.Sum() == sum)
            {
                Console.WriteLine(string.Join(" ", result));
            }
        }
    }

    private static void GetSubTreeSum(Tree<int> tree, Queue<int> result)
    {
        foreach (var child in tree.Children)
        {
            result.Enqueue(child.Value);

            GetSubTreeSum(child, result);
        }
    }

    private static void GetSubTrees(Tree<int> node, IList<Tree<int>> subTrees)
    {
        if (node == null)
        {
            return;
        }

        foreach (var child in node.Children)
        {
            GetSubTrees(child, subTrees);
        }

        subTrees.Add(node);
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
