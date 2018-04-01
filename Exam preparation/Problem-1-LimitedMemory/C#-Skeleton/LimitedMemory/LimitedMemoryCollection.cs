using System.Collections.Generic;
using System.Collections;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private Dictionary<K, LinkedListNode<Pair<K,V>>> elements;

        private LinkedList<Pair<K,V>> requests;

        public LimitedMemoryCollection(int capacity)
        {
            this.elements = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
            this.requests = new LinkedList<Pair<K, V>>();
            this.Capacity = capacity;
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            return this.requests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count => this.elements.Count;

        public void Set(K key, V value)
        {
            var kvp = new Pair<K,V>(key, value);

            if (this.Count < this.Capacity)
            {
                if (!this.elements.ContainsKey(key))
                {
                    this.elements.Add(key, new LinkedListNode<Pair<K, V>>(kvp));
                    this.requests.AddLast(kvp);
                }
                else
                {
                    this.elements[key] = new LinkedListNode<Pair<K, V>>(kvp);
                }
            }
            else if (this.Count == this.Capacity)
            {
                var removed = this.requests.First;
                this.requests.RemoveFirst();
                this.elements.Remove(removed.Value.Key);
                this.elements.Add(key,new LinkedListNode<Pair<K, V>>(kvp));
                this.requests.AddLast(kvp);
            }
        }

        public V Get(K key)
        {
            if (!this.elements.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            this.UpdatePriority(key);
            return this.elements[key].Value.Value;
        }

        private void UpdatePriority(K key)
        {
            var pair = new Pair<K, V>(key, this.elements[key].Value.Value);
            this.requests.Remove(pair);
            this.requests.AddLast(pair);
        }
    }
}
