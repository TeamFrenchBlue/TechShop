angular.module('app')

.controller('HomeController', function ($scope, $rootScope, publicRequests) {
    $scope.items = [];

    publicRequests.getAllProducts()
    .success(function (data) {
        $scope.items = data;
    })
    .error(function (error) {
        console.log(error);
    })
});