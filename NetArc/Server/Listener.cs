using System.Net;
using System.Net.Sockets;

namespace NetArc.Server;

// TODO: !!ВАЖНО!! Listener callback сервера использует для логирования в консоль, а в connection он должен передавать собственный колбек для обработки входящих сообщений 

/// <summary>
/// Слушатель входящих соединений с клиентов
/// </summary>
internal class Listener
{
    /// <summary>
    /// Создать прослушку
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений клиентов </param>
    /// <param name="port"> Порт для прослушивания </param>
    public Listener(Action<string> callback, int port)
    {
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var endPoint = new IPEndPoint(IPAddress.Any, port);
        _callback = callback;
        _server.Bind(endPoint);
    }

    /// <summary>
    /// Начать прослушку, для входящих соединений создать ConnectionTcp, запустить и добавить в массив соединений
    /// </summary>
    public bool Start()
    {
        if (_isStart) 
            return false;

        _isStart = true;
        Task.Run(() => {
            while(_isStart)
            {
                var client = _server.Accept();
                var con = new Connection(client, Callback, NextId);
                _clients.Add(("anon", con));
                con.Start();
            }
        });

        return true;
    }

    /// <summary>
    /// Остановить прослушку, остановить соединения и отчистить массив
    /// </summary>
    public bool Stop()
    {
        if (!_isStart) 
            return false;

        _isStart = false;
        return true;
    }

    /// <summary>
    /// Отправить сообщения всем клиентам
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(WebMessage message, int id = -1)
    {
        if (message.sender != "server" && id < 0)
            return false;

        _callback($"Пришло сообщение: {message.sender}, {message.type}, {message.name}, {message.text}.");

        switch (message)
        {
            case { sender: "server" } or { sender: "client", type: "message" }:
                foreach (var client in _clients)
                {
                    client.Item2.Send(message);
                }
                break;

            case { sender: "client", type: "auth"}:
                var isUnique = true;
                foreach (var client in _clients.Where(client => client.Item1 == message.text))
                {
                    isUnique = false;
                }
                var result = isUnique ? "accept" : "denied";
                (string, Connection)? connection = null;
                foreach (var client in _clients.Where(client => id == client.Item2.Id))
                {
                    connection = client; 
                    break;
                }
                connection?.Item2.Send(new WebMessage(sender: "server", type: "auth", name: "", text: result));
                break;
        }

        return true;
    }

    private void Callback(WebMessage msg, int id) => Send(msg, id);

    private readonly List<(string, Connection)> _clients = new();
    private readonly Action<string> _callback;
    private readonly Socket _server;

    private int _nextId = 0;
    private int NextId => _nextId++;

    private bool _isStart;
}