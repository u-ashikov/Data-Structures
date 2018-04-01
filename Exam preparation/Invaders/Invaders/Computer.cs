using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer
{
    private HashSet<Invader> invaders;

    private OrderedBag<Invader> byPriority;

    private int energy;

    public Computer(int energy)
    {
        this.Energy = energy;
        this.invaders = new HashSet<Invader>();
        this.byPriority = new OrderedBag<Invader>();
    }

    public int Energy
    {
        get
        {
            if (this.energy < 0)
            {
                return 0;
            }

            return this.energy;
        }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException();
            }

            this.energy = value;
        }
    }

    public void Skip(int turns)
    {
        foreach (var inv in this.invaders)
        {
            inv.Distance -= turns;
        }

        var removed = this.byPriority.RemoveAll(i => i.Distance <= 0);

        foreach (var inv in removed)
        {
            this.byPriority.RemoveAllCopies(inv);
            this.invaders.Remove(inv);
        }

        this.energy -= removed.Sum(i => i.Damage);
    }

    public void AddInvader(Invader invader)
    {
        this.invaders.Add(invader);

        this.byPriority.Add(invader);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        var removed = this.byPriority.Take(count).ToList();

        foreach (var inv in removed)
        {
            this.byPriority.RemoveAllCopies(inv);
            this.invaders.Remove(inv);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        var removed = this.byPriority.RemoveAll(i => i.Distance <= radius);

        foreach (var inv in removed)
        {
            this.invaders.Remove(inv);
        }
    }

    public IEnumerable<Invader> Invaders() => this.invaders;
}
