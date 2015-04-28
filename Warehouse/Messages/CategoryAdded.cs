using Web.Messaging;

namespace Web.Messages
{
    public class CategoryAdded : Message
    {
        public string Category { get; set; }
    }
}
