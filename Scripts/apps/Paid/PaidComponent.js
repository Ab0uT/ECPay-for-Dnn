/// <reference path="../app.js" />

// 已付款訂單清單畫面
app.component("paidList", {
    template:
           `<table class="table"> 
               <thead class="thead-light"> 
                   <tr> 
                       <th scope="col">訂單編號</th> 
                       <th scope="col">資訊</th>  
                       <th scope="col"></th> 
                       <th scope="col">理貨人員</th> 
                       <th scope="col">開始日期</th>  
                        <th scope="col" > 完成日期</th>  
                   </tr> 
               </thead> 
               <tbody> 
                <tr ng-repeat="m in params"> 
                    <td>{{ m.Order.OrderNumber }}</td> 
                    <td><a href="#" ng-click="detail(m.Order.OrderNumber)">{{ m.Order.LastName }} {{ m.Order.FirstName }}<br/>{{m.Order.PostalCode}} {{ m.Order.Unit }}</a></td> 
                    <td><a  ng-class="m.Tally.IsTally == true?'btn btn-danger':'btn btn-success'" ng-click="doTally(m.Order.OrderId)" href="javascript: void(0)">理貨</a></td> 
                    <td>{{m.Tally.TallyMan}}</td> 
                    <td>{{m.Tally.TallyStartDate}}</td> 
               </tr> 
               </tbody>`,
    bindings: {
        params: '<',
        detail: '&'
    },
    controller: "paidCtrl"
});