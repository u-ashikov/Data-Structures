using System;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        var input = Console.ReadLine();
        var textEditor = new TextEditor();

        string user = "";
        string command = "";
        string pattern = @"""(.*)""";

        while (input.ToLower() != "end")
        {
            var text = Regex.Match(input, pattern).Groups[1].Value;
            var parameters = input.Split();

            if (!textEditor.UserExists(user) && parameters[0] != "login")
            {
                input = Console.ReadLine();
                continue;
            }

            if (parameters[0] == "login")
            {
                command = parameters[0];
                user = parameters[1];
            }
            else if (parameters[0] == "users")
            {
                command = parameters[0];
            }
            else
            {
                command = parameters[1];
                user = parameters[0];
            }

            switch (command)
            {
                case "login":
                    textEditor.Login(user);
                    break;
                case "insert":
                    textEditor.Insert(user, int.Parse(parameters[2]), text);
                    break;
                case "prepend":
                    textEditor.Prepend(user, text);
                    break;
                case "print":
                    Console.WriteLine(textEditor.Print(user));
                    break;
                case "length":
                    Console.WriteLine(textEditor.Length(user));
                    break;
                case "delete":
                    textEditor.Delete(user, int.Parse(parameters[2]), int.Parse(parameters[3]));
                    break;
                case "substring":
                    textEditor.Substring(user, int.Parse(parameters[2]), int.Parse(parameters[3]));
                    break;
                case "users":
                    Console.WriteLine(string.Join(Environment.NewLine, textEditor.Users(parameters.Length > 1 ? parameters[1] : string.Empty)));
                    break;
                case "undo":
                    textEditor.Undo(user);
                    break;
                case "clear":
                    textEditor.Clear(user);
                    break;
                case "logout":
                    textEditor.Logout(user);
                    break;
            }

            input = Console.ReadLine();
        }
    }
}
