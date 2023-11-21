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
        _callback = callback;
        _port = port;
        var ip = new IPEndPoint(IPAddress.Any, _port);
        _server = new TcpClient(ip);
        _stream = _server.GetStream();
        _receiver = new Task(() =>
        {
            var parser = new Parser();
            while (_isStart)
            {
                var buffer = new byte[1024];
                var size = _stream.Read(buffer);
                if (size > 0)
                {
                    _callback(parser.ParseMessage(Encoding.ASCII.GetString(buffer, 0, size)));
                }
            }
        });
    }

    /// <summary>
    /// Сделать попытку соединения с сервером, если получилось, то начать прослушку входящих сообщений с сервера
    /// </summary>
    public bool Start(string ip)
    {
        var ipep = new IPEndPoint(IPAddress.Parse(ip), _port);
        _server.Connect(ipep);

        _isStart = true;
        _receiver.Start();
        
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
        _server.Close();
        _stream.Close();
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
            _stream.Write(Encoding.ASCII.GetBytes(message));
        });

        return true;
    }

    /// <summary>
    /// Установить новый обработчик сообщений сервера
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <returns> Удалось ли установить обработчик </returns>
    public void SetCallback(Action<WebMessage> callback) => _callback = callback;

    private readonly TcpClient _server;
    private readonly NetworkStream _stream;
    private readonly Task _receiver;
    private readonly int _port;

    private Action<WebMessage> _callback; 

    private bool _isStart;
}