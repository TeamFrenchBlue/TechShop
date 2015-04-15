angular.module('app')

.factory('adminRequests', function (baseUrl, requester, userSession) {

    var url = "";
    var data = {};
    var headers = "";

    var adminRequests = {
        edit: function (controllerName, data) {
            url = baseUrl + controllerName + '/edit/' + data.Id;

            headers = {
                Authorization: 'Bearer ' + userSession.getToken()
            };

            return requester.put(url, data, headers);
        },

        delete: function (controllerName, id) {
            url = baseUrl + controllerName + '/delete/' + id;

            headers = {
                Authorization: 'Bearer ' + userSession.getToken()
            };

            return requester.delete(url, headers);
        },

        add: function (controllerName, data) {
            url = baseUrl + controllerName + '/add/' + data.Id;

            headers = {
                Authorization: 'Bearer ' + userSession.getToken()
            };

            return requester.post(url, data, headers);
        },

    };

    return adminRequests;
});