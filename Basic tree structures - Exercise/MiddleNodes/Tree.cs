using System.Collections.Generic;

public class Tree<T>
{
    public T Value { get; private set; }

    public Tree<T> Parent { get; set; }

    public IList<Tree<T>> Children { get; private set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>();
        this.AddChildren(children);
    }

    private void AddChildren(Tree<T>[] children)
    {
        foreach (var child in children)
        {
            this.Children.Add(child);
        }
    }
}
