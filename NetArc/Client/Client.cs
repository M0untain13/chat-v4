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
		_parser = new Parser();
        _listener = new BroadcastListener(listenPort);
        _connector = new Connector(callback, connectPort);
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
	public bool Stop() => _connector.Stop();

    /// <summary>
    /// Отправить сообщение на сервер
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(WebMessage message) => _connector.Send(_parser.CreateMessage(message));

    /// <summary>
    /// Установить новый обработчик сообщений сервера. По сути callback нужно засунуть в ConnectorTcp
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <returns> Удалось ли установить обработчик </returns>
    public void SetCallback(Action<WebMessage> callback) => _connector.SetCallback(callback);

    private readonly Parser _parser;
	private readonly BroadcastListener _listener;
	private readonly Connector _connector;
}