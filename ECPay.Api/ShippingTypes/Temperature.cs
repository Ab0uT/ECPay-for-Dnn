using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    /// <summary>
    /// 溫層
    /// </summary>
    public class Temperature
    {
        public const string
            ROOM = "0001",// 常溫
            REFRIGERATION = "0002",// 冷藏
            FREEZE = "0003";// 冷凍
    }
}
