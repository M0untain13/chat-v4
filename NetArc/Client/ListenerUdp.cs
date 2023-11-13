namespace NetArc.Client;

/// <summary>
/// Слушатель вещаний сервера
/// </summary>
internal class ListenerUdp
{
    /// <summary>
    /// Создать слушатель
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <param name="port"> Порт для прослушки вещаний </param>
    public ListenerUdp(Action<string> callback, int port)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Начать прослушку
    /// </summary>
    public bool Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить прослушку
    /// </summary>
    public bool Stop()
    {
        throw new NotImplementedException();
    }
}