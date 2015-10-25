angular.module('nano').config(['$routeProvider', '$httpProvider',
function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('httpInterceptorFactory');

    var base = '/app/';

    $routeProvider
    .when('/dashboard', {
        templateUrl: base + 'dashboard/dashboard.html',
        controller: 'dashboardController',
        resolve: {
            'merchant': ['authenticationFactory', 'restFactory',
                    function (authenticationFactory, restFactory) {
                        console.log(authenticationFactory.getMerchantId());
                        //console.log($scope.mId);
                        /*return restFactory.get()
                        .then(function(res) {

                        })
                        .catch(function() {
                            return null;
                        });*/
                    }
                ]
            }
        })
    .otherwise('/dashboard');

}]);