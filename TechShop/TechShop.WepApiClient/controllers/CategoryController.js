angular.module('app')

.controller('CategoryController', function ($scope, $route, $rootScope, publicRequests) {
    $rootScope.category = $route.current.params['categoryName'];

    $scope.trades = [];
    $scope.items = [];

    publicRequests.getAllTradesByCategory($rootScope.category)
    .success(function (data) {
        $scope.trades = data;
    })
    .error(function (error) {
        console.log(error)
    });

    publicRequests.getAllProductsByCategory($rootScope.category)
    .success(function (data) {
        $scope.items = data;
        console.log(data)
    })
    .error(function (error) {
        console.log(error)
    });


});