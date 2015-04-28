using Web.Messaging;

namespace Web.Messages
{
    public class PerformingMaintenance : Message
    {
        public string SystemId { get; set; }
    }
}
