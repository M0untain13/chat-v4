using static System.Net.Mime.MediaTypeNames;

namespace NetArc;

public class Sender
{
	public readonly string value;
	private Sender(string value)
	{
		this.value = value;
	}

	public static Sender Server => new("server");
	public static Sender Client => new("client");
}

public class Type
{
	public readonly string value;
	private Type(string value)
	{
		this.value = value;
	}

    public static Type Broadcast => new("broadcast");
    public static Type Auth => new("auth");
    public static Type Message => new("message");
}

public class Message
{
	public Sender sender;
	public Type type;
	public string name;
	public string text;

	public Message(Sender sender, Type type, string name, string text)
	{
		this.sender = sender;
		this.type = type;
		this.name = name;
		this.text = text;
	}
}

public class Parser
{
	public static string CreateMessage(Message message)
	{
		var senderTage = $"<sender>{message.sender}</sender>";
		var typeTage = $"<type>{message.type}</type>";
		var nameTage = $"<name>{message.name}</name>";
		var textTage = $"<text>{message.text}</text>";

		return string.Concat(senderTage, typeTage, nameTage, textTage);
	}

	public static Message ParseMessage(string message)
	{
		// TODO: сделаем вид, что на вход всегда подаются правильные данные без ошибок
		var sender = _ParseTag(message, "<sender>", "</sender>") switch
		{
			"server" => Sender.Server,
			"client" => Sender.Client,
			_ => throw new ArgumentException()
		};
		var type = _ParseTag(message, "<type>", "</type>") switch
		{
			"broadcast" => Type.Broadcast,
			"auth" => Type.Auth,
			"message" => Type.Message,
			_ => throw new ArgumentException()
		};
        var name = _ParseTag(message, "<name>", "</name>");
		var text = _ParseTag(message, "<text>", "</text>");

		return new Message(sender, type, name, text);
	}

	public static string _ParseTag(string message, string tagS, string tagE)
	{
		var start = message.IndexOf(tagS, StringComparison.Ordinal) + tagS.Length;
		var end = message.IndexOf(tagE, StringComparison.Ordinal) - start;

		if (start == -1 || end == -1)
			return "";

		return message.Substring(start, end);
	}
}