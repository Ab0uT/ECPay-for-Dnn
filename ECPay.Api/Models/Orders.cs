using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class Orders : GenericModel
    {
        /// <summary>
        /// 廠商交易編號
        /// </summary>
        [StringLength(20)]
        public string MerchantTradeNo
        {
            get { return GetString("MerchantTradeNo"); }
            set { SetString("MerchantTradeNo", value); }
        }

        /// <summary>
        /// 廠商交易時間
        /// </summary>
        [Required]
        [StringLength(20)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public string MerchantTradeDate
        {
            get { return GetString("MerchantTradeDate"); }
            set { SetString("MerchantTradeDate", value); }
        }

        /// <summary>
        /// 物流類型
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LogisticsType
        {
            get { return GetString("LogisticsType"); }
            set { SetString("LogisticsType", value); }
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
        /// 是否代收金額
        /// </summary>
        [StringLength(1)]
        public string IsCollection
        {
            get { return GetString("IsCollection"); }
            set { SetString("IsCollection", value); }
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
        /// 寄件人電話
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^\(?\d{2}\)?\-?\d{2,6}\-?\d{2,6}(#\d{1,6}){0,1}$")]
        public string SenderPhone
        {
            get { return GetString("SenderPhone"); }
            set { SetString("SenderPhone", value); }
        }

        /// <summary>
        /// 寄件人手機
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^09\d{8}$")]
        public string SenderCellPhone
        {
            get { return GetString("SenderCellPhone"); }
            set { SetString("SenderCellPhone", value); }
        }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        [Required]
        [StringLength(10)]
        [RegularExpression(@"[^\W_]+")]
        public string ReceiverName
        {
            get { return GetString("ReceiverName"); }
            set { SetString("ReceiverName", value); }
        }

        /// <summary>
        /// 收件人電話
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^\(?\d{2}\)?\-?\d{2,6}\-?\d{2,6}(#\d{1,6}){0,1}$")]
        public string ReceiverPhone
        {
            get { return GetString("ReceiverPhone"); }
            set { SetString("ReceiverPhone", value); }
        }

        /// <summary>
        /// 收件人手機
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^09\d{8}$")]
        public string ReceiverCellPhone
        {
            get { return GetString("ReceiverCellPhone"); }
            set { SetString("ReceiverCellPhone", value); }
        }

        /// <summary>
        /// 收件人email
        /// </summary>        
        [StringLength(50)]
        [EmailAddress]
        public string ReceiverEmail
        {
            get { return GetString("ReceiverEmail"); }
            set { SetString("ReceiverEmail", value); }
        }

        /// <summary>
        /// 交易描述
        /// </summary>
        [StringLength(200)]
        public string TradeDesc
        {
            get { return GetString("TradeDesc"); }
            set { SetString("TradeDesc", value); }
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
        /// Client端回覆網址
        /// </summary>
        [StringLength(200)]
        [Url]
        public string ClientReplyURL
        {
            get { return GetString("ClientReplyURL"); }
            set { SetString("ClientReplyURL", value); }
        }

        /// <summary>
        /// Server端物流回傳網址
        /// </summary>
        [StringLength(200)]
        [Url]
        public string LogisticsC2CReplyURL
        {
            get { return GetString("LogisticsC2CReplyURL"); }
            set { SetString("LogisticsC2CReplyURL", value); }
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
