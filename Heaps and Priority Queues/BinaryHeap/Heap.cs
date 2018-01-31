using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        SortHeap(arr);
    }

    private static void SortHeap(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            Swap(0, i, arr);
            HeapifyDown(0, i, arr);
        }
    }

    private static void ConstructHeap(T[] arr)
    {
        HeapifyDown(0, arr.Length, arr);
    }

    private static void HeapifyDown(int parentIndex, int length, T[] heap)
    {
        if (parentIndex >= length / 2)
        {
            return;
        }

        int childIndex = (2 * parentIndex) + 1;
        int rightChildIndex = childIndex + 1;
        var leftChild = heap[childIndex];

        if (rightChildIndex < length && heap[rightChildIndex].CompareTo(leftChild) > 0)
        {
            childIndex++;
        }

        int compare = heap[parentIndex].CompareTo(heap[childIndex]);

        if (compare < 0)
        {
            Swap(childIndex, parentIndex, heap);
        }

        parentIndex++;

        HeapifyDown(parentIndex, length, heap);
    }

    private static void Swap(int childIndex, int parentIndex, T[] heap)
    {
        var oldParent = heap[parentIndex];
        heap[parentIndex] = heap[childIndex];
        heap[childIndex] = oldParent;
    }
}
