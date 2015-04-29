
using System;

namespace Web.Models
{
    public enum OrderStatus
    {
        PLACED,
        PICKING_STARTED,
        PICKED,
        CANCELED,
        PACKAGED,
        IN_TRANSIT,
        DELIVERED
    }
    public class Order
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; }
        public OrderStatus Orderstatus { get; set; }
    }
}
