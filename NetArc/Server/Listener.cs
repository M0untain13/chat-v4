using System.Net;
using System.Net.Sockets;

namespace NetArc.Server;

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
        _callback = callback;

        var host = Dns.GetHostName();
        var addrList = Dns.GetHostByName(host).AddressList;
        var ip = addrList[10].ToString();

        _ipep = new IPEndPoint(IPAddress.Parse(ip), port);
        _server = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
    }

    /// <summary>
    /// Начать прослушку, для входящих соединений создать ConnectionTcp, запустить и добавить в массив соединений
    /// </summary>
    public bool Start()
    {
        if (_isStart) 
            return false;

        _server.Bind(_ipep);
        _server.Listen();
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
    /// <param name="id"></param>
    public bool Send(WebMessage message, int id = -1)
    {
        if (message.sender != "server" && id < 0)
            return false;

        if (message.sender == "client" && message.type != "error")
        {
            _callback($"Пришло сообщение: {message.name}, {message.type}, {message.text}.");
        }

        switch (message)
        {
            case { sender: "server" }:
                foreach (var client in _clients)
                    client.Item2.Send(message);
                break;

            case { sender: "client", type: "auth"}:
                var isUnique = true;
                var deniedNames = new string[] {
                    "anon", "Anon", "server", "Server"
                };
                if (deniedNames.Contains(message.text) 
                    || _clients.Any(client => client.Item1 == message.text)) 
                {
                    isUnique = false;
                }
                var result = isUnique ? "accept" : "denied";
                if (isUnique) {
                    for (var i = 0; i < _clients.Count; i++)
                        if (_clients[i].Item2.Id == id) {
                            _clients[i] = (message.text, _clients[i].Item2);
                            _callback("Клиент авторизовался!");
                            break;
                        }
                }
                else
                    _callback("Клиент не смог авторизоваться...");
                    
                (string, Connection)? connection = null;
                foreach (var client in _clients.Where(client => id == client.Item2.Id)) {
                    connection = client; 
                    break;
                }
                connection?.Item2.Send(new WebMessage(sender: "server", type: "auth", name: message.text, text: result));
                break;

            case { sender: "client", type: "message" }:
                var isAcceptMessage = true;
                for (var i = 0; i < _clients.Count; i++)
                    if (_clients[i].Item2.Id == id && _clients[i].Item1 == "anon") {
                        isAcceptMessage = false;
                        break;
                    }
                if (isAcceptMessage)
                    foreach (var client in _clients)
                        if(client.Item1 != "anon")
                            client.Item2.Send(message);
                break;

            case { sender: "client", type: "exit" }:
                for (var i = 0; i < _clients.Count; i++)
                    if (_clients[i].Item2.Id == id) {
                        _clients.RemoveAt(i);
                        break;
                    }
                break;

            case { sender: "client", type: "error" }:
                for (var i = 0; i < _clients.Count; i++)
                    if (_clients[i].Item2.Id == id)
                    {
                        _clients.RemoveAt(i);
                        break;
                    }
                _callback("Клиент разорвал соединение...");
                break;
        }

        return true;
    }

    private void Callback(WebMessage msg, int id) => Send(msg, id);

    // TODO: Мне кажется нужно добавить мьютекс для доступа к списку клиентов
    private readonly List<(string, Connection)> _clients = new();
    private readonly Action<string> _callback;
    private readonly Socket _server;
    private readonly IPEndPoint _ipep;

    private int _nextId;
    private int NextId => _nextId++;

    private bool _isStart;
}