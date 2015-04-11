angular.module('app')

.controller('LoginController', function ($scope, $rootScope, $location, userRequests, userSession, notyService) {

    if (userSession.getCurrentUser()) {
        $location.path('/');
    }

    $scope.username = 'asdasd';
    $scope.password = 'asdasd';

    $scope.login = function login() {
        userRequests.login($scope.username, $scope.password)
        .success(function (data) {
            userSession.login(data);
            $location.path('/');
            notyService.success("Login successfully.");
        })
        .error(function (error) {
            notyService.error("Login error: " + error.Message);
            console.log(error);
        });
    };
});