$("body").attr({
    "ng-app": "ECPay"
});

'use strict';


var app = angular.module("ECPay", ["ui.router", "ngResource","ui.bootstrap"]);

app.config(function ($urlRouterProvider, $stateProvider, $httpProvider, $qProvider, $compileProvider) {
    $qProvider.errorOnUnhandledRejections(false);
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|javascript):/);
    $urlRouterProvider.otherwise('', '/');
    var url = "DesktopModules/ECPay/Views/";

    $stateProvider
        .state('orders', {
            url: '/',
            component: "orders"
        })
        .state('orders.paidList', {
            component: "paidList"

        })
        .state('orders.processingList', {
            component: "processingList"
        });


    if ($.ServicesFramework) {
        var httpHeaders = {
            "ModuleId": sf.getModuleId(),
            "TabId": sf.getTabId(),
            "RequestVerificationToken": sf.getAntiForgeryValue()
        };

        angular.extend($httpProvider.defaults.headers.common, httpHeaders);
    }
});

var tempLoc = $('script[src*="ECPay/Scripts"]').attr('src');

tempLoc = tempLoc.replace('angular.min.js', '');   // the js folder path
if (tempLoc.indexOf('?') > -1) {
    tempLoc = tempLoc.substr(0, tempLoc.indexOf('?'));
}