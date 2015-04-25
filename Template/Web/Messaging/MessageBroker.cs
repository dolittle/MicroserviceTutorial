using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Bifrost.Execution;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Web.TestBench;
using IConnection = RabbitMQ.Client.IConnection;

namespace Web.Messaging
{
    [Singleton]
    public class MessageBroker : IMessageBroker
    {
        Dictionary<Type, object> _consumersByMessageType;
        Dictionary<string, Type> _messageTypesByName;

        string _topicName;
        string _consumerName;

        ConnectionFactory _connectionFactory;
        IConnection _connection;
        IModel _channel;
        QueueingBasicConsumer _consumer;

        public MessageBroker(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            var consumers = typeDiscoverer.FindMultiple(typeof(IMessageConsumer<>));
            _consumersByMessageType = consumers.ToDictionary(
                t => t.GetInterface(typeof(IMessageConsumer<>).Name).GetGenericArguments()[0], 
                t => container.Get(t));
            _messageTypesByName = typeDiscoverer.FindMultiple(typeof(Message)).ToDictionary(t => t.Name, t => t);


            _topicName = "TheTopic";
            _consumerName = "Template";

            _connectionFactory = new ConnectionFactory() 
            { 
                HostName = "mstutorial.cloudapp.net",
                UserName = "tutorial",
                Password = "tutorial"
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_topicName, "topic");
            _channel.QueueDeclare(_consumerName, true, false, false, null);
            _channel.QueueBind(_consumerName, _topicName, "*");

            _consumer = new QueueingBasicConsumer(_channel);
            _channel.BasicConsume(_consumerName, true, _consumer);

            ThreadPool.QueueUserWorkItem(Receiver);
        }


        void Receiver(object state)
        {
            var result = _consumer.Queue.Dequeue();
            try
            {
                if( !result.Redelivered )
                {
                    var messageAsString = Encoding.UTF8.GetString(result.Body);
                    var message = JsonConvert.DeserializeObject<Message>(messageAsString);
                    if (_messageTypesByName.ContainsKey(message.MessageType))
                    {
                        var messageType = _messageTypesByName[message.MessageType];
                        var concreteMessage = JsonConvert.DeserializeObject(messageAsString, messageType);
                        var consumer = _consumersByMessageType[messageType];
                        var handleMethod = consumer.GetType().GetMethod("Handle", BindingFlags.Public | BindingFlags.Instance);
                        if (handleMethod != null)
                            handleMethod.Invoke(consumer, new object[] {concreteMessage});
                    }
                }
            }
            catch
            {
                _consumer.Queue.Enqueue(result);
            }

            ThreadPool.QueueUserWorkItem(Receiver);
        }

        public IEnumerable<Type> GetMessageTypes()
        {
            return _messageTypesByName.Select(k => k.Value);
        }

        public void Send(Message message)
        {
            message.MessageType = message.GetType().Name;
            var messageAsString = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageAsString);
            _channel.BasicPublish(_topicName, "", null, body);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageBrokerHub>();
            hubContext.Clients.All.received(message);
        }
    }
}
