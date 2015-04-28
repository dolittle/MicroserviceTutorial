Bifrost.namespace("Web.Navigation", {
    sidebar: Bifrost.views.ViewModel.extend(function (categoryHub) {
        var self = this;

        this.categories = ko.observableArray();

        categoryHub.server.getAll().continueWith(self.categories);

        categoryHub.client(function (client) {
            client.categoriesChanged = self.categories;
        });
    })
});