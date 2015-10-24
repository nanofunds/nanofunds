angular.module('moment', [])
.factory('moment', ['$window', function ($window) {
    return $window.moment;
}]);

var app = angular.module('nano', [
    'ngRoute',
    'ngSanitize',
    'ngStorage',
    'ng-currency'
]);