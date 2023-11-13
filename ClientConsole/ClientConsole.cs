using NetArc.Client;

namespace ClientConsole
{
    internal class ClientConsole
    {
        // TODO: Создать клиент, запустить клиент, отправлять сообщения серверу
        private static void Main()
        {
            var client = new Client(Callback, 8000, 8001);
        }

        // TODO: Метод для получения сообщений с сервера
        private static void Callback(string message)
        {
            Console.WriteLine(message);
        }
    }
}