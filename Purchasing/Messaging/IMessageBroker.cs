using System;
using System.Collections.Generic;

namespace Web.Messaging
{
    public interface IMessageBroker
    {
        IEnumerable<Type> GetMessageTypes();

        void Send(Message message);
    }
}
