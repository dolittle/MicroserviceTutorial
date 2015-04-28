Bifrost.namespace("Web.Navigation", {
    index: Bifrost.views.ViewModel.extend(function (globalMessenger) {
        var self = this;
        this.navigationItems = [
            { name: "TestBench", link: "#TestBench/index", target: "_self" },
            { name: "Orders", link: "http://www.vg.no", target: "contentFrame" },
            { name: "Warehouse", link: "http://www.db.no", target: "contentFrame" },
            { name: "Packaging", link: "#", target: "contentFrame" },
            { name: "Delivery", link: "#", target: "contentFrame" },
        ];
        this.cartLink = "https://www.komplett.no/cart";
        this.searchLink = "https://www.komplett.no/search";

        this.currentNavigationItem = ko.observable(self.navigationItems[0]);

        

        this.select = function (navigationItem) {
            self.currentNavigationItem(navigationItem);
            globalMessenger.publish("navigated", navigationItem);
        };
        globalMessenger.publish("navigated", self.navigationItems[0]);


        this.searchQuery = ko.observable();

        this.submitSearch = function () {
            var navigationItem = {
                name: "search",
                link: self.searchLink + "?q=" + self.searchQuery(),
                target: "contentFrame"
            };
            self.select(navigationItem);
        };

        this.goToCart = function () {
            var navigationItem = {
                name: "cart",
                link: self.cartLink,
                target: "contentFrame"
            };
            self.select(navigationItem);
        };
    })
});