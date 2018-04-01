using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organization : IOrganization
{
    private Dictionary<string, LinkedList<Person>> peopleByName;

    private Dictionary<int,Person> people;

    private Dictionary<int, LinkedList<Person>> peopleByNameSize;

    private int index;

    public Organization()
    {
        this.peopleByName = new Dictionary<string, LinkedList<Person>>();
        this.people = new Dictionary<int, Person>();
        this.peopleByNameSize = new Dictionary<int, LinkedList<Person>>();
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var p in this.people.Values)
        {
            yield return p;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count => this.people.Count;

    public bool Contains(Person person)
    {
        return this.peopleByName.ContainsKey(person.Name);
    }

    public bool ContainsByName(string name)
    {
        return this.peopleByName.ContainsKey(name);
    }

    public void Add(Person person)
    {
        this.people.Add(this.index++, person);

        if (!this.peopleByNameSize.ContainsKey(person.Name.Length))
        {
            this.peopleByNameSize.Add(person.Name.Length, new LinkedList<Person>());
        }

        this.peopleByNameSize[person.Name.Length].AddLast(new LinkedListNode<Person>(person));

        if (!this.peopleByName.ContainsKey(person.Name))
        {
            this.peopleByName.Add(person.Name, new LinkedList<Person>());
        }

        this.peopleByName[person.Name].AddLast(new LinkedListNode<Person>(person));
    }

    public Person GetAtIndex(int index)
    {
        if (!this.people.ContainsKey(index))
        {
            throw new IndexOutOfRangeException();
        }

        return this.people[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (!this.peopleByName.ContainsKey(name))
        {
            return new List<Person>();
        }

        return this.peopleByName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        return this.people.Values.Take(count);
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        var result = new List<Person>();
        var keys = this.peopleByNameSize.Keys.Where(k => k >= minLength && k <= maxLength);

        foreach (var key in keys)
        {
            result.AddRange(this.peopleByNameSize[key]);
        }

        return result;
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!this.peopleByNameSize.ContainsKey(length))
        {
            throw new ArgumentException();
        }

        return this.peopleByNameSize[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.people.Values;
    }
}