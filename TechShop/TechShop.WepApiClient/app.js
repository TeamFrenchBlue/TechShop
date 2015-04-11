angular.module('app', [
    'ngRoute',
    'ngAnimate',
    'ui.bootstrap'
])

.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider

    .when('/', {
        templateUrl: 'views/home-view.html',
        controller: 'HomeController'
    })

    .when('/register', {
        templateUrl: 'views/register-view.html',
        controller: 'RegisterController'
    })

    .when('/login', {
        templateUrl: 'views/login-view.html',
        controller: 'LoginController'
    })

     .when('/logout', {
         templateUrl: 'views/login-view.html',
         controller: 'LogoutController'
     })

    .when('/category/:categoryName', {
        templateUrl: 'views/category-view.html',
        controller: 'CategoryController'
    })

    .when('/category/:categoryName/:id', {
        templateUrl: 'views/item-view.html',
        controller: 'ItemController'
    })


}])


.controller('LogoutController', function ($location, userSession, notyService) {
    userSession.logout();
    $location.path('/');
    notyService.success('Logout successfully.');
})

.run(function ($rootScope, $location, userSession) {
    $rootScope.username = "";
    $rootScope.isLogin = false;
    $rootScope.isAdmin = "";
    $rootScope.category = "";
    $rootScope.subLocation = "";

    $rootScope.$on('$locationChangeStart', function (event) {
        if (userSession.getCurrentUser()) {
            $rootScope.username = userSession.getCurrentUser().userName;
            $rootScope.isAdmin = userSession.getCurrentUser().isAdmin;
            $rootScope.isLogin = true;

        } else {
            $rootScope.username = "";
            $rootScope.isAdmin = "";
            $rootScope.isLogin = false;
        }

        if ($location.path().indexOf("/user/") != -1 && !userSession.getCurrentUser()) {
            // Authorization check: anonymous site visitors cannot access user routes
            $location.path("/");
            $rootScope.isLogin = false;
        }

        if (userSession.isAdmin() && $location.path().indexOf("/user/") != -1) {
            $location.path('/admin/home');
            console.log("admin");
        }
    });
})