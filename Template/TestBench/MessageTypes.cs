using System.Linq;
using Bifrost.Read;
using Web.Messaging;

namespace Web.TestBench
{
    public class MessageTypes : IQueryFor<MessageType>
    {
        IMessageBroker _messageBroker;

        public MessageTypes(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public IQueryable<MessageType> Query
        {
            get
            {
                return _messageBroker.GetMessageTypes().Select(t => new MessageType { 
                    Name = t.Name,
                    Properties = t.GetProperties().Select(p=>p.Name)
                }).AsQueryable();
            }
        }
    }
}
