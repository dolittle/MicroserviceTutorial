using System;
using Web.Messaging;

namespace Web.Messages
{
    public class OrderPackaged : Message
    {
        public string OrderNumber { get; set; }
    }
}
