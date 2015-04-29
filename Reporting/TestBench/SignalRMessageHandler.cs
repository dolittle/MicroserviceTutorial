using System;
using Microsoft.AspNet.SignalR;
using Web.Messaging;

namespace Web.TestBench
{
    public class SignalRMessageHandler : IMessageConsumer<Message>
    {
        public void Handle(Message message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageBrokerHub>();
            Console.WriteLine(message.ToString());
            hubContext.Clients.All.received(message);
        }
    }
}
