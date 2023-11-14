namespace NetArc;

/// <summary>
/// Перечисление кодов (тегов) для обозначения сообщений
/// </summary>
public static class Code
{
    public static string Wrap(string text) => $"<|{text}|>";

    public const string 
        BROADCAST = "<|broadcast|>",
        AUTH = "<|auth|>",
        ACCEPT = "<|accept|>",
        DENIED = "<|denied|>";
}