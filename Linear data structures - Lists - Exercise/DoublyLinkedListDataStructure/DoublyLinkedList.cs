using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        throw new NotImplementedException();
    }

    public void AddLast(T element)
    {
        throw new NotImplementedException();
    }

    public T RemoveFirst()
    {
        throw new NotImplementedException();
    }

    public T RemoveLast()
    {
        throw new NotImplementedException();
    }

    public void ForEach(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public T[] ToArray()
    {
        throw new NotImplementedException();
    }
}
