using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models.C2C
{
    public class PrintHiLifeTradeDocument : GenericModel
    {
        /// <summary>
        ///  物流交易編號
        /// </summary>
        [Required]
        [StringLength(20)]
        public string AllPayLogisticsID
        {
            get { return GetString("AllPayLogisticsID"); }
            set { SetString("AllPayLogisticsID", value); }
        }

        /// <summary>
        ///  寄貨編號
        /// </summary>
        [Required]
        [StringLength(15)]
        public string CVSPaymentNo
        {
            get { return GetString("CVSPaymentNo"); }
            set { SetString("CVSPaymentNo", value); }
        }

        /// <summary>
        /// 特約合作平台商代號
        /// </summary>
        [StringLength(10)]
        public string PlatformID
        {
            get { return GetString("PlatformID"); }
            set { SetString("PlatformID", value); }
        }
    }
}
