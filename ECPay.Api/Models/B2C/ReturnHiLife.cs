using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class ReturnHiLife : GenericModel
    {
        /// <summary>
        /// 綠界科技的物流交易編號
        /// </summary>
        [StringLength(20)]
        public string AllPayLogisticsID
        {
            get { return GetString("AllPayLogisticsID"); }
            set { SetString("AllPayLogisticsID", value); }
        }

        /// <summary>
        /// Server端回覆網址
        /// </summary>
        [Required]
        [StringLength(200)]
        [Url]
        public string ServerReplyURL
        {
            get { return GetString("ServerReplyURL"); }
            set { SetString("ServerReplyURL", value); }
        }

        /// <summary>
        /// 商品名稱
        /// </summary>
        [StringLength(50)]
        [RegularExpression(@"[^\W_]+")]
        public string GoodsName
        {
            get { return GetString("GoodsName"); }
            set { SetString("GoodsName", value); }
        }

        /// <summary>
        /// 商品金額
        /// </summary>
        [Required]
        [Range(1, 20000)]
        [RegularExpression(@"^\d+$")]
        public int GoodsAmount
        {
            get { return GetInt("GoodsAmount"); }
            set { SetInt("GoodsAmount", value); }
        }

        /// <summary>
        /// 代收金額
        /// </summary>
        [RegularExpression(@"^\d+$")]
        public int CollectionAmount
        {
            get { return GetInt("CollectionAmount"); }
            set { SetInt("CollectionAmount", value); }
        }

        /// <summary>
        /// 服務型態代碼
        /// </summary>
        [Required]
        [StringLength(5)]
        public string ServiceType
        {
            get { return GetString("ServiceType"); }
            set { SetString("ServiceType", value); }
        }

        /// <summary>
        /// 寄件人姓名
        /// </summary>
        [Required]
        [StringLength(10)]
        [RegularExpression(@"[^\W_]+")]
        public string SenderName
        {
            get { return GetString("SenderName"); }
            set { SetString("SenderName", value); }
        }

        /// <summary>
        /// 退貨人電話
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^\(?\d{2}\)?\-?\d{2,6}\-?\d{2,6}(#\d{1,6}){0,1}$")]
        public string SenderPhone
        {
            get { return GetString("SenderPhone"); }
            set { SetString("SenderPhone", value); }
        }

        /// <summary>
        /// 備註
        /// </summary>
        [StringLength(200)]
        public string Remark
        {
            get { return GetString("Remark"); }
            set { SetString("Remark", value); }
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
