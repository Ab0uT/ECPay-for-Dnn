/// <reference path="../app.js" />

app.controller("ECPayCtrl", function ($scope, ECPayService, ECPay, param, $interval, $state) {
    var myWindow;
    let val = param.value;
    $scope.vm = param.value;
    $scope.selects = ECPay;

    $scope.ecpay = {
        GoodsAmount: val.Total,
        LogisticsType: val.LogisticsType,
        LogisticsSubType: val.LogisticsSubType,
        ReceiverName: `${val.LastName} ${val.FirstName}`,
        ReceiverCellPhone: val.Telephone,
        ReceiverEmail: val.Email,
        IsCollection: val.isCollection ? "Y" : "N"
    };

    if (val.LogisticsType === "CVS") {
        $scope.ecpay.ReceiverStoreID = val.StoreId;
        $scope.ecpay.ReturnStoreID = val.StoreId;
    }

    if (val.LogisticsType === "Home") {
        $scope.ecpay.ReceiverZipCode = val.PostalCode;
        $scope.ecpay.ReceiverAddress = val.Unit;
    }
       
    $scope.ecpaySubmit = function (form) {
        if (form.$valid) {
            if ($scope.ecpay.LogisticsType === "Home") {
                ECPayService.ECPayPost("ECPay", "HomeDelivery")
                    .save({ orderId: val.OrderId }, $scope.ecpay).$promise
                    .then(function (response) {
                        if (response.html !== undefined) {
                            myWindow = window.open("", "NewFile", "width=730,height=345,left=100,top=100,resizable=yes,scrollbars=yes");
                            myWindow.document.write(response.html);
                            checkWin();
                        } else {
                            alert(response.message);
                        }
                    });
            } else {
                ECPayService.ECPayPost("ECPay", "StorePickup")
                    .save({ orderId: val.OrderId }, $scope.ecpay).$promise
                    .then(function (response) {
                        if (response.html !== undefined) {
                            myWindow = window.open("", "NewFile", "width=730,height=345,left=100,top=100,resizable=yes,scrollbars=yes");
                            myWindow.document.write(response.html);
                            checkWin();
                        } else {
                            alert(response.message);
                        }
                    });
            }
        }
    };

    function checkWin() {
        var refreshTime = $interval(function () {
            var closed = false;
            if (myWindow) {
                if (myWindow.closed) {
                    closed = true;
                    $state.reload('orders.processingList');
                    $interval.cancel(refreshTime);
                }
            }
        }, 2000);
    }
});