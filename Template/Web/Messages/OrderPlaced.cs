
using System.Collections.Generic;
using Web.Messaging;
namespace Web.Messages
{
    public class OrderPlaced : Message
    {
        public string OrderNumber { get; set; }

        public IEnumerable<MaterialInOrder> Materials { get; set; }
    }
}
