using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class StorePickup : Orders
    {
        /// <summary>
        /// 收件人門市代號
        /// </summary>
        [Required]
        [ShippingMethods.ValidateMixTypeID(6)]
        public string ReceiverStoreID
        {
            get { return GetString("ReceiverStoreID"); }
            set { SetString("ReceiverStoreID", value); }
        }

        /// <summary>
        /// 退貨門市代號
        /// </summary>
        [Required]
        [ShippingMethods.ValidateMixTypeID(6)]
        public string ReturnStoreID
        {
            get { return GetString("ReturnStoreID"); }
            set { SetString("ReturnStoreID", value); }
        }
    }
}
