using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> byEmail;

    private Dictionary<string, SortedSet<Person>> byNameAndTown;

    private Dictionary<string, SortedSet<Person>> byDomain;

    private OrderedDictionary<int, SortedSet<Person>> byAge;

    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> byTownAndAge;

    public PersonCollection()
    {
        this.byEmail = new Dictionary<string, Person>();

        this.byNameAndTown = new Dictionary<string, SortedSet<Person>>();

        this.byDomain = new Dictionary<string, SortedSet<Person>>();

        this.byAge = new OrderedDictionary<int, SortedSet<Person>>();

        this.byTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.byEmail.ContainsKey(email))
        {
            return false;
        }

        var person = new Person(email, name, age, town);

        this.byEmail.Add(email, person);
        
        if (!this.byNameAndTown.ContainsKey(town+name))
        {
            this.byNameAndTown.Add(town+name, new SortedSet<Person>());
        }

        this.byNameAndTown[town+name].Add(person);

        var domain = email.Split('@')[1];

        if (!this.byDomain.ContainsKey(domain))
        {
            this.byDomain.Add(domain, new SortedSet<Person>());
        }

        this.byDomain[domain].Add(person);

        if (!this.byAge.ContainsKey(age))
        {
            this.byAge.Add(age, new SortedSet<Person>());
        }

        this.byAge[age].Add(person);

        if (!this.byTownAndAge.ContainsKey(town))
        {
            this.byTownAndAge.Add(town, new OrderedDictionary<int, SortedSet<Person>>());
        }

        if (!this.byTownAndAge[town].ContainsKey(age))
        {
            this.byTownAndAge[town].Add(age, new SortedSet<Person>());
        }

        this.byTownAndAge[town][age].Add(person);

        return true;
    }

    public int Count => this.byEmail.Count;

    public Person FindPerson(string email)
    {
        if (this.byEmail.ContainsKey(email))
        {
            return this.byEmail[email];
        }

        return null;
    }

    public bool DeletePerson(string email)
    {
        if (this.byEmail.ContainsKey(email))
        {
            var person = this.byEmail[email];

            this.byEmail.Remove(email);

            this.byNameAndTown[person.Town+person.Name].Remove(person);

            this.byDomain[person.Email.Split('@')[1]].Remove(person);

            this.byAge[person.Age].Remove(person);

            this.byTownAndAge[person.Town][person.Age].Remove(person);

            return true;
        }

        return false;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (this.byDomain.ContainsKey(emailDomain))
        {
            return this.byDomain[emailDomain];
        }

        return new List<Person>();
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        if (this.byNameAndTown.ContainsKey(town+name))
        {
            return this.byNameAndTown[town+name];
        }

        return new List<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsInRange = this.byAge.Range(startAge,true,endAge,true);

        foreach (var kvp in personsInRange)
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (this.byTownAndAge.ContainsKey(town))
        {
            var people = this.byTownAndAge[town].Range(startAge, true, endAge, true);

            foreach (var kvp in people)
            {
                foreach (var person in kvp.Value)
                {
                    yield return person;
                }
            }
        }
    }
}
