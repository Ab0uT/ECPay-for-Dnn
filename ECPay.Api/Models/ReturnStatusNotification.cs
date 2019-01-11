using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class ReturnStatusNotification
    {
        public string MerchantID { get; set; }

        public string RtnMerchantTradeNo { get; set; }

        public int RtnCode { get; set; }

        public string RtnMsg { get; set; }

        public string AllPayLogisticsID { get; set; }

        public int GoodsAmount { get; set; }

        public string UpdateStatusDate { get; set; }

        public string BookingNote { get; set; }

        public string CheckMacValue { get; set; }
    }
}
