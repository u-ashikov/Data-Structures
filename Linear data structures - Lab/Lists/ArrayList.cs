using System;

public class ArrayList<T>
{
    private const int InitialCapacity = 2;

    private T[] elements;

    private int count;

    public ArrayList()
    {
        this.elements = new T[InitialCapacity];
    }

    public int Count
    {
        get
        {
            return this.count;
        }
        private set
        {
            this.count += value;
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.elements[index];
        }

        set
        {
            if (index < 0 || index >= this.count)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.elements[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.count >= this.elements.Length)
        {
            this.Resize();
        }

        this.elements[this.count++] = item;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.count)
        {
            throw new ArgumentOutOfRangeException();
        }

        var removedElement = this.elements[index];

        this.MoveElementsToLeft(index);

        this.count--;

        if (this.count <= this.elements.Length / 4)
        {
            this.Shrink();
        }

        return removedElement;
    }

    private void Resize()
    {
        var oldArray = this.elements;
        var newArray = new T[this.count * 2];
        oldArray.CopyTo(newArray, 0);
        this.elements = newArray;
    }

    private void MoveElementsToLeft(int index)
    {
        for (int i = index; i < this.count-1; i++)
        {
            this.elements[i] = this.elements[i + 1];
        }
    }

    private void Shrink()
    {
        var newArray = new T[this.elements.Length / 2];

        for (int i = 0; i < this.count; i++)
        {
            newArray[i] = this.elements[i];
        }

        this.elements = newArray;
    }
}
