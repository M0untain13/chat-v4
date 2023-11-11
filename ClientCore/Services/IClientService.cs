namespace ClientCore.Services;

public interface IClientService
{
    /// <summary>
    /// Присоединиться к серверу
    /// </summary>
    /// <param name="callback"> Функция получения ответа </param>
    void Connect(Action<string> callback);

    /// <summary>
    /// Отключить соединение
    /// </summary>
    void Disconnect();

    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    void Send(string message);
}