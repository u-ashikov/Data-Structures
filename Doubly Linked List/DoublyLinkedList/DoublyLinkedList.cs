using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private Node head;

    private Node tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node(element);
        }
        else 
        {
            var oldHead = this.head;
            this.head = new Node(element);
            this.head.Next = oldHead;
            oldHead.Previous = this.head;

            if (this.Count == 1)
            {
                this.tail = oldHead;
            }
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node(element);
        }
        else
        {
            var oldTail = this.tail;
            this.tail = new Node(element);
            oldTail.Next = this.tail;
            this.tail.Previous = oldTail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        Node removedNode = this.head;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head.Next.Previous = null;
            this.head = removedNode.Next;
        }

        this.Count--;

        return removedNode.Value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedNode = this.tail;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.tail.Previous.Next = null;
            this.tail = this.tail.Previous;
        }

        this.Count--;

        return removedNode.Value;
    }

    public void ForEach(Action<T> action)
    {
        foreach (var item in this)
        {
            action.Invoke(item);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var node = this.head;
        while (node != null)
        {
            yield return node.Value;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];

        int index = 0;

        foreach (var item in this)
        {
            arr[index] = item;
            index++;
        }

        return arr;
    }

    private class Node
    {
        public T Value { get; private set; }

        public Node Next { get; set; }

        public Node Previous { get; set; }

        public Node(T input)
        {
            this.Value = input;
        }
    }
}
