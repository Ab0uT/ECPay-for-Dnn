using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class UpdateUniMartNotification : GenericModel
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
        /// 物品名稱
        /// </summary>
        [StringLength(60)]
        public string GoodsName
        {
            get { return GetString("GoodsName"); }
            set { SetString("GoodsName", value); }
        }

        /// <summary>
        /// 商品金額
        /// </summary>
        public string GoodsAmount
        {
            get { return GetString("GoodsAmount"); }
            set { SetString("GoodsAmount", value); }
        }

        /// <summary>
        /// 門市類型
        /// </summary>
        [StringLength(2)]
        public string StoreType
        {
            get { return GetString("StoreType"); }
            set { SetString("StoreType", value); }
        }

        /// <summary>
        /// 狀態代碼
        /// </summary>
        [StringLength(2)]
        public string Status
        {
            get { return GetString("Status"); }
            set { SetString("Status", value); }
        }

        /// <summary>
        /// 門市店號
        /// </summary>
        [StringLength(20)]
        public string StoreID
        {
            get { return GetString("StoreID"); }
            set { SetString("StoreID", value); }
        }
    }
}
