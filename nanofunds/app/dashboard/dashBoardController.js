angular.module('nano').controller('dashboardController', ['$scope', '$location', 'projection', 'restFactory',
function ($scope, $location, projection, restFactory) {
    //$location.search({});
    console.log(projection);
    $scope.merchant = {
        name: 'A Test Merchant'
    };

    $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
    $scope.series = ['Series A', 'Series B'];
    $scope.data = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];
    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };

    console.log('dashboard controlelr');

}]);