using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    private Node head;

    private Node tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new Node(item);

        if (this.Count == 0)
        {
            this.head = this.tail = node;
        }
        else
        {
            node.Next = this.head;
            this.head = node;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        var node = new Node(item);

        if (this.Count == 0)
        {
            this.head = this.tail = node;
        }
        else
        {
            this.tail.Next = node;
            this.tail = node;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        Node removedElement = this.head;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = removedElement.Next;
        }

        this.head = removedElement.Next;
        this.Count--;

        return removedElement.Value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        Node removedElement = null;

        if (this.Count == 1)
        {
            removedElement = this.tail;
            this.head = this.tail = null;
            this.Count--;

            return removedElement.Value;
        }

        var currentNode = this.head;
        while (currentNode.Next != this.tail)
        {
            currentNode = currentNode.Next;
        }

        removedElement = currentNode.Next;
        currentNode.Next = null;
        this.tail = currentNode;       
        this.Count--;

        return removedElement.Value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.head;

        while (currentNode.Next != null)
        {
            yield return currentNode.Value;

            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Node
    {
        public T Value { get; set; }

        public Node Next { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }
}
