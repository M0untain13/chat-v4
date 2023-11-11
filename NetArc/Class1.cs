namespace NetArc
{
    public interface IArcNode
    {
        public void Start();
        public void Stop();
        public void Send(string message);
    }

    public class Server : IArcNode
    {
        Server()
        {

        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }

    public static class ArcFactory
    {
        static IArcNode GetServer() 
        {
            return new Server();
        }
    }
}