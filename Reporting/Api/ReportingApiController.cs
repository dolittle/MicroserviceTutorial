using System;
using System.Data.Entity;
using System.Web.Http;
using Web.Messages;
using Web.Messaging;
using Web.Models;

namespace Web.Api
{
    public class ReportingApiController : ApiController
    {

        private OrderDbContext dbContext;

        public ReportingApiController()
        {
            dbContext = new OrderDbContext();
        }

        [HttpGet]
        public string Get(String status)
        {
            var status1 = OrderStatus.PLACED;
            if (OrderStatus.PLACED.ToString().Equals(status))
            {
                status1 = OrderStatus.PLACED;
            }
            else if (OrderStatus.PICKING_STARTED.ToString().Equals(status))
            {
                status1 = OrderStatus.PICKING_STARTED;
            }
            else if (OrderStatus.PICKED.ToString().Equals(status))
            {
                status1 = OrderStatus.PICKED;
            }
            else if (OrderStatus.CANCELED.ToString().Equals(status))
            {
                status1 = OrderStatus.CANCELED;
            }
            else if (OrderStatus.PACKAGED.ToString().Equals(status))
            {
                status1 = OrderStatus.PACKAGED;
            }
            else if (OrderStatus.IN_TRANSIT.ToString().Equals(status))
            {
                status1 = OrderStatus.IN_TRANSIT;
            }
            else if (OrderStatus.DELIVERED.ToString().Equals(status))
            {
                status1 = OrderStatus.DELIVERED;
            }
            return dbContext.CountOrders(status1).ToString();

        }
    }
}
