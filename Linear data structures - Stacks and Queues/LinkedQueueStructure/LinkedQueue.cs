using System;

public class LinkedQueue<T>
{
    private Node head;

    private Node tail;

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        var node = new Node(item);

        if (this.Count == 0)
        {
            this.head = this.tail = node;
        }
        else
        {
            var oldtail = this.tail;
            this.tail = node;
            oldtail.Next = this.tail;
            this.tail.Prev = oldtail;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        Node oldHead = this.head;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = oldHead.Next;
            this.head.Prev = null;
        }

        this.Count--;

        return oldHead.Value;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];
        int index = 0;

        var node = this.head;
        while (node != null)
        {
            arr[index++] = node.Value;
            node = node.Next;
        }

        return arr;
    }

    private class Node
    {
        public T Value { get; private set; }

        public Node Next { get; set; }

        public Node Prev { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }
}
