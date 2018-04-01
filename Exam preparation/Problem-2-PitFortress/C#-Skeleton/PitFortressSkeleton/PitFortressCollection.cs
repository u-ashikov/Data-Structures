using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Interfaces;
using Wintellect.PowerCollections;

public class PitFortressCollection : IPitFortress
{
    private const int MinDelay = 1;
    private const int MaxDelay = 10000;

    private Dictionary<string, Player> players;

    private OrderedDictionary<int, LinkedList<Minion>> minionsByPosition;

    private Dictionary<string, LinkedList<Mine>> minesByPlayer;

    private OrderedBag<Minion> orderedMinions;

    private OrderedBag<Mine> orderedMines;

    public PitFortressCollection()
    {
        this.players = new Dictionary<string, Player>();
        this.minionsByPosition = new OrderedDictionary<int, LinkedList<Minion>>();
        this.orderedMinions = new OrderedBag<Minion>();
        this.minesByPlayer = new Dictionary<string, LinkedList<Mine>>();
        this.orderedMines = new OrderedBag<Mine>();
    }

    public int PlayersCount => this.players.Count;

    public int MinionsCount => this.orderedMinions.Count;

    public int MinesCount => this.orderedMines.Count;

    public void AddPlayer(string name, int mineRadius)
    {
        var player = new Player(name, mineRadius);

        if (this.players.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        this.players.Add(name, player);
    }

    public void AddMinion(int xCoordinate)
    {
        var minion = new Minion(xCoordinate, this.MinionsCount+1);

        if (!this.minionsByPosition.ContainsKey(xCoordinate))
        {
            this.minionsByPosition.Add(xCoordinate, new LinkedList<Minion>());
        }

        this.minionsByPosition[xCoordinate].AddLast(minion);
        this.orderedMinions.Add(minion);
    }

    public void SetMine(string playerName, int xCoordinate, int delay, int damage)
    {
        if (!this.players.ContainsKey(playerName))
        {
            throw new ArgumentException();
        }

        if (delay < MinDelay || delay > MaxDelay)
        {
            throw new ArgumentException();
        }

        var minRadius = this.MinesCount + 1;
        var maxRadius = this.players[playerName];

        var mine = new Mine(minRadius, delay, damage, xCoordinate, maxRadius);

        if (!this.minesByPlayer.ContainsKey(playerName))
        {
            this.minesByPlayer.Add(playerName, new LinkedList<Mine>());
        }

        this.minesByPlayer[playerName].AddLast(mine);
        this.orderedMines.Add(mine);
    }

    public IEnumerable<Minion> ReportMinions() => this.orderedMinions;

    public IEnumerable<Player> Top3PlayersByScore()
    {
        this.CheckPlayersCount();

        return this.players.Values.OrderBy(p => p).Take(3);
    }

    public IEnumerable<Player> Min3PlayersByScore()
    {
        this.CheckPlayersCount();

        return this.players.Values.OrderByDescending(p=>p).Take(3);
    }

    public IEnumerable<Mine> GetMines() => this.orderedMines;

    public void PlayTurn()
    {
        var removedMines = new LinkedList<Mine>();

        foreach (var mine in this.orderedMines)
        {
            mine.Delay--;

            if (mine.Delay <= 0)
            {
                var player = this.players[mine.Player.Name];
                var minRange = mine.XCoordinate - player.Radius;
                var maxRange = mine.XCoordinate + player.Radius;

                this.HitMinions(mine, player, minRange, maxRange);

                this.minesByPlayer[mine.Player.Name].Remove(mine);
                removedMines.AddLast(mine);
            }
        }

        this.orderedMines.RemoveMany(removedMines);
    }

    private void CheckPlayersCount()
    {
        if (this.PlayersCount < 3)
        {
            throw new ArgumentException();
        }
    }

    private void HitMinions(Mine mine, Player player, int minRange, int maxRange)
    {
        var minionsInRadius = this.minionsByPosition
                            .Range(minRange, true, maxRange, true)
                            .SelectMany(m => m.Value)
                            .ToList();

        foreach (var minion in minionsInRadius)
        {
            minion.Health -= mine.Damage;

            if (minion.Health <= 0)
            {
                this.minionsByPosition[minion.XCoordinate].Remove(minion);
                this.orderedMinions.Remove(minion);

                player.Score++;
            }
        }
    }
}
