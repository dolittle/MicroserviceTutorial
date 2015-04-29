using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Web.Messages;
using Web.Models;
using Sample = Web.Models.Sample;

namespace Web.Api
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> OrderDb { get; set; }

        public OrderDbContext()
            : base("DefaultConnection")
        {
        }

        public Order InsertOrUpdateOrder(string ordernumber,OrderStatus status)
        {
            var order = new Order() ;
            order.OrderNumber = ordernumber;
            order.Orderstatus = status;
            
            var existingorder=OrderDb.SingleOrDefault(z=>z.OrderNumber==ordernumber);
            if (existingorder != null)
            {
                OrderDb.Remove(existingorder);
            }

            OrderDb.Add(order);
            SaveChanges();
            return order;
        }
        
        public int CountOrders(OrderStatus status)
        {
            var orders = OrderDb.Where(z=>z.Orderstatus==status).ToList();
            return orders.Count;
        }
    }
}
