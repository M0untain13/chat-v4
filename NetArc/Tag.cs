namespace NetArc;

/// <summary>
/// Перечисление тегов для обозначения сообщений
/// </summary>
public static class Tag
{
    /// <summary>
    /// Метод для обёртки имени, кому отправляется сообщение от сервера
    /// </summary>
    /// <param name="text"> Текст имени </param>
    /// <returns> Тег </returns>
    public static string Wrap(string text) => $"<|{text}|>";

    public const string 
        BROADCAST = "<|broadcast|>",    // Вещание сервера
        AUTH = "<|auth|>",              // Попытка авторизоваться
        ACCEPT = "<|accept|>",          // Сервер принимает авторизацию
        DENIED = "<|denied|>";          // Сервер отклоняет авторизацию
}