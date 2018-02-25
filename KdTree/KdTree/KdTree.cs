using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root => this.root;

    public bool Contains(Point2D point)
    {
        return this.ContainsRecursively(this.root,point,0);
    }

    private bool ContainsRecursively(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return false;
        }

        int cmp = this.FindCompareFactor(node, point, depth);

        if (cmp > 0)
        {
            return this.ContainsRecursively(node.Right, point, depth + 1);
        }
        else if (cmp < 0)
        {
            return this.ContainsRecursively(node.Left, point, depth + 1);
        }

        return true;
    }

    public void Insert(Point2D point)
    {
        var node = new Node(point);

        if (this.root == null)
        {
            this.root = node;
        }
        else
        {
            this.root = this.InsertRecursively(this.root,node,0);
        }
    }

    private Node InsertRecursively(Node node,Node newNode, int depth)
    {
        if (node == null)
        {
            return newNode;
        }

        var cmp = this.FindCompareFactor(node, newNode.Point, depth);

        if (cmp <= 0)
        {
            node.Left = this.InsertRecursively(node.Left, newNode, depth + 1);
        }
        else if (cmp >= 0)
        {
            node.Right = this.InsertRecursively(node.Right, newNode, depth + 1);
        }

        return node;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    private int FindCompareFactor(Node node, Point2D point, int depth)
    {
        var cmp = depth % 2;

        if (cmp == 0)
        {
            //compare by X
            cmp = point.X.CompareTo(node.Point.X);
        }
        else
        {
            //compare by Y
            cmp = point.Y.CompareTo(node.Point.Y);
        }

        return cmp;
    }
}
