namespace NetArc.Server;

/// <summary>
/// Стартовая точка для создания сервера
/// </summary>
public class Server
{
    /// <summary>
    /// Создать сервер, создать прослушку и вещатель
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений клиентов </param>
    /// <param name="listenPort"> Порт для прослушки соединений </param>
    /// <param name="broadcastPort"> Порт для вещания </param>
    /// <param name="broadcastTimeout"> Периодичность отправления вещаний в мс </param>
    public Server(Action<string> callback, int listenPort, int broadcastPort, int broadcastTimeout)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Запустить сервер, запустить прослушку и вещатель
    /// </summary>
    public void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить сервер, остановить прослушку и вещатель
    /// </summary>
    public void Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщение через прослушку
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public void Send(string message)
    {
        throw new NotImplementedException();
    }
}