using System;

public class ArrayStack<T>
{
    private const int InitialCapacity = 16;

    private T[] elements;

    public int Count { get; private set; }

    public int Capacity
    {
        get => this.elements.Length;
    }

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Push(T element)
    {
        if (this.Count == this.elements.Length)
        {
            this.Grow();
        }

        this.elements[this.Count++] = element;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var poppedElement = this.elements[this.Count-1];

        this.Count--;

        return poppedElement;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];

        for (int i = 0; i < this.Count; i++)
        {
            arr[i] = this.elements[this.Count-1-i];
        }

        return arr;
    }

    private void Grow()
    {
        var newArray = new T[this.Count * 2];

        for (int i = 0; i < this.Count; i++)
        {
            newArray[i] = this.elements[i];
        }

        this.elements = newArray;
    }
}
