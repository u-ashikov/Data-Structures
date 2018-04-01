using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Scoreboard : IScoreboard
{
    private Dictionary<string, string> users;

    private Dictionary<string, string> games;

    private Dictionary<string, OrderedBag<ScoreboardEntry>> scores;

    private int entriesCount;

    public Scoreboard(int maxEntriesToKeep = 10)
    {
        this.users = new Dictionary<string, string>();
        this.games = new Dictionary<string, string>();
        this.scores = new Dictionary<string, OrderedBag<ScoreboardEntry>>();
        this.entriesCount = maxEntriesToKeep;
    }

    public bool RegisterUser(string username, string password)
    {
        if (this.users.ContainsKey(username))
        {
            return false;
        }

        this.users.Add(username, password);

        return true;
    }

    public bool RegisterGame(string game, string password)
    {
        if (this.games.ContainsKey(game))
        {
            return false;
        }

        this.games.Add(game, password);
        
        if (!this.scores.ContainsKey(game))
        {
            this.scores.Add(game, new OrderedBag<ScoreboardEntry>());
        }

        return true;
    }

    public bool AddScore(string username, string userPassword, string game, string gamePassword, int score)
    {
        if (!this.games.ContainsKey(game) || !this.users.ContainsKey(username) || this.games[game] != gamePassword || this.users[username] != userPassword)
        {
            return false;
        }

        this.scores[game].Add(new ScoreboardEntry(username,score));

        return true;
    }

    public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
    {
        if (!this.scores.ContainsKey(game))
        {
            return null;
        }

        return this.scores[game].Take(10);
    }

    public bool DeleteGame(string game, string gamePassword)
    {
        if (this.games.ContainsKey(game))
        {
            if (this.games[game] == gamePassword)
            {
                this.scores.Remove(game);
                return this.games.Remove(game);
            }
        }

        return false;
    }

    public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
    {
        return this.games.Keys.Where(k => k.StartsWith(gameNamePrefix)).OrderBy(k => k).Take(10);
    }
}