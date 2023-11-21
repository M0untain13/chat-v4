using NetArc;
using NetArc.Client;

namespace ClientConsole
{
    internal class ClientConsole
    {
        private static string _name = "anon";
        private static bool _isStart;

        // TODO: Создать клиент, запустить клиент, отправлять сообщения серверу
        private static void Main()
        {
            var client = new Client(Callback, 8000, 8001);

            if (!client.Start())
            {
                Console.WriteLine("Клиент не смог подключиться к серверу...");
            }
            else
            {
                Console.WriteLine("Подключение успешно!");
                _isStart = true;
                while (_isStart)
                {
                    var message = (Console.ReadLine()!).Split();
                    switch (message[0])
                    {
                        case "exit":
                            _isStart = false;
                            client.Send(new WebMessage("client", "exit", _name, ""));
                            break;
                        case "auth":
                            client.Send(new WebMessage("client", "auth", _name, message[1]));
                            break;
                        case "message":
                            message[0] = "";
                            client.Send(new WebMessage("client", "message", _name, string.Join(" ", message)));
                            break;
                        default:
                            Console.WriteLine("Введена неизвестная команда...");
                        break;
                    }
                }
            }
        }

        // TODO: Метод для получения сообщений с сервера
        private static void Callback(WebMessage message)
        {
            switch (message)
            {
                case { sender: "server", type: "auth", text: "accept" }:
                    Console.WriteLine("Авторизация успешна!");
                    _name = message.name;
                    break;
                case { sender: "server", type: "exit" }:
                    Console.WriteLine($"Сервер завершил свою работу. {message.text}.");
                    _isStart = false;
                    break;
                case { sender: "client", type: "message" }:
                    Console.WriteLine($"{message.name}: {message.text}");
                    break;
            }
        }
    }
}