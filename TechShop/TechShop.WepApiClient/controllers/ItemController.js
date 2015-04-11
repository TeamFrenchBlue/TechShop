angular.module('app')

.controller('ItemController', function ($scope, $route, $rootScope) {

    $rootScope.category = $route.current.params['categoryName'];
    $scope.currentItemId = $route.current.params['id'];
    console.log($scope.currentItemId);
});