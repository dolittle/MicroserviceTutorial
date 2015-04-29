Bifrost.namespace("Web.TestBench", {
    index: Bifrost.views.ViewModel.extend(function (messageTypes, messageBrokerHub) {
        var self = this;
        this.selectedMessageType = ko.observable({ properties: [] });
        this.messageTypes = messageTypes.all();

        this.values = {};

        this.messages = ko.observableArray();

        this.selectedMessageType.subscribe(function (messageType) {
            if (Bifrost.isNullOrUndefined(messageType)) return;
            self.values = {
                messageType: messageType.name
            };
            messageType.properties.forEach(function (property) {
                self.values[property] = ko.observable();
            });
        });

        messageBrokerHub.client(function (client) {
            client.received = function (message) {
                var messageAsJson = ko.toJSON(message);
                self.messages.push(messageAsJson);
            };
        });
        
        this.sendMessage = function () {
            var values = ko.toJSON(self.values);
            messageBrokerHub.server.send(values);
        };


    })
});