Bifrost.namespace("Web.Navigation", {
    sidebar: Bifrost.views.ViewModel.extend(function (categoryHub, globalMessenger) {
        var self = this;

        this.categories = ko.observableArray();

        categoryHub.server.getAll().continueWith(self.categories);

        categoryHub.client(function (client) {
            client.categoriesChanged = self.categories;
        });


        this.navigate = function (category) {
            globalMessenger.publish("navigateToCategory", "http://www.komplett.no?q=" + category);
        }
    })
});