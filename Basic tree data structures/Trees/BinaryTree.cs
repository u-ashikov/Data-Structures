using System;

public class BinaryTree<T>
{
    public T Value { get; private set; }

    public BinaryTree<T> Left { get; private set; }

    public BinaryTree<T> Right { get; private set; }

    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.Left = leftChild;
        this.Right = rightChild;
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', 2 * indent)}{this.Value}");

        if (this.Left != null)
        {
            this.Left.PrintIndentedPreOrder(indent + 1);
        }

        if (this.Right != null)
        {
            this.Right.PrintIndentedPreOrder(indent + 1);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if (this.Left != null)
        {
            this.Left.EachInOrder(action);
        }

        action.Invoke(this.Value);

        if (this.Right != null)
        {
            this.Right.EachInOrder(action);
        }
    }

    public void EachPostOrder(Action<T> action)
    {
        if (this.Left != null)
        {
            this.Left.EachPostOrder(action);
        }

        if (this.Right != null)
        {
            this.Right.EachPostOrder(action);
        }

        action.Invoke(this.Value);
    }
}
