Bifrost.namespace("Web.Status", {
    index: Bifrost.views.ViewModel.extend(function (statusHub) {
        var self = this;

        this.systems = ko.observableArray();

        statusHub.client(function (client) {
            client.statusChanged = function (systemWithStatus) {
                self.systems().forEach(function (existing) {
                    if (existing.system == systemWithStatus.system) {
                        existing.status = systemWithStatus.status;
                    }
                });
            };
        });

        statusHub.server.getSystemsAndStatuses().continueWith(self.systems);
    })
});