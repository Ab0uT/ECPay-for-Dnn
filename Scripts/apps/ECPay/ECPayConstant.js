/// <reference path="../app.js" />

// 預設綠界該需要的值
app.constant("ECPay", {
    LogisticsType: [
        { name: "超商取貨", value: "CVS" },
        { name: "宅配", value: "Home" }
    ],
    Temperature: [
        { name: "常溫", value: "0001" },
        { name: "冷藏", value: "0002" },
        { name: "冷凍", value: "0003" }
    ],
    Distance: [
        { name: "同縣市", value: "00" }, 
        { name: "外縣市", value: "01" }, 
        { name: "離島", value: "02" }
    ],
    Specification: [
        { name: "60cm以下", value: "0001" },
        { name: "61cm~90cm", value: "0002" },
        { name: "91cm~120cm", value: "0003" },
        { name: "121cm~150cm", value: "0004" }
    ],
    ScheduledPickupTime: [
        { name: "9~12時", value: "1" },
        { name: "12~17時", value: "2" },
        { name: "17~20時", value: "3" },
        { name: "不限時", value: "4" }
    ],
    ScheduledDeliveryTime: [
        { name: "9~12時", value: "1" },// 9~12時
        { name: "12~17時", value: "2" },// 12~17時
        { name: "17~20時", value: "3" },// 17~20時
        { name: "不限時", value: "4" },// 不限時
        { name: "20~21時(需限定區域)", value: "5" },// 20~21時(需限定區域)
        { name: "早午 9~17", value: "12" },// 早午 9~17
        { name: "早晚 9~12 & 17~20", value: "13" },// 早晚 9~12 & 17~20
        { name: "午晚 13~20", value: "23" }// 午晚 13~20
    ]
});


