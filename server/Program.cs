namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.StartListen(Console.WriteLine);
            while (true)
            {
                var message = Console.ReadLine() ?? "";
                server.Send(message);
            }
        }
    }
}