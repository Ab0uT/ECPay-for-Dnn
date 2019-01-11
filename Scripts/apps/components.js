/// <reference path="app.js" />

app.component("orders", {
    template: '<ul class="nav nav-pills nav-fill">' +
        '<li class="nav-item">' +
        '<a class="nav-link" ui-sref=".paidList" ui-sref-active="active">已付款訂單</a>' +
        '</li>' +
        '<li class="nav-item">' +
        '<a class="nav-link" ui-sref=".processingList" ui-sref-active="active">理貨中訂單</a>' +
        '</li>' +
        '</ul>' +
        '<div class="container">' +
        '<div ui-view></div>' +
        '</div>'
});





