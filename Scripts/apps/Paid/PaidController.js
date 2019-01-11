/// <reference path="../app.js" />

app.controller("paidCtrl", function ($scope, paidService, $state, $uibModal) {

    paidService.orderList("Stock", "GetOrderStateList")
        .get({ state: '040' }).$promise
        .then(function (response) {
            $scope.params = response.value;
        });

    $scope.doTally = function (orderid) {
        $scope.param = paidService.doTally("stock", "TallyRecord")
            .save({ orderId: orderid }).$promise
            .then(function (response) {
                $state.reload('orders.paidList');
            });
    };

    $scope.detail = function (ordernumber) {
        $scope.param = paidService.getOrderNumber("stock", "getorder")
            .get({ number: ordernumber }).$promise;

        $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: tempLoc + 'apps/Detail/Detail.html',
            controller: 'detailCtrl',
            size: 'lg',
            resolve: {
                param: function () {
                    return $scope.param;
                }
            }
        });
    };
});