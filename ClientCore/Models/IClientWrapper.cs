using NetArc;
using NetArc.Client;

namespace ClientCore.Models;

public interface IClientWrapper
{
    bool Start();

    bool Stop();

    bool Send(WebMessage message);

    bool SetCallback(Action<WebMessage> callback);
}