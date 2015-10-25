﻿angular.module('nano').config(['$routeProvider', '$httpProvider',
function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('httpInterceptorFactory');

    var base = '/app/';

    $routeProvider
    .when('/', {
        templateUrl: base + 'home/home.html'
    })
    .when('/dashboard', {
        templateUrl: base + 'dashboard/dashboard.html',
        controller: 'dashboardController'
     })
    .otherwise('/');

}]);