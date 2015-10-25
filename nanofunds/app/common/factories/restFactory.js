angular.module('nano').factory('restFactory', ['$http',
function ($http) {

    var base = 'http://localhost:2275/api';
    var factory = {};

    factory.get = function () {
        return $http.get(base + '');
    };

    factory.get = function () {
        return $http.get(base + '', { params: { id: id } });
    };

    factory.put = function (model) {
        return $http.put(base + '', model);
    };

    factory.post = function (model) {
        return $http.post(base + '', model);
    };

    return factory;
}]);