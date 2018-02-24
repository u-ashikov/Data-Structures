using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this.root, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        var node = this.root;

        if (node == null || node.max < lo)
        {
            return null;
        }

        while (node != null)
        {
            if (node.interval.Intersects(lo,hi))
            {
                return node.interval;
            }

            if (lo > node.interval.Lo)
            {
                node = node.right;
            }
            else if (lo < node.interval.Lo)
            {
                node = node.left;
            }
        }

        if (node == null)
        {
            return null;
        }

        return node.interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        var result = new List<Interval>();

        this.SearchRecursively(this.root, result,lo,hi);

        return result;
    }

    private void SearchRecursively(Node node, List<Interval> result,double lo, double hi)
    {
        if (node == null)
        {
            return;
        }

        var goLeft = node.left != null && node.interval.Lo > lo;
        var goRight = node.right != null && node.interval.Lo < hi;

        if (goLeft)
        {
            SearchRecursively(node.left, result, lo, hi);
        }

        if (node != null && node.interval.Intersects(lo, hi))
        {
            result.Add(node.interval);
        }

        if (goRight)
        {
            SearchRecursively(node.right, result, lo, hi);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.left, action);
        action(node.interval);
        EachInOrder(node.right, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        this.UpdateMax(node, hi);

        if (cmp < 0)
        {
            node.left = Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = Insert(node.right, lo, hi);
        }
        
        return node;
    }

    private void UpdateMax(Node node, double hi)
    {
        if (node != null && node.max < hi)
        {
            node.max = hi;
        }
    }
}
