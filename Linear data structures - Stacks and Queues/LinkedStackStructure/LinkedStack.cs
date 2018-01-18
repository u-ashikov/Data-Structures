using System;

public class LinkedStack<T>
{
    private Node head;

    public int Count { get; private set; }

    public void Push(T element)
    {
        var node = new Node(element);

        if (this.Count == 0)
        {
            this.head = node;
        }
        else
        {
            var oldHead = this.head;
            this.head = node;
            this.head.Next = oldHead;
        }

        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedNode = this.head;
        this.head = removedNode.Next;

        this.Count--;

        return removedNode.Value;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];
        var index = 0;

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

        public Node(T value)
        {
            this.Value = value;
        }
    }
}
