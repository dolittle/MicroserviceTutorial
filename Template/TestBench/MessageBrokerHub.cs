using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using Web.Messaging;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web.TestBench
{
    public class MessageBrokerHub : Hub
    {
        IMessageBroker _messageBroker;
        JsonSerializer _serializer;

        public MessageBrokerHub(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;

            _serializer = new JsonSerializer();
            _serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void Send(string messageAsString)
        {
            var messageHash = JObject.Parse(messageAsString);
            
            var messageTypeAsString = messageHash["messageType"].Value<string>();
            var messageType = _messageBroker.GetMessageTypes().SingleOrDefault(t => t.Name == messageTypeAsString);
            if (messageType != null)
            {
                var message = messageHash.ToObject(messageType, _serializer) as Message;
                _messageBroker.Send(message);
            }
        }
    }
}
