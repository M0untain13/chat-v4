using NetArc;
using NetArc.Interfaces;

namespace server
{
    internal class Program
    {
        private static INetworkElement _server = null!;

        static void Main(string[] args)
        {
            var factory = Facade.CreateServerFactory(8000, 8001);
            _server = factory.CreateServer(Callback);
            _server.Start();

            var message = string.Empty;
            while (message != "exit")
            {
                message = Console.ReadLine() ?? string.Empty;

                if (message == "exit")
                {
                    _server.Stop();
                }
                else
                {
                    _server.Send(message);
                }
            }
        }

        private static void Callback(string message)
        {
            Console.WriteLine(message);
            _server.Send(message);
        }
    }
}