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

    .when('/user/settings', {
        templateUrl: 'views/user/settings-view.html',
        controller: 'UserSettingController'
    })

    .when('/admin/settings', {
        templateUrl: 'views/admin/settings-view.html',
        controller: 'AdminSettingsController'
    })

    .when('/admin/settings/products', {
        templateUrl: 'views/admin/settings-product-view.html',
        controller: 'AdminSettingsProductController'
    })

     .when('/admin/settings/:settingsName', {
         templateUrl: 'views/admin/settings-view.html',
         controller: 'AdminSettingsController'
     })




}])


.controller('LogoutController', function ($location, userSession, notyService) {
    userSession.logout();
    $location.path('/');
    notyService.success('Logout successfully.');
})

.run(function ($rootScope, $location, userSession, publicRequests) {
    $rootScope.username = "";
    $rootScope.isLogin = false;
    $rootScope.isAdmin = "";
    $rootScope.category = "";
    $rootScope.subLocation = "";

    $rootScope.getCategories = function () {
        publicRequests.getAllCategories()
        .success(function (data) {
            $rootScope.categories = data;
            console.log(data);
        })
        .error(function (error) {
            console.log(error);
        })
    }

    $rootScope.getCategories();



    $rootScope.$on('$locationChangeStart', function (event) {
        if (userSession.getCurrentUser()) {
            $rootScope.username = userSession.getCurrentUser().userName;
            $rootScope.isAdmin = userSession.isAdmin();
            $rootScope.isLogin = true;

        } else {
            $rootScope.username = "";
            $rootScope.isAdmin = "";
            $rootScope.isLogin = false;
        }

        console.log($rootScope.isAdmin);


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