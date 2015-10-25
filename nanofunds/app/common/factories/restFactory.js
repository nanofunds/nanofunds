angular.module('nano').factory('restFactory', ['$http',
function ($http) {

    var base = 'http://nanofunds.azurewebsites.net/api/';
    var factory = {};

    factory.get = function (id) {
        return $http.get(base + 'merchant', { params: { id: id } });
    };

    factory.put = function (model) {
        return $http.put(base + '', model);
    };

    factory.post = function (model) {
        return $http.post(base + '', model);
    };

    return factory;
}]);