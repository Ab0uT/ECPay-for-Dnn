/// <reference path="../app.js" />

app.factory("ProcessingService", function ($resource) {
    var factory = {};
    var baseBath = sf.getServiceRoot("ECPay");

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

    factory.tallyManList = function (controller, action) {
        var api = `${baseBath}${controller}/${action}`;
        return $resource(api, null, {
            "get": {
                method: "GET",
                headers: { "Content-Type": "application/json; charset=UTF-8" }
            }
        });
    };
    return factory;
});