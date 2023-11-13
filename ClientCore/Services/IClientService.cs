using ClientCore.Models;

namespace ClientCore.Services;

public interface IClientService
{
    IClientWrapper GetClient(Action<string> callback, int connectPort, int listenPort);
}