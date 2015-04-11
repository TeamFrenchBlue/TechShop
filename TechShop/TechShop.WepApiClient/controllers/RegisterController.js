angular.module('app')

.controller('RegisterController', function ($scope, $rootScope, $location, userRequests, userSession, notyService) {

    if (userSession.getCurrentUser()) {
        $location.path('/');
    }

    $scope.userData = {};

    $scope.userData.name = "asdasd";
    $scope.userData.username = "asdasd";
    $scope.userData.password = "asdasd";
    $scope.userData.email = "asdasd@asd.bg";
    $scope.userData.confirmPassword = "asdasd";


    $scope.register = function login() {
        console.log($scope.userData);
        userRequests.register($scope.userData)
        .success(function (data) {
            userSession.login(data);
            console.log(data);
            notyService.success("Register successfully.");
            $location.path('/');
            notyService.success("Login successfully.");
        })
        .error(function (error) {
            notyService.error("Login error: " + error);
            console.log(error);
        });
    };
});