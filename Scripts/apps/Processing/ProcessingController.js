/// <reference path="../app.js" />

app.controller("ProcessingCtrl", function ($scope, ProcessingService, ECPayService, $uibModal) {

    ProcessingService.tallyManList("Stock", "GetTallyManList")
        .get().$promise
        .then(function (response) {
            $scope.params = response.value;
        });

    $scope.param;

    $scope.open = function (ordernumber) {
        $scope.param = ProcessingService.getOrderNumber("stock", "GetTallyOrder")
            .get({ number: ordernumber }).$promise;

        $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: tempLoc + 'apps/Processing/Form.html',
            controller: 'ECPayCtrl',
            resolve: {
                param: function () {
                    return $scope.param;
                }
            }
        });
    };

    $scope.detail = function (ordernumber) {
        $scope.param = ProcessingService.getOrderNumber("stock", "GetTallyOrder")
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

    var myWindow;
    $scope.getPrint = function (orderNumber) {
        ECPayService.ECPayPrint("Stock", "PrintTradeDocument")
            .save({ orderNumber: orderNumber }).$promise
            .then(function (response) {
                myWindow = window.open("", "NewFile", "width=730,height=345,left=100,top=100,resizable=yes,scrollbars=yes");
                myWindow.document.write(response.html);
            });
    };
});
