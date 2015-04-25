using Web.Messages;
using Web.Messaging;

namespace Web
{
    public class TestHandler : IMessageConsumer<MaterialAdded>
    {
        public void Handle(MaterialAdded message)
        {
            var i = 0;
            i++;
        }
    }
}
