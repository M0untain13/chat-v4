using ClientCore.Models;
using NetArc;

namespace ClientCore.Services;

public interface IClientService
{
    IClientWrapper GetClient(Action<WebMessage> callback, int connectPort, int listenPort);
}