using System.Net.Sockets;

namespace NetArc.Server;

/// <summary>
/// Обработчик соединения с клиента
/// </summary>
internal class ConnectionTcp
{
    /// <summary>
    /// Создать соединение
    /// </summary>
    /// <param name="client"> Сокет клиента </param>
    /// <param name="callback"> Метод для обработки сообщений клиента </param>
    public ConnectionTcp(Socket client, Action<string> callback)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Запустить прослушку входящих соединений с данного клиента
    /// </summary>
    public void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить работу с клиентом
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщение клиенту
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public void Send(string message)
    {
        throw new NotImplementedException();
    }
}