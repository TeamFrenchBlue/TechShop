angular.module('app')

.factory('publicRequests', function (baseUrl, requester) {

    var url = "";
    var data = {};

    var publicRequests = {
        getAllCategories: function () {
            url = baseUrl + 'categories/getall';
            return requester.get(url);
        },

        getAllTrades: function () {
            url = baseUrl + 'trades/getall';
            return requester.get(url);
        },

        getAllProducts: function () {
            url = baseUrl + 'products/getall';
            return requester.get(url);
        },

        getProductById: function (id) {
            url = baseUrl + 'products/getall/?id=' + id;
            return requester.get(url);
        },

        getAllProductsByCategory: function (name) {
            url = baseUrl + 'products/getall/?CategoryName=' + name;
            return requester.get(url);
        },

        getAllTradesByCategory: function (name) {
            url = baseUrl + 'trades/GetByCategoryName/?name=' + name;
            return requester.get(url);
        }

    };


    return publicRequests;
});