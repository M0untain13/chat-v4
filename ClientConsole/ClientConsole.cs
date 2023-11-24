using NetArc;
using NetArc.Client;
using NetArc.Server;

namespace ClientConsole
{
    internal class ClientConsole
    {
        private static string _name = "anon";
        private static bool _isStart;

        private static Client _client;

        private static readonly Dictionary<string, Action<string[]>> _commands = new()
        {
            {"exit", message => {
                _isStart = false;
                _client.Send(new WebMessage("client", "exit", _name, ""));
            }},
            {"message", message => {
                var msgList = message.ToList();
                msgList.RemoveAt(0);
                _client.Send(new WebMessage("client", "message", _name, string.Join(" ", msgList)));
            }},
            {"auth", message => {
                _client.Send(new WebMessage("client", "auth", _name, message[1]));
            }},
            {"help", _ => {
                Console.WriteLine($"Существующие команды: {string.Join(", ", _commandsNames)}");
            }}
        };

        private static readonly string[] _commandsNames = _commands.Keys.ToArray();

        private static void Main()
        {
            _client = new Client(Callback, 9001, 9002);

            if (!_client.Start())
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
                    if (_commandsNames.Contains(message[0]))
                    {
                        _commands[message[0]](message);
                    }
                    else
                    {
                        Console.WriteLine("Введена неизвестная команда...");
                    }
                }
            }
        }

        private static void Callback(WebMessage message)
        {
            var msgText = "";
            switch (message)
            {
                case { sender: "server", type: "auth", text: "accept" }:
                    msgText = "Авторизация успешна!";
                    _name = message.name;
                    break;
                case { sender: "server", type: "auth", text: "denied" }:
                    msgText = "Авторизация отклонена...";
                    _name = message.name;
                    break;
                case { sender: "server", type: "exit" }:
                    msgText = $"Сервер завершил свою работу...";
                    _isStart = false;
                    break;
                case { sender: "client", type: "message" }:
                    msgText = $"{message.name}: {message.text}";
                    break;
                case { sender: "server", type: "message" }:
                    msgText = $"Server: {message.text}";
                    break;
                default:
                    return;
            }

            Console.WriteLine(msgText);
        }
    }
}