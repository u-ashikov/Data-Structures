using System;
using System.Collections;
using System.Collections.Generic;

class ReversedList<T> : IEnumerable<T>
{
    private T[] elements;

    private int count;

    private const int InitialCapacity = 2;

    public ReversedList(int capacity = InitialCapacity)
    {
        this.elements = new T[InitialCapacity];
    }

    public int Count => this.count;

    public int Capacity => this.elements.Length;

    public void Add(T item)
    {
        if (this.count >= this.elements.Length)
        {
            this.Resize();
        }

        this.elements[this.count] = item;
        this.count++;
    }

    public T RemoveAt(int index)
    {
        this.CheckIndexValidity(index);

        var removedElement = this.elements[this.count - 1 - index];
        this.ShiftElements(this.count - 1 - index);
        this.count--;

        return removedElement;
    }

    private void ShiftElements(int index)
    {
        for (int i = index; i < this.count - 1; i++)
        {
            this.elements[i] = this.elements[i + 1];
        }
    }

    public T this[int index]
    {
        get
        {
            this.CheckIndexValidity(index);

            return this.elements[this.count - 1 - index];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.count - 1; i >= 0; i--)
        {
            yield return this.elements[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void Resize()
    {
        var newArray = new T[this.count * 2];
        this.elements.CopyTo(newArray, 0);
        this.elements = newArray;
    }

    private void CheckIndexValidity(int index)
    {
        if (index < 0 || index >= this.count)
        {
            throw new IndexOutOfRangeException();
        }
    }
}