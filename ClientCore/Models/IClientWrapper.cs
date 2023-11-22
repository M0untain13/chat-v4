using NetArc;
using NetArc.Client;

namespace ClientCore.Models;

public interface IClientWrapper
{
    bool Start();

    bool Stop();

    bool Send(WebMessage message);

    void SetCallback(Action<WebMessage> callback);
}