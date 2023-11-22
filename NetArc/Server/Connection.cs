using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetArc.Server;

/// <summary>
/// Обработчик соединения с клиента
/// </summary>
internal class Connection
{
    /// <summary>
    /// Создать соединение
    /// </summary>
    /// <param name="client"> Сокет клиента </param>
    /// <param name="callback"> Метод для обработки сообщений клиента </param>
    
    private static int SERVERUDPPORT = 8000;
    private Socket _UdpSocket;
    public Connection(Socket client, Action<WebMessage> callback)
    {
        _UdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _UdpSocket.Bind(new IPEndPoint(IPAddress.Any, SERVERUDPPORT));

        Parser parser_mes = new Parser();

        Task UDPThread = new Task(() =>
        {
            try
            {
                while (true)
                {
                    EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                    //int bytesRead = _UdpSocket.ReceiveFrom(buffer, ref clientEP);

                    //string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //callback(parser_mes.ParseMessage(message));
                    //Console.WriteLine("Получено от клиента: " + message);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error while receiving the message: {ex.Message}");
            }
        });
    }

   


    /// <summary>
    /// Запустить прослушку входящих соединений с данного клиента
    /// </summary>
    public bool Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить работу с клиентом
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public bool Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщение клиенту
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(string message)
    {
        throw new NotImplementedException();
    }
}