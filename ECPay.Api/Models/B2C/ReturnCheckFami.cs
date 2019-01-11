using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class ReturnCheckFami : GenericModel
    {
        /// <summary>
        /// 物流退貨訂單編號
        /// </summary>
        [Required]
        [StringLength(13)]
        public string RtnMerchantTradeNo
        {
            get { return GetString("RtnMerchantTradeNo"); }
            set { SetString("RtnMerchantTradeNo", value); }
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
