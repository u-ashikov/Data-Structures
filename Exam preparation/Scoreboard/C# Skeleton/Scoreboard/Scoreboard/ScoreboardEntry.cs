using System;

public class ScoreboardEntry : IComparable<ScoreboardEntry>
{
    public ScoreboardEntry(string username, int score)
    {
        this.Username = username;
        this.Score = score;
    }

    public int Score { get; set; }

    public string Username { get; set; }

    public int CompareTo(ScoreboardEntry other)
    {
        int cmp = other.Score.CompareTo(this.Score);

        if (cmp == 0)
        {
            cmp = this.Username.CompareTo(other.Username);
        }
        return cmp;
    }
}