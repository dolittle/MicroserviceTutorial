Bifrost.namespace("Web.Purchasing", {
    index: Bifrost.views.ViewModel.extend(function (purchasingHub) {
        var self = this;
        this.vendor = ko.observable();
        this.name = ko.observable();
        this.description = ko.observable();
        this.category = ko.observable();
        this.price = ko.observable();

        this.register = function () {
            purchasingHub.server.register({
                vendor: self.vendor(),
                name: self.name(),
                description: self.description(),
                category: self.category(),
                price: self.price()
            });
        }
    })
});