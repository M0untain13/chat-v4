using NetArc;
using NetArc.Server;

namespace ServerConsole
{
    internal class ServerConsole
    {
        private static bool _isStart;
        private static Server _server;

        private static readonly Dictionary<string, Action<string[]>> _commands = new()
        {
            {"exit", message => {
                _isStart = false;
                var text = string.Join(" ", message);
                if (text == string.Empty)
                    text = "Без сообщения.";
                _server.Send(new WebMessage("server", "exit", "", text));
                _server.Stop();
            }},
            {"message", message => {
                var msgList = message.ToList();
                msgList.RemoveAt(0);
                _server.Send(new WebMessage("server", "message", "", string.Join(' ', msgList)));
            }},
            {"help", _ => {
                Console.WriteLine($"Существующие команды: {string.Join(", ", _commandsNames)}");
            }}
        };

        private static readonly string[] _commandsNames = _commands.Keys.ToArray();

        private static void Main()
        {
            _server = new Server(Callback, 4899, 5650, 2000);

            if (!_server.Start())
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

        private static void Callback(string message)
        {
            Console.WriteLine(message);
        }
    }
}