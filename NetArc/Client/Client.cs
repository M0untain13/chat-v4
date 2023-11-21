namespace NetArc.Client;

/// <summary>
/// Стартовая точка для создания клиента
/// </summary>
public class Client
{
	/// <summary>
	/// Создать клиент, создать соединитель и прослушку вещаний
	/// </summary>
	/// <param name="callback"> Метод для обработки сообщений сервера </param>
	/// <param name="connectPort"> Порт для соединения к серверу </param>
	/// <param name="listenPort"> Порт прослушки вещаний </param>
	public Client(Action<WebMessage> callback, int connectPort, int listenPort)
    {
        _listener = new ClientUdp(listenPort);
        _connector = new ClientTcp(callback, connectPort);
    }

	/// <summary>
	/// Начать попытку соединения с сервером (Сначала слушать IP сервера с вещателя, а затем сделать попытку соединения с сервером по этому IP)
	/// </summary>
	public bool Start()
    {
        var ip = _listener.GetServerIp();
        return _connector.Start(ip);
    }

	/// <summary>
	/// Остановить работу клиента
	/// </summary>
	public bool Stop()
	{
		return _connector.Stop();
	}

	/// <summary>
	/// Отправить сообщение на сервер
	/// </summary>
	/// <param name="message"> Текст сообщения </param>
	public bool Send(WebMessage message)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Установить новый обработчик сообщений сервера. По сути callback нужно засунуть в ConnectorTcp
	/// </summary>
	/// <param name="callback"> Метод для обработки сообщений сервера </param>
	/// <returns> Удалось ли установить обработчик </returns>
	public bool SetCallback(Action<WebMessage> callback)
	{
		throw new NotImplementedException();
	}

    private Parser _parser;
	private ClientUdp _listener;
	private ClientTcp _connector;
}