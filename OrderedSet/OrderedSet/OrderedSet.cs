namespace OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T> where T:IComparable
    {
        private Node<T> root;

        public int Count { get; private set; }

        public void Add(T element)
        {
            var node = new Node<T>(element);

            if (this.root == null)
            {
                this.root = node;
                this.Count++;
            }
            else
            {
                if (!this.Contains(element))
                {
                    this.InsertRecursively(element, this.root);
                    this.Count++;
                }
            }
        }

        private void InsertRecursively(T element, Node<T> node)
        {
            if (element.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node<T>(element);
                    node.Left.Parent = node;
                    return;
                }

                this.InsertRecursively(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                if (node.Right == null)
                {
                    node.Right = new Node<T>(element);
                    node.Right.Parent = node;
                    return;
                }

                this.InsertRecursively(element, node.Right);
            }
        }

        public void Remove(T element)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException();
            }

            var node = this.Find(element,this.root);

            if (node.Parent == null)
            {
                this.root = node.Right;
                this.root.Left = node.Left;
            }
            else if (node.Parent.Left == node)
            {
                node.Parent.Left = node.Right;
            }
            else if (node.Parent.Right == node)
            {
                node.Parent.Right = node.Right;
            }

            this.Count--;
        }

        private Node<T> Find(T element, Node<T> node)
        {
            if (element.CompareTo(node.Value) > 0)
            {
                return this.Find(element, node.Right);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                return this.Find(element, node.Left);
            }

            return node;
        }

        public bool Contains(T element)
        {
            var current = this.root;

            while (current != null)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else if (element.CompareTo(current.Value) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var result = new Queue<T>();
            this.InOrderTraverse(root, result);

            foreach (var item in result)
            {
                yield return item;
            }
        }

        private void InOrderTraverse(Node<T> node, Queue<T> result)
        {
            if (node == null)
            {
                return;
            }

            this.InOrderTraverse(node.Left,result);
            result.Enqueue(node.Value);
            this.InOrderTraverse(node.Right,result);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
