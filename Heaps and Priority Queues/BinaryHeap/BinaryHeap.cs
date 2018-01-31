using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get => this.heap.Count;
    }

    public void Insert(T item)
    {
        this.heap.Add(item);

        int itemIndex = this.Count - 1;
        int parentIndex = (itemIndex - 1) / 2;

        this.HeapifyUp(itemIndex, parentIndex);
    }

    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedElement = this.heap[0];

        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);

        this.HeapifyDown(0);

        return removedElement;
    }

    private void HeapifyUp(int childIndex, int parentIndex)
    {
        if (parentIndex < 0)
        {
            return;
        }

        var element = this.heap[childIndex];
        var parent = this.heap[parentIndex];

        if (element.CompareTo(parent) > 0)
        {
            this.Swap(childIndex, parentIndex);
            this.HeapifyUp(parentIndex, parentIndex - 1);
        }
    }

    private void HeapifyDown(int parentIndex)
    {
        if (parentIndex >= this.Count / 2)
        {
            return;
        }

        int childIndex = (2 * parentIndex) + 1;
        int rightChildIndex = childIndex + 1;
        var leftChild = this.heap[childIndex];

        if (rightChildIndex < this.Count && this.heap[rightChildIndex].CompareTo(leftChild) > 0)
        {
            childIndex++;
        }

        int compare = this.heap[parentIndex].CompareTo(this.heap[childIndex]);

        if (compare < 0)
        {
            this.Swap(childIndex, parentIndex);
            this.HeapifyDown(parentIndex++);
        }
    }

    private void Swap(int childIndex, int parentIndex)
    {
        var oldParent = this.heap[parentIndex];
        this.heap[parentIndex] = this.heap[childIndex];
        this.heap[childIndex] = oldParent;
    }
}
