using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetArc.Client;

/// <summary>
/// Соединитель клиента с сервером
/// </summary>
internal class Connector
{
    /// <summary>
    /// Создать соединитель
    /// </summary>
    /// <param name="server"> Сокет сервера </param>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <param name="port"> Порт для соединения </param>
    public Connector(Action<WebMessage> callback, int port)
    {
        _receiver = null!;
        _callback = callback;
        _port = port;
        _client = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
    }

    /// <summary>
    /// Сделать попытку соединения с сервером, если получилось, то начать прослушку входящих сообщений с сервера
    /// </summary>
    public bool Start(string ip)
    {
        var ipep = new IPEndPoint(IPAddress.Parse(ip), _port);
        _client.Connect(ipep);
        _isStart = true;
        _receiver = Task.Run(() =>
        {
            var parser = new Parser();
            while (_isStart)
            {
                var buffer = new byte[1024];
                try
                {
                    var size = _client.Receive(buffer);
                    if (size > 0)
                    {
                        _callback(parser.ParseMessage(Encoding.UTF8.GetString(buffer, 0, size)));
                    }
                }
                catch (SocketException ex)
                {
                    _isStart = false;
                    _client.Close();
                }
            }
        });
        
        return true;
    }

    /// <summary>
    /// Остановить попытку соединения или прослушку входящих соединений
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public bool Stop()
    {
        if (!_isStart)
            return false;
        
        _isStart = false;
        _client.Close();
        _receiver.Wait();

        return true;
    }

    /// <summary>
    /// Отправить сообщение на сервер
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(string message)
    {
        if (!_isStart)
            return false;

        Task.Run(() =>
        {
            _client.Send(Encoding.UTF8.GetBytes(message));
        });

        return true;
    }

    /// <summary>
    /// Установить новый обработчик сообщений сервера
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <returns> Удалось ли установить обработчик </returns>
    public void SetCallback(Action<WebMessage> callback) => _callback = callback;

    private readonly Socket _client;
    private readonly int _port;

    private Task _receiver;

    private Action<WebMessage> _callback; 

    private bool _isStart;
}