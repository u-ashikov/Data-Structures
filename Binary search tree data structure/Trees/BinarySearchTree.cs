using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrderRecursive(this.root, action);
    }

    private void EachInOrderRecursive(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrderRecursive(node.Left,action);
        action.Invoke(node.Value);
        this.EachInOrderRecursive(node.Right, action);
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }

        var node = this.root;

        this.InsertRecursive(node, value);
    }

    private void InsertRecursive(Node node, T value)
    {
        var result = node.Value.CompareTo(value);

        if (result < 0)
        {
            //right
            if (node.Right != null)
            {
                node = node.Right;
                this.InsertRecursive(node, value);
            }
            else
            {
                node.Right = new Node(value);
            }
        }
        else if (result > 0)
        {
            //left
            if (node.Left != null)
            {
                node = node.Left;
                this.InsertRecursive(node, value);
            }
            else
            {
                node.Left = new Node(value);
            }
        }
    }

    public bool Contains(T value)
    {
        if (this.root == null)
        {
            return false;
        }

        var node = this.root;

        while (node != null)
        {
            var result = node.Value.CompareTo(value);

            if (result > 0)
            {
                //left
                node = node.Left;
            }
            else if (result < 0)
            {
                //right
                node = node.Right;
            }
            else
            {
                break;
            }
        }

        return node != null;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if(parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        BinarySearchTree<T> searchedTree = new BinarySearchTree<T>();

        var node = this.root;

        while (node != null)
        {
            var result = node.Value.CompareTo(item);

            if (result < 0)
            {
                //right
                node = node.Right;
            }
            else if (result > 0)
            {
                //left
                node = node.Left;
            }
            else
            {
                //found
                return this.GetSubTree(node,searchedTree);
            }
        }

        return null;
    }

    private BinarySearchTree<T> GetSubTree(Node node,BinarySearchTree<T> searchedTree)
    {
        if (node == null)
        {
            return null;
        }

        searchedTree.Insert(node.Value);

        this.GetSubTree(node.Left, searchedTree);
        this.GetSubTree(node.Right, searchedTree);

        return searchedTree;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        List<T> result = new List<T>();

        this.RangeRecursively(this.root, result, startRange, endRange);

        return result;
    }

    private void RangeRecursively(Node node, IList<T> result, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = node.Value.CompareTo(startRange);
        int nodeInHigherRange = node.Value.CompareTo(endRange);

        if (nodeInLowerRange > 0)
        {
            this.RangeRecursively(node.Left, result, startRange, endRange);
        }
        
        if(nodeInLowerRange >=0 && nodeInHigherRange <= 0)
        {
            result.Add(node.Value);
        }

        if (nodeInHigherRange < 0)
        {
            this.RangeRecursively(node.Right, result, startRange, endRange);
        }
    }

    private class Node
    {
        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);
        bst.Insert(3);

        // Act
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);
    }
}
