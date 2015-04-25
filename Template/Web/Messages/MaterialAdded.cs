using Web.Messaging;

namespace Web.Messages
{
    public class MaterialAdded : Message
    {
        public string MaterialNumber { get; set; }
        public string Category { get; set; }
    }
}
