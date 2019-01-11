using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    public class ScheduledPickupTime
    {
        /// <summary>
        /// 預計取件時段
        /// </summary>
        public const string
             TIME_9_12 = "1",// 9~12時
             TIME_12_17 = "2",// 12~17時
             TIME_17_20 = "3",// 17~20時
             UNLIMITED = "4";// 不限時
    }
}
