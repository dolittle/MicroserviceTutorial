using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace Web.Status
{
    public class StatusHub : Hub
    {
        public IEnumerable<SystemWithStatus> GetSystemsAndStatuses()
        {
            return new[] { 
                new SystemWithStatus { System="Cart", Status="Running" },
                new SystemWithStatus { System="Catalog", Status="Running" },
                new SystemWithStatus { System="Search", Status="Running" },
                new SystemWithStatus { System="Reporting", Status="Running" },
                new SystemWithStatus { System="Purchasing", Status="Running" },
                new SystemWithStatus { System="Warehouse", Status="Running" },
                new SystemWithStatus { System="Packaging", Status="Running" },
                new SystemWithStatus { System="Delivery", Status="Running" }
            };
        }
    }
}
