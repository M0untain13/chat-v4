

using NetArc;

namespace TestForNetArc
{
    public class TestForTag
    {
        [Fact]
        public void TestForWrapping()
        {
            string 
                name = "Димон",
                text = "Пошли бухать!";

            string
                name_r = "",
                text_r = "";

            string message = $"{Tag.MESSAGE}{Tag.Wrap(name, Tag.NAME_S, Tag.NAME_E)}{Tag.Wrap(text, Tag.TEXT_S, Tag.TEXT_E)}";

            if (message.Contains(Tag.MESSAGE))
            {
                message = message.Replace(Tag.MESSAGE, "");

                name_r = Tag.ParseWrap(message, Tag.NAME_S, Tag.NAME_E);
                text_r = Tag.ParseWrap(message, Tag.TEXT_S, Tag.TEXT_E);
            }

            Assert.Equal(name, name_r);
            Assert.Equal(text, text_r);
        }
    }
}