/// <reference path="../app.js" />

app.component("processingList", {
    bindings: {
        params: '<'
    },
    template:
        '   <table class="table">' +
        '       <thead class="thead-light">' +
        '           <tr>' +
        '               <th scope="col">訂單編號</th>' +
        '               <th scope="col">資訊</th>' +
        '               <th scope="col"></th>' +
        '               <th scope="col"></th>' +
        '               <th scope="col"></th>' +
        '           </tr>' +
        '       </thead>' +
        '       <tbody>' +
        '        <tr ng-repeat="m in params">' +
        '            <td>{{ m.Order.OrderNumber }}</td>' +
        '            <td><a href="#" ng-click="detail(m.Order.OrderNumber)">{{ m.Order.LastName }} {{ m.Order.FirstName }}<br/>{{m.Order.PostalCode}} {{ m.Order.Unit }}</a></td>' +
        '            <td><button type="button" class="btn btn-success"  ng-click="open(m.Order.OrderNumber)">物流訂單建立</button></td>' +
        '            <td><a  type="button" class="btn btn-info" ng-if="m.Order.Print" ng-click="getPrint(m.Order.OrderNumber)"  href="javascript:void(0)">列印</a></td>' +
        '            <td><button class="btn btn-danger">移除</button></td>' +
        '       </tr>' +
        '       </tbody>' +
        '   </table>',
    controller: "ProcessingCtrl"
});