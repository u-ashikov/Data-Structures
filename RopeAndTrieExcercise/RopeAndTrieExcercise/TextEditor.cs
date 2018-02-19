using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextEditor : ITextEditor
{
    private IDictionary<string, StringBuilder> users;

    private IDictionary<string, Stack<string>> texts;

    public TextEditor()
    {
        this.users = new Dictionary<string, StringBuilder>();
        this.texts = new Dictionary<string, Stack<string>>();
    }

    public void Clear(string username)
    {
        this.Cache(username);
        this.users[username].Clear();
    }

    public void Delete(string username, int startIndex, int length)
    {
        this.Cache(username);
        this.users[username].Remove(startIndex, length);
    }

    public void Insert(string username, int index, string str)
    {
        this.Cache(username);
        this.users[username].Insert(index, str);
    }

    public int Length(string username) => this.users[username].Length;

    public void Login(string username)
    {
        if (!this.users.ContainsKey(username))
        {
            this.users.Add(username, new StringBuilder());
            this.texts.Add(username, new Stack<string>());
        }
    }

    public void Logout(string username)
    {
        this.users.Remove(username);
        this.texts.Remove(username);
    }

    public void Prepend(string username, string str)
    {
        if (this.users.ContainsKey(username))
        {
            this.Cache(username);
            this.users[username].Insert(0, str);
        }
    }

    public string Print(string username) => this.users[username].ToString();

    public void Substring(string username, int startIndex, int length)
    {
        this.Cache(username);
        this.users[username] = new StringBuilder(this.users[username].ToString().Substring(startIndex, length));
    }

    public void Undo(string username)
    {
        if (this.texts[username].Count != 0)
        {
            this.users[username] = new StringBuilder(this.texts[username].Pop());
        }
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        if (string.IsNullOrEmpty(prefix))
        {
            return this.users.Keys;
        }

        return this.users.Keys.Where(u => u.StartsWith(prefix));
    }

    public bool UserExists(string username) => this.users.ContainsKey(username);

    private void Cache(string username)
    {
        this.texts[username].Push(this.users[username].ToString());
    }
}
