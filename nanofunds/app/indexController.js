angular.module('nano').controller('indexController', ['$scope', '$location', '$window', 'authenticationFactory',
function ($scope, $location, $window, authenticationFactory) {
    authenticationFactory.setMerchantId($window.mId);
    console.log('index controller');
}]);