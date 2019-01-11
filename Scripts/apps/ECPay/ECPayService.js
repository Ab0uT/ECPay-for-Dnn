/// <reference path="../app.js" />

app.factory("ECPayService", function ($resource) {
    var factory = {};
    var baseBath = sf.getServiceRoot("ECPay");

    // 傳送綠界物流
    factory.ECPayPost = function (controller, action) {
        var api = `${baseBath}${controller}/${action}?orderId=:orderId`;
        return $resource(api, { orderId: "@orderId" }, {
            "save": {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=UTF-8" }
            }
        });
    };

    // 取open-store 單筆資料


    factory.ECPayPrint = function (controller, action) {
        var api = `${baseBath}${controller}/${action}?orderNumber=:orderNumber`;
        return $resource(api, { orderNumber: "@orderNumber" },
            {
                "save": {
                    method: "POST",
                    headers: {  "Content-Type": "text/plain; charset=utf-8" }
                }
            });
    };

    return factory;
});