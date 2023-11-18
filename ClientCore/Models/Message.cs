namespace ClientCore.Models;

public class Message
{
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public Message(string name, string text)
    {
        Name = name;
        Text = text;
    }
}