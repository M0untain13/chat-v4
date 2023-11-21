using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestForNetArc")]

namespace NetArc;

internal class Parser
{
	public string CreateMessage(WebMessage webMessage)
	{
		var senderTage = $"<sender>{webMessage.sender}</sender>";
		var typeTage = $"<type>{webMessage.type}</type>";
		var nameTage = $"<name>{webMessage.name}</name>";
		var textTage = $"<text>{webMessage.text}</text>";

		return string.Concat(senderTage, typeTage, nameTage, textTage);
	}

	public WebMessage ParseMessage(string message)
	{
		// TODO: сделаем вид, что на вход всегда подаются правильные данные без ошибок
		var sender = _ParseTag(message, "<sender>", "</sender>");
		var type = _ParseTag(message, "<type>", "</type>");
        var name = _ParseTag(message, "<name>", "</name>");
		var text = _ParseTag(message, "<text>", "</text>");

		return new WebMessage(sender, type, name, text);
	}

	private string _ParseTag(string message, string tagS, string tagE)
	{
		var start = message.IndexOf(tagS, StringComparison.Ordinal) + tagS.Length;
		var end = message.IndexOf(tagE, StringComparison.Ordinal) - start;

		if (start == -1 || end == -1)
			return "";

		return message.Substring(start, end);
	}
}