namespace NetArc;

/// <summary>
/// Перечисление тегов для обозначения сообщений
/// </summary>
public static class Tag
{
    public const string
        BROADCAST = "<|broadcast|>", // Вещание сервера
        MESSAGE = "<|message|>", // Сообщение от клиента клиентам через сервер
        AUTH = "<|auth|>", // Попытка авторизоваться
        ACCEPT = "<|accept|>", // Сервер принимает авторизацию
        DENIED = "<|denied|>"; // Сервер отклоняет авторизацию

    public const string
        NAME_S = "<name>",
        NAME_E = "<name/>",
        TEXT_S = "<text>",
        TEXT_E = "<text/>";

    /// <summary>
    /// Метод для обёртки имени, кому отправляется сообщение от сервера
    /// </summary>
    /// <param name="text"> Текст имени </param>
    /// <returns> Тег </returns>
    public static string Wrap(string text) => $"<|{text}|>";

    public static string Wrap(string text, string tag) => $"{tag}{text}{tag}";

    public static string ParseWrap(string text, string tag_s, string tag_e)
    {
        var start = text.IndexOf(tag_s);
        var end = text.IndexOf(tag_e);

        if(start == -1 || end == -1) 
            return "";

        return text.Substring(start + tag_s.Length, end);
    }
}