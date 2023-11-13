namespace NetArc.Client;

/// <summary>
/// Стартовая точка для создания клиента
/// </summary>
public class Client
{
    /// <summary>
    /// Создать клиент, создать соединитель и прослушку вещаний
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <param name="connectPort"> Порт для соединения к серверу </param>
    /// <param name="listenPort"> Порт прослушки вещаний </param>
    public Client(Action<string> callback, int connectPort, int listenPort)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Начать попытку соединения с сервером (Сначала слушать IP сервера с вещателя, а затем сделать попытку соединения с сервером по этому IP)
    /// </summary>
    public void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить работу клиента
    /// </summary>
    public void Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщение на сервер
    /// </summary>
    /// <param name="message"></param>
    public void Send(string message)
    {
        throw new NotImplementedException();
    }
}