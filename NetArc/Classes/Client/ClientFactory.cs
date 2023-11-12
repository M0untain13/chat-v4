using NetArc.Interfaces;

namespace NetArc.Classes.Client;

internal class ClientFactory : IClientFactory
{
	public ClientFactory(int broadcastPort, int listenPort)
	{
        _broadcastPort = broadcastPort;
        _listenPort = listenPort;
	}

	public INetworkElement CreateClient(Action<string> callback)
	{
		return new Client(callback, this);
	}

    private readonly int
        _broadcastPort,
        _listenPort;
}