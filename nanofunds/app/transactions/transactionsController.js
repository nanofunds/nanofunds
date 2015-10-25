angular.module('nano').controller('transactionsController', ['$scope', '$location', 'transactions', 'restFactory',
function ($scope, $location, transactions, restFactory) {
    //$location.search({});
    console.log(transactions);
    $scope.transactions = transactions;
    console.log('transactions controlelr');

}]);