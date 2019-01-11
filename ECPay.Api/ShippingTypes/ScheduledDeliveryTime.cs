using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    /// <summary>
    /// 預定送達時段
    /// </summary>
    public class ScheduledDeliveryTime
    {
        public const string
            TIME_9_12 = "1",// 9~12時
            TIME_12_17 = "2",// 12~17時
            TIME_17_20 = "3",// 17~20時
            UNLIMITED = "4",// 不限時
            TIME_20_21 = "5",// 20~21時(需限定區域)
            TIME_9_17 = "12",// 早午 9~17
            TIME_9_12_17_20 = "13",// 早晚 9~12 & 17~20
            TIME_13_20 = "23";// 午晚 13~20
    }
}
