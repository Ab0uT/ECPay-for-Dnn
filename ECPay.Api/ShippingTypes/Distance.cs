using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingTypes
{
    /// <summary>
    /// 距離
    /// </summary>
    public class Distance
    {
        public const string
            SAME = "00", //同縣市
            OTHER = "01", //外縣市
            ISLAND = "02"; //離島
    }
}
