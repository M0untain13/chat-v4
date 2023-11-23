using System.Net;
using System.Net.Sockets;

namespace NetArc.Server;

// TODO: !!ВАЖНО!! Listener callback сервера использует для логирования в консоль, а в connection он должен передавать собственный колбек для обработки входящих сообщений 

/// <summary>
/// Слушатель входящих соединений с клиентов
/// </summary>
internal class Listener
{
    private readonly List<(string, Connection, int)> _clients = new(); // Список "подключенных" клиентов
    private Action<string> _callback;
    private Socket _server;
    private IPEndPoint _endPoint;

    private int _nextId = 0;
    private int NextId
    {
        get
        {
            return _nextId++;
        }
    }

    /// <summary>
    /// Создать прослушку
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений клиентов </param>
    /// <param name="port"> Порт для прослушивания </param>
    public Listener(Action<string> callback, int port)
    {
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _endPoint = new IPEndPoint(IPAddress.Any, port);
        _callback = callback;
        _server.Bind(_endPoint);
    }


    private bool _isStart;
    private void Callback(WebMessage msg, int id) => Send(msg, id);


    /// <summary>
    /// Начать прослушку, для входящих соединений создать ConnectionTcp, запустить и добавить в массив соединений
    /// </summary>
    public bool Start()
    {
        if (_isStart) { return false; }
        _isStart = true;
        Task.Run(() => {
            while(_isStart)
            {
                Socket client = _server.Accept();
                // TODO: id закинуть в connection и переделать Callback
                if (client != null) { _clients.Add(("anon", new Connection(client, Callback), NextId)); }
            }
        });
        return true;
    }

    /// <summary>
    /// Остановить прослушку, остановить соединения и отчистить массив
    /// </summary>
    public bool Stop()
    {
        if (!_isStart) { return false; }
        _isStart = false;
        return true;
    }

    /// <summary>
    /// Отправить сообщения всем клиентам
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(WebMessage message, int id = -1)
    {
        if (message.sender != "server" && id < 0) { return false; }
        switch (message)
        {
            case { sender: "server" } or { sender: "client", type: "message" }:
                foreach (var client in _clients)
                {
                    client.Item2.Send(message);
                }
                break;
            case { sender: "client", type: "auth"}:
                bool isUnique = true;
                foreach (var client in _clients)
                {
                    if (client.Item1 == message.text) { isUnique = false; }
                }
                string result = isUnique ? "accept" : "denied";
                (string, Connection, int)? connection = null;
                foreach (var client in _clients)
                {
                    if (id == client.Item3) { connection = client; break; }
                }
                if (connection != null) { 
                    (((string, Connection, int))(connection)).Item2.Send(new WebMessage(sender: "server", type: "auth", name: "", text: result)); }
                break;
        }
        return true;
    }
}