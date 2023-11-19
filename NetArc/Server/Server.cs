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

    private readonly int _listenPort;
    private readonly int _broadcastPort;
    private readonly int _broadcastTimeout;
    private Action<WebMessage> _messageCallback;

    private ServerUdp _udpServer;

    public Server(Action<WebMessage> callback, int listenPort, int broadcastPort, int broadcastTimeout)
    {
        _messageCallback = callback;
        _listenPort = listenPort;
        _broadcastPort = broadcastPort;
        _broadcastTimeout = broadcastTimeout;
        _udpServer = new ServerUdp(broadcastPort, broadcastTimeout);
    }

    /// <summary>
    /// Запустить сервер, запустить прослушку и вещатель
    /// </summary>
    public bool Start()
    {
        return true;
    }

    /// <summary>
    /// Остановить сервер, остановить прослушку и вещатель
    /// </summary>
    public bool Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщение через прослушку
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(WebMessage message)
    {
        throw new NotImplementedException();
    }
}