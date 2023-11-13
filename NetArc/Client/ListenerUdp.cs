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
    public void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить прослушку
    /// </summary>
    public void Stop()
    {
        throw new NotImplementedException();
    }
}