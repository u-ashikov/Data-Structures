using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;

    private const double GrowFactor = 0.75;

    private double FillFactor => (double)(this.Count+1) / this.Capacity;

    private LinkedList<KeyValue<TKey, TValue>>[] hashTable;

    public HashTable(int capacity = DefaultCapacity)
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[capacity];
    }

    public int Count { get; private set; }

    public int Capacity => this.hashTable.Length;

    public void Add(TKey key, TValue value)
    {
        var hashCode = key.GetHashCode();

        if (this.FillFactor >= GrowFactor)
        {
            this.Grow();
        }

        var slotIndex = Math.Abs(hashCode) % this.hashTable.Length;

        if (this.hashTable[slotIndex] == null)
        {
            this.hashTable[slotIndex] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        this.CheckDuplicateKey(key, slotIndex);

        var kvp = new KeyValue<TKey, TValue>(key, value);
        this.hashTable[slotIndex].AddLast(kvp);
        this.Count++;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        if (!this.ContainsKey(key))
        {
            this.Add(key, value);
            return false;
        }

        var kvp = this.Find(key);
        kvp.Value = value;

        return true;
    }

    public TValue Get(TKey key)
    {
        var kvp = this.Find(key);

        if (kvp == null)
        {
            throw new KeyNotFoundException();
        }

        return kvp.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (!this.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            return this.Get(key);
        }
        set
        {
            if (!this.ContainsKey(key))
            {
                this.Add(key, value);
            }
            else
            {
                this.AddOrReplace(key, value);
            }
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var kvp = this.Find(key);

        if (kvp == null)
        {
            value = default(TValue);
            return false;
        }

        value = kvp.Value;

        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var index = Math.Abs(key.GetHashCode()) % this.Capacity;

        var list = this.hashTable[index];

        if (list != null)
        {
            foreach (var item in list)
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key) => this.Find(key) != null;

    public bool Remove(TKey key)
    {
        var index = Math.Abs(key.GetHashCode()) % this.Capacity;

        var list = this.hashTable[index];

        if (list != null)
        {
            foreach (var item in list)
            {
                if (item.Key.Equals(key))
                {
                    list.Remove(item);
                    this.Count--;
                    return true;
                }
            }
        }

        return false;
    }

    public void Clear()
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys =>
        this.hashTable
        .Where(l=>l != null)
        .SelectMany(l => l.Select(kvp => kvp.Key));

    public IEnumerable<TValue> Values =>
        this.hashTable.Where(l => l != null).SelectMany(l => l.Select(kvp => kvp.Value));

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var list in this.hashTable)
        {
            if (list != null)
            {
                foreach (var element in list)
                {
                    yield return element;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private void CheckDuplicateKey(TKey key, int slotIndex)
    {
        foreach (var item in this.hashTable[slotIndex])
        {
            if (item.Key.Equals(key))
            {
                throw new ArgumentException();
            }
        }
    }

    private void Grow()
    {
        var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);

        foreach (var list in this.hashTable)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    newHashTable.Add(item.Key, item.Value);
                }
            }
        }

        this.hashTable = newHashTable.hashTable;
        this.Count = newHashTable.Count;
    }
}
