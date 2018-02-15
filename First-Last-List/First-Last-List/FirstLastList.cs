using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private LinkedList<T> elements;

    private OrderedBag<LinkedListNode<T>> orderedElements;

    private OrderedBag<LinkedListNode<T>> reversedOrderedElements;

    public FirstLastList()
    {
        this.elements = new LinkedList<T>();
        this.orderedElements = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.reversedOrderedElements = new OrderedBag<LinkedListNode<T>>((x, y) => -x.Value.CompareTo(y.Value));
    }

    public int Count => this.elements.Count;

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element);

        this.elements.AddLast(node);
        this.orderedElements.Add(node);
        this.reversedOrderedElements.Add(node);
    }

    public void Clear()
    {
        this.elements.Clear();
        this.orderedElements.Clear();
        this.reversedOrderedElements.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        this.CheckRangeValidity(count);

        var current = this.elements.First;

        while (count > 0)
        {
            yield return current.Value;
            current = current.Next;
            count--;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        this.CheckRangeValidity(count);

        var current = this.elements.Last;

        while (count > 0)
        {
            yield return current.Value;
            current = current.Previous;
            count--;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        this.CheckRangeValidity(count);

        foreach (var element in this.reversedOrderedElements)
        {
            if (count <= 0)
            {
                break;
            }

            yield return element.Value;
            count--;
        }
    }

    public IEnumerable<T> Min(int count)
    {
        this.CheckRangeValidity(count);

        foreach (var element in this.orderedElements)
        {
            if (count <= 0)
            {
                break;
            }

            yield return element.Value;
            count--;
        }
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);
        var elementsToRemove = this.orderedElements.Range(node, true, node, true);

        foreach (LinkedListNode<T> el in elementsToRemove)
        {
            this.elements.Remove(el);
        }

        int removedElements = this.orderedElements.RemoveAllCopies(node);
        this.reversedOrderedElements.RemoveAllCopies(node);

        return removedElements;
    }

    private void CheckRangeValidity(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
