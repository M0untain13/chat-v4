using System;
using System.Net.Sockets;
using System.Text;

namespace NetArc.Server;

/// <summary>
/// Обработчик соединения с клиента
/// </summary>
internal class Connection
{
    public int Id { get; }

    /// <summary>
    /// Создать соединение
    /// </summary>
    /// <param name="client"> Сокет клиента </param>
    /// <param name="callback"> Метод для обработки сообщений клиента </param>
    /// <param name="id"></param>
    public Connection(Socket client, Action<WebMessage, int> callback, int id)
    {
        _client = client;
        _callback = callback;
        _parser = new Parser();
        Id = id;
    }

    /// <summary>
    /// Запустить прослушку входящих соединений с данного клиента
    /// </summary>
    public bool Start()
    {
        if (_isStart)
            return false;

        _isStart = true;

        _receiver = Task.Run(() =>
        {
            while (_isStart)
            {
                try
                {
                    var buffer = new byte[1024];
                    _client.Receive(buffer);
                    _callback(_parser.ParseMessage(Encoding.UTF8.GetString(buffer)), Id);
                }
                catch (SocketException ex)
                {
                    _callback(new WebMessage("client", "error", Id.ToString(), ex.Message), Id);
                    _isStart = false;
                    _client.Close();
                }
            }
        });

        return true;
    }

    /// <summary>
    /// Остановить работу с клиентом
    /// </summary>
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
    /// Отправить сообщение клиенту
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(WebMessage message)
    {
        if (!_isStart)
            return false;

        Task.Run(() =>
        {
            _client.Send(Encoding.UTF8.GetBytes(_parser.CreateMessage(message)));
        });

        return true;
    }

    private Task _receiver = new(() => { });

    private readonly Socket _client;
    private readonly Action<WebMessage, int> _callback;
    private readonly Parser _parser;

    private bool _isStart;
}