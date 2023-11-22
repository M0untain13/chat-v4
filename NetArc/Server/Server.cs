using System.Net;
using System.Net.Sockets;
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
        _broadcaster = new Broadcaster(broadcastPort, broadcastTimeout);
        _listener = new Listener(callback, listenPort);
    }

    /// <summary>
    /// Запустить сервер, запустить прослушку и вещатель
    /// </summary>
    public bool Start()
    {
        return _broadcaster.Start() && _listener.Start();
    }

    /// <summary>
    /// Остановить сервер, остановить прослушку и вещатель
    /// </summary>
    public bool Stop()
    {
        return _broadcaster.Stop() || _listener.Stop();
    }

    /// <summary>
    /// Отправить сообщение через прослушку
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(WebMessage message)
    {
        return _listener.Send(message);
    }

    private readonly Broadcaster _broadcaster;
    private readonly Listener _listener;
}