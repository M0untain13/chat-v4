namespace NetArc;

public class WebMessage
{
    public string sender;
    public string type;
    public string name;
    public string text;

    public WebMessage(string sender, string type, string name, string text)
    {
        this.sender = sender;
        this.type = type;
        this.name = name;
        this.text = text;
    }
}