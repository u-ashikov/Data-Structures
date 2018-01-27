namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T> where T:IComparable
    {
        private Node root;

        private IDictionary<T, Node> elements;

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.elements = new Dictionary<T, Node>();
            elements[root] = this.root;
        }

        public void Add(T element, T child)
        {
            if (!this.elements.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.elements.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            Node newNode = new Node(child);

            var parent = this.elements[element];
            newNode.Parent = parent;
            parent.Children.Add(newNode);

            this.elements[child] = newNode;
        }

        public int Count => this.elements.Count;

        public void Remove(T element)
        {
            if (!this.elements.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.root.Value.CompareTo(element) == 0)
            {
                throw new InvalidOperationException();
            }

            var elementToRemove = this.elements[element];
            var parent = elementToRemove.Parent;

            if (elementToRemove.Children.Any())
            {
                foreach (var child in elementToRemove.Children)
                {
                    child.Parent = parent;
                    this.elements[elementToRemove.Parent.Value].Children.Add(child);
                }

                elementToRemove.Children.Clear();
            }

            if (parent != null)
            {
                parent.Children.Remove(elementToRemove);
            }

            this.elements.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.elements.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            return this.elements[item].Children.Select(c=>c.Value);
        }

        public T GetParent(T item)
        {
            if (!this.elements.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var searchedElement = this.elements[item];

            if (searchedElement.Parent == null)
            {
                return default(T);
            }

            return searchedElement.Parent.Value;
        }

        public bool Contains(T value) => this.elements.ContainsKey(value);

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            return this.elements.Keys.Intersect(other);
        } 

        public IEnumerator<T> GetEnumerator()
        {
            var result = new Queue<Node>();
            result.Enqueue(this.root);

            while (result.Count > 0)
            {
                var node = result.Dequeue();
                yield return node.Value;

                foreach (var child in node.Children)
                {
                    result.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public T Value { get; set; }

            public Node Parent { get; set; }

            public ICollection<Node> Children { get; set; }

            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node>();
            }
        }
    }
}