angular.module('app')

.controller('CategoryController', function ($scope, $route, $rootScope) {

    $rootScope.category = $route.current.params['categoryName'];
     
});