using Web.Messaging;

namespace Web.Messages
{
    public class MaintenancePerformed : Message
    {
        public string SystemId { get; set; }
    }
}
