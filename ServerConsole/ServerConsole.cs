using NetArc;
using NetArc.Client;
using NetArc.Server;
using System.Xml.Linq;

namespace ServerConsole
{
    internal class ServerConsole
    {
        private static bool _isStart;

        private static void Main()
        {
            var server = new Server(Callback, 8000, 8001, 2000);

            if (!server.Start())
            {
                Console.WriteLine("Сервер не смог запуститься...");
            }
            else
            {
                Console.WriteLine("Запуск успешен!");
                _isStart = true;
                while (_isStart)
                {
                    var message = (Console.ReadLine()!).Split();
                    switch (message[0])
                    {
                        case "exit":
                            _isStart = false;
                            var text = string.Join(" ", message);
                            if (text == string.Empty)
                                text = "Без сообщения.";
                            server.Send(new WebMessage("server", "exit", "", text));
                            server.Stop();
                            break;
                        default:
                            Console.WriteLine("Введена неизвестная команда...");
                            break;
                    }
                }
            }
        }

        private static void Callback(string message)
        {
            Console.WriteLine(message);
        }
    }
}