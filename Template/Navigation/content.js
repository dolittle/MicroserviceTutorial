Bifrost.namespace("Web.Navigation", {
    content: Bifrost.views.ViewModel.extend(function (globalMessenger) {
        var self = this;
        this.currentNavigationItem = ko.observable({ target: "_self" });

        globalMessenger.subscribeTo("navigated", function (item) {
            self.currentNavigationItem(item);
        });
    })
});