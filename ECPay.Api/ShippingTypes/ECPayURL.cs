using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    /// <summary>
    /// 正式環境網址
    /// </summary>
    public class ECPayURL
    {
        public const string
            // 電子地圖
            CVS_MAP = "https://logistics.ecpay.com.tw/Express/map",
            // 物流訂單建立
            SHIPPING_ORDER = "https://logistics.ecpay.com.tw/Express/Create",
            // 宅配逆物流訂單
            HOME_RETURN_ORDER = "https://logistics.ecpay.com.tw/Express/ReturnHome",
            // 超商取貨逆物流訂單(統一超商B2C)
            UNIMART_RETURN_ORDER = "https://logistics.ecpay.com.tw/express/ReturnUniMartCVS",
            // 超商取貨逆物流訂單(萊爾富超商B2C)
            HILIFE_RETURN_ORDER = "https://logistics.ecpay.com.tw/express/ReturnHiLifeCVS",
            // 超商取貨逆物流訂單(全家超商B2C)
            FAMILY_RETURN_ORDER = "https://logistics.ecpay.com.tw/express/ReturnCVS",
            // 全家逆物流核帳(全家超商B2C)
            FAMILY_RETURN_CHECK = "https://logistics.ecpay.com.tw/Helper/LogisticsCheckAccoounts",
            // 統一修改物流資訊(全家超商B2C)
            UNIMART_UPDATE_LOGISTICS_INFO = "https://logistics.ecpay.com.tw/Helper/UpdateShipmentInfo",
            // 更新門市(統一超商C2C)
            UNIMART_UPDATE_STORE_INFO = "https://logistics.ecpay.com.tw/Express/UpdateStoreInfo",
            // 取消訂單(統一超商C2C)
            UNIMART_CANCEL_LOGISTICS_ORDER = "https://logistics.ecpay.com.tw/Express/CancelC2COrder",
            // 物流訂單查詢
            QUERY_LOGISTICS_INFO = "https://logistics.ecpay.com.tw/Helper/QueryLogisticsTradeInfo/V2",
            // 產生托運單(宅配)/一段標(超商取貨)
            PRINT_TRADE_DOC = "https://logistics.ecpay.com.tw/helper/printTradeDocument",
            // 列印繳款單(統一超商C2C)
            PRINT_UNIMART_C2C_BILL = "https://logistics.ecpay.com.tw/Express/PrintUniMartC2COrderInfo",
            // 全家列印小白單(全家超商C2C)
            PRINT_FAMILY_C2C_BILL = "https://logistics.ecpay.com.tw/Express/PrintFAMIC2COrderInfo",
            // 萊爾富列印小白單(萊爾富超商C2C)
            Print_HILIFE_C2C_BILL = "https://logistics.ecpay.com.tw/Express/PrintHILIFEC2COrderInfo",
             // 產生 B2C 測標資料
             CREATE_TEST_DATA = "https://logistics.ecpay.com.tw/Express/CreateTestData";
    }
}
