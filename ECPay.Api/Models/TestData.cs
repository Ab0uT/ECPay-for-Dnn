using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class TestData : GenericModel
    {
        /// <summary>
        ///  Client端回覆網址
        /// </summary>
        [StringLength(200)]
        public string ClientReplyURL
        {
            get { return GetString("ClientReplyURL"); }
            set { SetString("ClientReplyURL", value); }
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

        /// <summary>
        /// 物流子類型
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LogisticsSubType
        {
            get { return GetString("LogisticsSubType"); }
            set { SetString("LogisticsSubType", value); }
        }
    }
}
