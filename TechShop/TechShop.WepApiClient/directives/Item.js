
angular.module('app')

.directive('itemForSell', function () {
    return {
        scope: {
            datasource: '=' //Two-way data binding
        },
        templateUrl: 'directives/Item.html'
    };
});