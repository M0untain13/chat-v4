using NetArc;

namespace TestForNetArc
{
    public class TestForParse
    {
        [Fact]
        public void TestForWrapping()
        {
            var message = new WebMessage("client", "message", "Димон", "Привет");

            var parser = new Parser();

            var newMessage = parser.ParseMessage(parser.CreateMessage(message));

            Assert.Multiple(() =>
            {
                Assert.Equal(message.sender, newMessage.sender);
                Assert.Equal(message.type, newMessage.type);
                Assert.Equal(message.name, newMessage.name);
                Assert.Equal(message.text, newMessage.text);
            });
        
                
        }
    }
}