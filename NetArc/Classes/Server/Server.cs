using NetArc.Interfaces;

namespace NetArc.Classes.Server;

internal class Server : INetworkElement
{
    // callback - Получить от клиента сообщение, обработать его и отправить сообщение остальным клиентам и мб вывести что-нибудь в консоль
    public Server(Action<string> callback, IServerFactory networkFactory)
    {
        _listener = networkFactory.CreateListener(callback);
        _broadcaster = networkFactory.CreateBroadcaster(2000);
    }

    public void Start()
    {
        _isWork = true;
        _listener.Start();
        _broadcaster.Start();
    }

    public void Stop()
    {
        _isWork = false;
        _listener.Stop();
        _broadcaster.Stop();
    }

    public void Send(string message)
    {
        if (!_isWork)
            return;

        _listener.Send(message);
    }

    private bool _isWork;

    private readonly INetworkElement
        _listener,
        _broadcaster;
}