using System;
using Microsoft.AspNet.SignalR;
using Web.Messages;
using Web.Messaging;

namespace Web.Purchasing
{
    public class PurchasingHub : Hub
    {
        IMessageBroker _messageBroker;

        public PurchasingHub(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public void Register(Material material)
        {
            _messageBroker.Send(new MaterialAdded
            {
                MaterialNumber = Guid.NewGuid().ToString(),
                Vendor = material.Vendor,
                Name = material.Name,
                Description = material.Description,
                Category = material.Category
            });
        }
    }
}
