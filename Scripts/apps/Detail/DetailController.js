/// <reference path="../app.js" />

app.controller("detailCtrl", function (param, $scope) {
    $scope.value = param.value;
    $scope.items = param.items;
});

