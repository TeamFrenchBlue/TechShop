angular.module('app')

.controller('ItemController', function ($scope, $route, $rootScope, publicRequests, notyService) {
    $rootScope.category = $route.current.params['categoryName'];
    $scope.currentItemId = $route.current.params['id'];
    $scope.item = [];

    publicRequests.getProductById($scope.currentItemId)
    .success(function (data) {
        console.log(data);
        $scope.item = data[0];
    })
    .error(function (error) {
        notyService.error(error.Message + "\n" + "error.MessageDetail");
        console.log(error);
    })


});