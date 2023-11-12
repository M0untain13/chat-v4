using NetArc;
using NetArc.Interfaces;

namespace ClientConsole
{
    internal class Program
    {
        private static INetworkElement _client = null!;

        static void Main()
        {
            var factory = Facade.CreateServerFactory(8000, 8001);
            _client = factory.CreateServer(Callback);
            _client.Start();

            var message = string.Empty;
            while (message != "exit")
            {
                message = Console.ReadLine() ?? string.Empty;

                if (message == "exit")
                {
                    _client.Stop();
                }
                else
                {
                    _client.Send(message);
                }
            }
        }

        private static void Callback(string message)
        {
            Console.WriteLine(message);
        }
    }
}