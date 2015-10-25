angular.module('nano').config(['$routeProvider', '$httpProvider',
function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('httpInterceptorFactory');

    var base = '/app/';

    $routeProvider
    .when('/dashboard', {
        templateUrl: base + 'dashboard/dashboard.html',
        controller: 'dashboardController',
        resolve: {
            'projection': ['authenticationFactory', 'restFactory',
                    function (authenticationFactory, restFactory) {
                        var mId = authenticationFactory.getMerchantId();
                        return restFactory.get(mId)
                        .then(function(res) {
                               return res.data;
                            })
                        .catch(function() {
                            return null;
                        });
                    }
                ]
            }
        })
    .otherwise('/dashboard');

}]);