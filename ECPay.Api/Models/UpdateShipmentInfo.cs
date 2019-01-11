using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class UpdateShipmentInfo : GenericModel
    {
        /// <summary>
        /// 綠界科技的物流交易編號
        /// </summary>
        [Required]
        [StringLength(20)]
        public string AllPayLogisticsID
        {
            get { return GetString("AllPayLogisticsID"); }
            set { SetString("AllPayLogisticsID", value); }
        }

        /// <summary>
        ///  物流訂單出貨日期
        /// </summary>
        [StringLength(10)]
        public string ShipmentDate
        {
            get { return GetString("ShipmentDate"); }
            set { SetString("ShipmentDate", value); }
        }

        /// <summary>
        ///  物流訂單取貨門市
        /// </summary>
        [StringLength(6)]
        public string ReceiverStoreID
        {
            get { return GetString("ReceiverStoreID"); }
            set { SetString("ReceiverStoreID", value); }
        }
    }
}
