angular.module('nano').controller('indexController', ['$scope', '$location', '$window', '$route', 'authenticationFactory',
function ($scope, $location, $window, $route, authenticationFactory) {
    authenticationFactory.setMerchantId($window.mId);
    
    $scope.$on("$routeChangeSuccess", function() {
        $scope.activeTab = $route.current.activetab;
        console.log($route.current.activetab);
    });
}]);