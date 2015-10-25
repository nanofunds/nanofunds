angular.module('nano').controller('dashboardController', ['$scope', '$location', 'projection', 'restFactory',
function ($scope, $location, projection, restFactory) {
    console.log(projection);
    $scope.merchant = {
        name: projection.Merchant
    };

    $scope.labels = projection.Days;
    $scope.series = ['Actual', 'Estimated'];
    $scope.data = projection.Graphs;
    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };
}]);