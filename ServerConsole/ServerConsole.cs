using NetArc;
using NetArc.Server;

namespace ServerConsole
{
    internal class ServerConsole
    {
        // TODO: Создать сервер, запустить сервер, отправлять сообщения клиентам
        private static void Main()
        {
            var server = new Server(Callback, 8000, 8001, 2000);
        }

        // TODO: Метод для получения сообщений с клиентов
        private static void Callback(WebMessage message)
        {
            Console.WriteLine(message);
        }
    }
}