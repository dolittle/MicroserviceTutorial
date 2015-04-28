using Microsoft.AspNet.SignalR;
using Web.Messaging;

namespace Web.TestBench
{
    public class SignalRMessageHandler : IMessageConsumer<Message>
    {
        public void Handle(Message message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageBrokerHub>();
            hubContext.Clients.All.received(message);
        }
    }
}
