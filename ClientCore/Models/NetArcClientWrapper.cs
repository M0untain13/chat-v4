using NetArc;
using NetArc.Client;

namespace ClientCore.Models;

public class NetArcClientWrapper : IClientWrapper
{
    public NetArcClientWrapper(Action<WebMessage> callback, int connectPort, int listenPort)
    {
        _client = new Client(callback, connectPort, listenPort);
    }

    public bool Start() => _client.Start();

    public bool Stop() => _client.Stop();

    public bool Send(WebMessage message) => _client.Send(message);

    public bool SetCallback(Action<WebMessage> callback) => _client.SetCallback(callback);

    private readonly Client _client;
}

public class TempClientWrapper : IClientWrapper
{
    public TempClientWrapper(Action<WebMessage> callback, int connectPort, int listenPort) { }

    public bool Start() => true;

    public bool Stop() => true;

    public bool Send(WebMessage message) => true;

    public bool SetCallback(Action<WebMessage> callback) => true;
}