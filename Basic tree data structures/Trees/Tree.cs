using System;
using System.Collections.Generic;

public class Tree<T>
{
    public T Value { get; private set; }

    public Tree<T>[] Children { get; private set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = children;
    }

    public void Print(int indent = 0)
    {
        this.PrintRecursively(this,indent);
    }

    private void PrintRecursively(Tree<T> node, int indent)
    {
        Console.WriteLine($"{new string(' ',indent)}{node.Value}");

        foreach (Tree<T> child in node.Children)
        {
            this.PrintRecursively(child, indent+2);
        }
    }

    public void Each(Action<T> action)
    {
        this.EachRecursively(this, action);
    }

    private void EachRecursively(Tree<T> node, Action<T> action)
    {
        action.Invoke(node.Value);

        foreach (var child in node.Children)
        {
            this.EachRecursively(child, action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();

        this.OrderDFSRecursively(this, result);

        return result;
    }

    private void OrderDFSRecursively(Tree<T> node, List<T> result)
    {
        foreach (var child in node.Children)
        {
            this.OrderDFSRecursively(child, result);
        }

        result.Add(node.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var queue = new Queue<Tree<T>>();
        queue.Enqueue(this);

        var result = new List<T>();

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node.Value);

            foreach (var child in node.Children)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }
}
