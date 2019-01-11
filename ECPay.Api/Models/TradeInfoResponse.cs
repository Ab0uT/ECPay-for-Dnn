using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class TradeInfoResponse
    {
        public string MerchantID { get; set; }

        public string MerchantTradeNo { get; set; }

        public int GoodsAmount { get; set; }

        public string LogisticsType { get; set; }

        public int HandlingCharge { get; set; }

        public string TradeDate { get; set; }

        public string LogisticsStatus { get; set; }

        public string GoodName { get; set; }

    }
}
