using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    /// <summary>
    /// 物流子類型
    /// </summary>
    public class LogisticsSubType
    {
        public const string
            TCAT = "TCAT", // 黑貓(宅配)
            ECAN = "ECAN", // 宅配通
            FAMILY = "FAMI", // 全家
            UNIMART = "UNIMART", // 統一超商
            HILIFE = "HILIFE", // 萊爾富
            FAMILY_C2C = "FAMIC2C", // 全家店到店
            UNIMART_C2C = "UNIMARTC2C", // 統一超商寄貨便
            HILIFE_C2C = "HILIFEC2C"; // 萊爾富富店到店
    }
}
