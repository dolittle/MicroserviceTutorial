using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Bifrost.Execution;
using Bifrost.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using IConnection = RabbitMQ.Client.IConnection;

namespace Web.Messaging
{
    [Singleton]
    public class MessageBroker : IMessageBroker
    {
        Dictionary<Type, List<object>> _consumersByMessageType = new Dictionary<Type, List<object>>();
        Dictionary<string, Type> _messageTypesByName;

        string _topicName;
        string _consumerName;

        ConnectionFactory _connectionFactory;
        IConnection _connection;
        IModel _channel;
        QueueingBasicConsumer _consumer;

        public MessageBroker(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            var consumerTypes = typeDiscoverer.FindMultiple(typeof(IMessageConsumer<>));
            consumerTypes.ForEach(t =>
            {
                var messageType = t.GetInterface(typeof(IMessageConsumer<>).Name).GetGenericArguments()[0];
                List<object> consumers;
                if (!_consumersByMessageType.ContainsKey(messageType))
                {
                    consumers = new List<object>();
                    _consumersByMessageType[messageType] = consumers;
                }
                else
                {
                    consumers = _consumersByMessageType[messageType];
                }
                consumers.Add(container.Get(t));
            });
            _messageTypesByName = typeDiscoverer.FindMultiple(typeof(Message)).ToDictionary(t => t.Name, t => t);

            _topicName = ConfigurationManager.AppSettings["RabbitMQTopic"];
            _consumerName = ConfigurationManager.AppSettings["RabbitMQQueue"];

            _connectionFactory = new ConnectionFactory() 
            { 
                HostName = ConfigurationManager.AppSettings["RabbitMQServer"],
                UserName = ConfigurationManager.AppSettings["RabbitMQUsername"],
                Password = ConfigurationManager.AppSettings["RabbitMQPassword"]
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
            try
            {
                var result = _consumer.Queue.Dequeue();
                try
                {
                    if (!result.Redelivered)
                    {
                        var messageAsString = Encoding.UTF8.GetString(result.Body);

                        var messageHash = JObject.Parse(messageAsString);
                        var messageTypeValue = messageHash["messageType"] ?? messageHash["MessageType"];

                        var messageTypeAsString = messageTypeValue.Value<string>();
                        if (_messageTypesByName.ContainsKey(messageTypeAsString))
                        {
                            var messageType = _messageTypesByName[messageTypeAsString];
                            var concreteMessage = JsonConvert.DeserializeObject(messageAsString, messageType);

                            var consumersByType = _consumersByMessageType.Where(k => k.Key.IsAssignableFrom(messageType)).Select(k => k.Value);
                            consumersByType.ForEach(c => c.ForEach(consumer =>
                            {
                                var handleMethod = consumer.GetType().GetMethod("Handle", BindingFlags.Public | BindingFlags.Instance);
                                if (handleMethod != null)
                                    handleMethod.Invoke(consumer, new object[] { concreteMessage });
                            }));
                        }
                    }
                }
                catch
                {
                    _consumer.Queue.Enqueue(result);
                }

                ThreadPool.QueueUserWorkItem(Receiver);
            }
            catch { }
        }

        public IEnumerable<Type> GetMessageTypes()
        {
            return _messageTypesByName.Select(k => k.Value);
        }

        public void Send(Message message)
        {
            message.TimeStamp = DateTime.UtcNow;
            message.MessageType = message.GetType().Name;
            var messageAsString = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageAsString);
            _channel.BasicPublish(_topicName, message.MessageType, null, body);
        }
    }
}
