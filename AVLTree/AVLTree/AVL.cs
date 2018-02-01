using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);

        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        this.UpdateHeight(node);
        node = this.Balance(node);

        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        var balance = this.GetHeight(node.Left) - this.GetHeight(node.Right);

        if (balance > 1)
        {
            //right
            var childBalance = this.GetHeight(node.Left.Left) - this.GetHeight(node.Left.Right);

            if (childBalance < 0)
            {
                node.Left = this.RotateLeft(node.Left);
            }

            node = this.RotateRight(node);
        }
        else if (balance < -1)
        {
            //left
            var childBalance = this.GetHeight(node.Right.Left) - this.GetHeight(node.Right.Right);

            if (childBalance > 0)
            {
                node.Right = this.RotateRight(node.Right);
            }

            node = this.RotateLeft(node);
        }

        this.UpdateHeight(node);

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(this.GetHeight(node.Left), this.GetHeight(node.Right)) + 1;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);

        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private int GetHeight(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> rightChild = node.Right;
        node.Right = rightChild.Left;
        rightChild.Left = node;

        this.UpdateHeight(node);

        return rightChild;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> leftChild = node.Left;
        node.Left = leftChild.Right;
        leftChild.Right = node;

        this.UpdateHeight(node);

        return leftChild;
    }
}
