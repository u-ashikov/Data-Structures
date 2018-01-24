using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private Node root;

    public void Insert(T element)
    {
        if (this.root == null)
        {
            this.root = new Node(element);
            return;
        }

        this.InsertRecursively(root, element);
    }

    private void InsertRecursively(Node node, T element)
    {
        var compareResult = element.CompareTo(node.Value);

        if (compareResult < 0)
        {
            //left

            if (node.Left != null)
            {
                this.InsertRecursively(node.Left, element);
            }
            else
            {
                node.Left = new Node(element);
            }
        }
        else if (compareResult > 0)
        {
            //right

            if (node.Right != null)
            {
                this.InsertRecursively(node.Right, element);
            }
            else
            {
                node.Right = new Node(element);
            }
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrderRecursively(this.root, action);
    }

    private void EachInOrderRecursively(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrderRecursively(node.Left, action);
        action(node.Value);
        this.EachInOrderRecursively(node.Right, action);
    }

    public bool Contains(T element)
    {
        if (this.root == null)
        {
            return false;
        }

        var node = this.root;

        while (node != null)
        {
            var compareResult = element.CompareTo(node.Value);

            if (compareResult < 0)
            {
                //left
                node = node.Left;
            }
            else if (compareResult > 0)
            {
                //right
                node = node.Right;
            }
            else
            {
                //found
                break;
            }
        }

        return node != null;
    }

    public BinarySearchTree<T> Search(T element)
    {
        BinarySearchTree<T> bst = new BinarySearchTree<T>();

        var searchedNode = this.FindElement(root, element);

        this.GetSubTree(searchedNode, bst);

        return bst;
    }

    private void GetSubTree(Node node, BinarySearchTree<T> bst)
    {
        if (node == null)
        {
            return;
        }

        bst.Insert(node.Value);
        this.GetSubTree(node.Left, bst);
        this.GetSubTree(node.Right, bst);
    }

    private Node FindElement(Node node, T element)
    {
        while (node != null)
        {
            var compareResult = element.CompareTo(node.Value);

            if (compareResult < 0)
            {
                //left
                node = node.Left;
            }
            else if (compareResult > 0)
            {
                //right
                node = node.Right;
            }
            else
            {
                //found
                break;
            }
        }

        return node;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var result = new List<T>();

        this.GetRange(startRange, endRange, result, this.root);

        return result;
    }

    private void GetRange(T startRange, T endRange, IList<T> result, Node node)
    {
        if (node == null)
        {
            return;
        }

        var compareLow = node.Value.CompareTo(startRange);
        var compareHigh = node.Value.CompareTo(endRange);

        if (compareLow > 0)
        {
            this.GetRange(startRange, endRange, result, node.Left);
        }

        if (compareLow >= 0 && compareHigh <= 0)
        {
            result.Add(node.Value);
        }

        if (compareHigh < 0)
        {
            this.GetRange(startRange, endRange, result, node.Right);
        }
    }

    public int Count()
    {
        var list = new List<T>();

        this.EachInOrder(list.Add);

        return list.Count;
    }

    public void Delete(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        this.root = this.Delete(element, this.root);
    }

    private Node Delete(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            node.Left = this.Delete(element, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(element, node.Right);
        }
        else
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            if (node.Left == null)
            {
                return node.Right;
            }

            Node temp = node;
            node = this.FindMin(temp.Right);
            node.Right = this.DeleteMin(temp.Right);
            node.Left = temp.Left;

        }

        return node;
    }

    private Node FindMin(Node node)
    {
        if (node.Left == null)
        {
            return node;
        }

        return this.FindMin(node.Left);
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);

        return node;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        Node current = this.root;
        Node parent = null;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        Node node = this.root;
        Node parent = null;

        while (node.Right != null)
        {
            parent = node;
            node = node.Right;
        }

        if (parent == null)
        {
            this.root = node.Left;
        }
        else
        {
            parent.Right = null;
        }
    }

    public int Rank(T element)
    {
        if (this.root == null)
        {
            return 0;
        }

        int rank = 0;

        this.Rank(this.root, element, ref rank);

        return rank;
    }

    private void Rank(Node node, T element, ref int rank)
    {
        if (node == null)
        {
            return;
        }

        var compareResult = element.CompareTo(node.Value);

        if (compareResult > 0)
        {
            rank++;
        }

        this.Rank(node.Left, element, ref rank);
        this.Rank(node.Right, element, ref rank);
    }

    public T Select(int rank)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        var node = this.root;
        var result = new Stack<Node>();

        this.Select(result, node, rank);

        return result.Pop().Value;
    }

    private void Select(Stack<Node> result, Node node, int rank)
    {
        if (node == null)
        {
            return;
        }

        if (this.Rank(node.Value) == rank)
        {
            result.Push(node);
        }

        this.Select(result, node.Left, rank);
        this.Select(result, node.Right, rank);
    }

    public T Floor(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        if (this.root.Value.CompareTo(element) == 0)
        {
            return this.root.Value;
        }

        var node = this.root;
        var result = new Stack<T>();

        while (node != null)
        {
            var compareResult = element.CompareTo(node.Value);

            if (compareResult <= 0)
            {
                //left
                node = node.Left;
            }
            else if (compareResult > 0)
            {
                //right
                result.Push(node.Value);
                node = node.Right;
            }
        }

        return result.Pop();
    }

    public T Ceiling(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        if (this.root.Value.CompareTo(element) == 0)
        {
            return this.root.Value;
        }

        var node = this.root;

        var result = new Stack<T>();

        while (node != null)
        {
            var compareResult = element.CompareTo(node.Value);

            if (compareResult >= 0)
            {
                node = node.Right;
            }
            else if (compareResult < 0)
            {
                result.Push(node.Value);
                node = node.Left;
            }
        }

        return result.Pop();
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        //Console.WriteLine(bst.Contains(100));

        //var range = bst.Range(5,10);

        //bst.DeleteMin();

        //Console.WriteLine(bst.Count());

        //bst.DeleteMax();

        //bst.EachInOrder(Console.WriteLine);

        //Console.WriteLine(bst.Rank(5));

        //bst.Ceiling(7);

        //bst.Floor(4);

        //bst.Select(3);

        //10,5,8,9,3,1,4,37,39,45

        //bst.Delete(4);
        bst.Delete(3);

        //Console.WriteLine();
    }
}