angular.module('app')

.controller('AdminSettingsController', function ($scope, $rootScope, $location, $route, publicRequests, adminRequests, notyService) {
    $scope.settings = $route.current.params['settingsName'];
    $scope.datas = [];
    $scope.currentObj = {};

    function getData() {
        switch ($scope.settings) {
            case 'categories':
                publicRequests.getAllCategories()
                .success(function (data) {
                    $scope.datas = data;
                }).
                error(function (error) {
                    console.log(error);
                });
                break;
            case 'trades':
                publicRequests.getAllTrades()
                .success(function (data) {
                    $scope.datas = data;
                }).
                error(function (error) {
                    console.log(error);
                });
                break;
            case 'products':
                publicRequests.getAllProducts()
               .success(function (data) {
                   $scope.datas = data;
               }).
               error(function (error) {
                   console.log(error);
               });
                break;
            default:
        }
    }

    $scope.showModalEdit = function (id) {
        getCurrentObjById(id);
        $('#editModal').modal('show');
    };

    $scope.showModalDelete = function (id) {
        getCurrentObjById(id);
        $('#deleteModal').modal('show');
    };

    $scope.showModalAdd = function () {
        getCurrentObjEmpty();
        $('#addModal').modal('show');
    };

    $scope.edit = function () {

        adminRequests.edit($scope.settings, $scope.currentObj)
        .success(function (data) {
            console.log(data);
            notyService.success(data);
        })
        .error(function (error) {
            var errorMessage = error.Message == null ? error : error.Message
            notyService.error(errorMessage);
            console.log(error);
        })

        if ($scope.settings == "categories") {
            $rootScope.getCategories();
        }

        $('#editModal').modal('hide');
    };

    $scope.delete = function () {

        adminRequests.delete($scope.settings, $scope.currentObj['Id'])
        .success(function (data) {
            console.log(data);
            notyService.success(data);

            if ($scope.settings == "categories") {
                $rootScope.getCategories();
            }

            var index = $scope.datas.indexOf($scope.currentObj);
            $scope.datas = $scope.datas.filter(function (value, index) {
                return value !== $scope.currentObj;
            })
        })
        .error(function (error) {
            var errorMessage = error.Message == null ? error : error.Message
            notyService.error(errorMessage);
            console.log(error);
        });

        $('#deleteModal').modal('hide');

    };

    $scope.add = function () {

        adminRequests.add($scope.settings, $scope.currentObj)
        .success(function (data) {
            console.log(data);

            notyService.success(data);
            if ($scope.settings == "categories") {
                $rootScope.getCategories();
            }

            $scope.datas.push($scope.currentObj);
        })
        .error(function (error) {
            var errorMessage = error.Message == null ? error : error.Message
            notyService.error(errorMessage);
            console.log(error);
        });

        $('#addModal').modal('hide');

    };

    function getCurrentObjById(id) {
        $scope.datas.forEach(function (element) {
            if (element.Id == id) {
                $scope.currentObj = element;
                return;
            }
        });
    };

    function getCurrentObjEmpty() {

        switch ($scope.settings) {
            case 'categories':
                $scope.currentObj = {
                    Id: 0,
                    Name: "",
                    Position: ""
                }
                break;
            case 'trades':
                $scope.currentObj = {
                    Id: 0,
                    Name: "",
                    Position: ""
                }
                break;
            case 'products':
                $scope.currentObj = {
                    Id: 0,
                    Name: "",
                    Position: ""
                }
                break;
            default:
        }
    }

    getData();
});