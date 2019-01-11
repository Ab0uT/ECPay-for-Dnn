/// <reference path="../app.js" />

// 取得 open-store 訂單傳輸流程
app.factory("paidService", function ($resource) {
    var factory = {};
    var baseBath = sf.getServiceRoot("ECPay");

    factory.orderList = function (controller, action) {
        var api = `${baseBath}${controller}/${action}?state=:state`;
        return $resource(api, { state: "@state" }, {
            "get": {
                method: "GET",
                headers: { "Content-Type": "application/json; charset=UTF-8" }
            },
            "query": { method: "GET", isArray: true },
            headers: { "Content-Type": "application/json; charset=UTF-8" }
        });
    };

    factory.getOrderNumber = function (controller, action) {
        var api = `${baseBath}${controller}/${action}?ordernumber=:number`;
        return $resource(api, { number: "@number" },
            {
                "get": {
                    method: "GET",
                    headers: { "Content-Type": "application/json; charset=UTF-8" }
                },
                "query": { method: "GET", isArray: true },
                headers: { "Content-Type": "application/json; charset=UTF-8" }
            });
    };

    factory.doTally = function (controller, action) {
        var api = `${baseBath}${controller}/${action}?orderId=:orderId`;
        return $resource(api, { orderId: "@orderId" }, {
            "save": {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=UTF-8" }
            }
        });
    };
    return factory;
});