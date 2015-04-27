Bifrost.namespace("Web.Cart", {
    index: Bifrost.views.ViewModel.extend(function (cartHub) {
        var self = this;

        this.cart = ko.observable({
            items: 0,
            total: 0
        });

        cartHub.server.getCart().continueWith(self.cart);

        cartHub.client(function (client) {
            client.cartChanged = function (cart) {
                self.cart(cart);
            };
        });

        
        
    })
});