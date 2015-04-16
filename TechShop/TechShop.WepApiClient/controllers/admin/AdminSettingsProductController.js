angular.module('app')

.controller('AdminSettingsProductController', function ($scope, $rootScope, $route, publicRequests, adminRequests, notyService) {
    $scope.settings = 'products';
    $scope.datas = [];
    $scope.currentObj = {};

    function getData() {
        publicRequests.getAllProducts()
       .success(function (data) {
           $scope.datas = data;
       }).
       error(function (error) {
           console.log(error);
       });
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
        $route.reload();

    };

    $scope.delete = function () {

        adminRequests.delete($scope.settings, $scope.currentObj['Id'])
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

        $('#deleteModal').modal('hide');
        $route.reload();

    };

    $scope.add = function () {

        adminRequests.add($scope.settings, $scope.currentObj)
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

        $('#addModal').modal('hide');
        $route.reload();

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
        $scope.currentObj = {
            Id: 0,
            Name: "",
            Position: ""
        }
    }

    $scope.fileSelected = function (fileInputField) {
        $scope.currentObj.fileName = fileInputField.value;
        delete $scope.currentObj.ImageUrl;
        var file = fileInputField.files[0];
        if (file.type.match(/image\/.*/)) {
            var reader = new FileReader();
            reader.onload = function () {
                $scope.currentObj.ImageUrl = reader.result;
                $scope.$apply();
            };
            reader.readAsDataURL(file);
        } else {
            delete $scope.currentObj.ImageUrl;
            $scope.$apply();
            notyService.error("File your are trying to upload isn't a picture!");
        }
    }



    getData();
});