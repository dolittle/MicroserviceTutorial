using System;
namespace Web.Messaging
{
    public abstract class Message
    {
        public DateTime TimeStamp { get; set; }
        public string MessageType { get; set; }
    }
}
