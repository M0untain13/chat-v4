using NetArc.Client;

namespace ClientCore.Models;

public class NetArcClientWrapper : IClientWrapper
{
    public NetArcClientWrapper(Action<string> callback, int connectPort, int listenPort)
    {
        _client = new Client(callback, connectPort, listenPort);
    }

    public bool Start() => _client.Start();

    public bool Stop() => _client.Stop();

    public bool Send(string message) => _client.Send(message);

    private readonly Client _client;
}