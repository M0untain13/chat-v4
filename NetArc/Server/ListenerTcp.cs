using System.Net;
using System.Net.Sockets;

namespace NetArc.Server;

// TODO: !!ВАЖНО!! Listener callback сервера использует для логирования в консоль, а в connection он должен передавать собственный колбек для обработки входящих сообщений 

/// <summary>
/// Слушатель входящих соединений с клиентов
/// </summary>
internal class ListenerTcp
{
    /// <summary>
    /// Создать прослушку
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений клиентов </param>
    /// <param name="port"> Порт для прослушивания </param>

    static List<IPEndPoint> Clients = new List<IPEndPoint>(); // Список "подключенных" клиентов

    public ListenerTcp(Action<string> callback, int port)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Начать прослушку, для входящих соединений создать ConnectionTcp, запустить и добавить в массив соединений
    /// </summary>
    public bool Start()
    {
        //Socket server;
        //Socket client = server.Accept();
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить прослушку, остановить соединения и отчистить массив
    /// </summary>
    public bool Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщения всем клиентам
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(string message)
    {
        throw new NotImplementedException();
    }
}