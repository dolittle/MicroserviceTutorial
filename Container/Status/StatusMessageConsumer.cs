using Microsoft.AspNet.SignalR;
using Web.Messages;
using Web.Messaging;

namespace Web.Status
{
    public class StatusMessageConsumer : IMessageConsumer<Message>
    {
        public void Handle(Message message)
        {
            var systemWithStatus = new SystemWithStatus();
            if (message is PerformingMaintenance) systemWithStatus = new SystemWithStatus { System = ((PerformingMaintenance)message).SystemId, Status = "Maintenance"};
            if (message is MaintenancePerformed) systemWithStatus = new SystemWithStatus { System = ((MaintenancePerformed)message).SystemId, Status = "MaintenanceDone" };

            GlobalHost.ConnectionManager.GetHubContext<StatusHub>().Clients.All.statusChanged(systemWithStatus);
        }
    }
}