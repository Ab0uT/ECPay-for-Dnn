using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    /// <summary>
    /// 是否代收貨款
    /// </summary>
    public class IsCollection
    {
        public const string
            YES = "Y", // 貨到付款
            NO = "N"; // 僅配送
    }
}
