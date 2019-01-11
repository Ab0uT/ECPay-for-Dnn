using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class HomeDelivery : Orders
    {
        /// <summary>
        /// 寄件人郵遞區號
        /// </summary>
        [Required]
        [StringLength(5)]
        public string SenderZipCode
        {
            get { return GetString("SenderZipCode"); }
            set { SetString("SenderZipCode", value); }
        }

        /// <summary>
        /// 寄件人地址
        /// </summary>
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string SenderAddress
        {
            get { return GetString("SenderAddress"); }
            set { SetString("SenderAddress", value); }
        }

        /// <summary>
        ///  收件人郵遞區號
        /// </summary>
        [Required]
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
        /// 溫層
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Temperature
        {
            get { return GetString("Temperature"); }
            set { SetString("Temperature", value); }
        }

        /// <summary>
        /// 距離
        /// </summary>
        [Required]
        [StringLength(2)]
        public string Distance
        {
            get { return GetString("Distance"); }
            set { SetString("Distance", value); }
        }

        /// <summary>
        /// 規格
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Specification
        {
            get { return GetString("Specification"); }
            set { SetString("Specification", value); }
        }

        /// <summary>
        /// 預定取件時段
        /// </summary>
        [StringLength(1)]
        public string ScheduledPickupTime
        {
            get { return GetString("ScheduledPickupTime"); }
            set { SetString("ScheduledPickupTime", value); }
        }

        /// <summary>
        /// 預定送達時段
        /// </summary>
        [StringLength(2)]
        public string ScheduledDeliveryTime
        {
            get { return GetString("ScheduledDeliveryTime"); }
            set { SetString("ScheduledDeliveryTime", value); }
        }

        /// <summary>
        /// 指定送達日
        /// </summary>
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string ScheduledDeliveryDate
        {
            get { return GetString("ScheduledDeliveryDate"); }
            set { SetString("ScheduledDeliveryDate", value); }
        }

        /// <summary>
        /// 包裹數
        /// </summary>
        public int PackageCount
        {
            get { return GetInt("PackageCount"); }
            set { SetInt("PackageCount", value); }
        }


    }
}
