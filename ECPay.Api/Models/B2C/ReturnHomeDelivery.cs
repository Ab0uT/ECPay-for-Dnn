using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class ReturnHomeDelivery : GenericModel
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
        /// 退貨人姓名
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
        /// 退貨人手機
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^09\d{8}$")]
        public string SenderCellPhone
        {
            get { return GetString("SenderCellPhone"); }
            set { SetString("SenderCellPhone", value); }
        }

        ///<summary>
        /// 退貨人郵遞區號
        ///</summary>
        [StringLength(5)]
        public string SenderZipCode
        {
            get { return GetString("SenderZipCode"); }
            set { SetString("SenderZipCode", value); }
        }

        ///<summary>
        /// 退貨人地址
        ///</summary>
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string SenderAddress
        {
            get { return GetString("SenderAddress"); }
            set { SetString("SenderAddress", value); }
        }

        /// <summary>
        /// 收件人姓名
        /// </summary>
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

        ///<summary>
        /// 收件人郵遞區號
        ///</summary>
        [StringLength(5)]
        public string ReceiverZipCode
        {
            get { return GetString("ReceiverZipCode"); }
            set { SetString("ReceiverZipCode", value); }
        }

        /// <summary>
        /// 收件人地址
        /// </summary>
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string ReceiverAddress
        {
            get { return GetString("ReceiverAddress"); }
            set { SetString("ReceiverAddress", value); }
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
