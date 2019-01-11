using DotNetNuke.Entities.Portals;
using DotNetNuke.Framework;
using Hugsys.ECPay.Models;
using NBrightDNN;
using Nevoweb.DNN.NBrightBuy.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Repositories
{
    public class OrderRepository : ServiceLocator<IOrderRepository, OrderRepository>, IOrderRepository
    {
        public void AddFreight(int orderId, string type)
        {
            var orderData = new OrderData(orderId);

            orderData.PurchaseInfo.SetXmlPropertyDouble("genxml/puglinfreight", 9);
            if (type != string.Empty)
                orderData.PurchaseInfo.SetXmlProperty("genxml/dropdownlist/orderstatus", type);
        }

        /// <summary>
        /// 取得訂單編號
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public OrderInfo GetByOrderNumber(string ordernumber, string type)
        {
            return GetOrderStateList(type).SingleOrDefault(m => m.OrderNumber == ordernumber);
        }

        /// <summary>
        /// 取得訂單清單
        /// </summary>
        /// <param name="type">訂單狀態</param>
        /// <returns></returns>
        public IEnumerable<OrderInfo> GetOrderStateList(string type)
        {
            NBrightBuyController nBrightBuy = new NBrightBuyController();
            List<OrderInfo> orders = new List<OrderInfo>();

            string search = $" and ([xmldata].value('(genxml/dropdownlist/orderstatus)[1]', 'nvarchar(max)') = '{type}')   ";
            string orderby = "   order by [XMLData].value('(genxml/createddate)[1]','datetime') DESC, ModifiedDate DESC  ";

            List<NBrightInfo> orderInfo = nBrightBuy.GetList(PortalSettings.Current.PortalId, -1, "ORDER", search, orderby);

            foreach (NBrightInfo info in orderInfo)
            {
                orders.Add(new OrderInfo
                {
                    OrderId = info.ItemID,
                    OrderNumber = info.GetXmlProperty("genxml/ordernumber"),
                    FirstName = info.GetXmlProperty("genxml/billaddress/genxml/textbox/firstname"),
                    LastName = info.GetXmlProperty("genxml/billaddress/genxml/textbox/lastname"),
                    Telephone = info.GetXmlProperty("genxml/billaddress/genxml/textbox/telephone"),
                    Email = info.GetXmlProperty("genxml/billaddress/genxml/textbox/email"),
                    Company = info.GetXmlProperty(""),
                    Unit = info.GetXmlProperty("genxml/billaddress/genxml/textbox/unit"),
                    Street = info.GetXmlProperty("genxml/billaddress/genxml/textbox/street"),
                    City = info.GetXmlProperty("genxml/billaddress/genxml/textbox/city"),
                    PostalCode = info.GetXmlProperty("genxml/billaddress/genxml/textbox/postalcode"),
                    Freight = Convert.ToDecimal(info.GetXmlPropertyDouble("genxml/appliedshipping")),
                    Total = info.GetXmlPropertyInt("genxml/appliedtotal"),
                    OrderStatus = info.GetXmlProperty("genxml/dropdownlist/orderstatus"),
                    Shipping = info.GetXmlProperty("genxml/extrainfo/genxml/hidden/shippingdisplayanme"),
                    LogisticsType = info.GetXmlProperty("genxml/extrainfo/genxml/hidden/logisticstype"),
                    LogisticsSubType = info.GetXmlProperty("genxml/extrainfo/genxml/hidden/logisticssubtype"),
                    StoreId = info.GetXmlProperty("genxml/extrainfo/genxml/hidden/cvsstoreid"),
                    StoreName = info.GetXmlProperty("genxml/extrainfo/genxml/textbox/cvsstorename"),
                    AllPayLogisticsID = info.GetXmlProperty("genxml/textbox/trackingcode"),
                    Print = info.GetXmlPropertyBool("genxml/tradedocument")
                });
            }

            return orders;
        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <param name="orderId">取得訂單ID</param>
        /// <param name="logisticsId">取得物流ID</param>
        /// <param name="type">改變訂單狀態</param>
        public void UpdateOrder(int orderId, string logisticsId, string type, int freight = 0)
        {
            var orderData = new OrderData(orderId);
            string xmlLogisticsId = orderData.PurchaseInfo.GetXmlProperty("genxml/textbox/trackingcode");

            if ((xmlLogisticsId != logisticsId) || (xmlLogisticsId == null))
                orderData.PurchaseInfo.SetXmlProperty("genxml/textbox/trackingcode", logisticsId);

            orderData.PurchaseInfo.SetXmlProperty("genxml/tradedocument", $"{true}");

            if (type != string.Empty)
                orderData.PurchaseInfo.SetXmlProperty("genxml/dropdownlist/orderstatus", type);

            if (freight != 0)
                orderData.PurchaseInfo.SetXmlPropertyDouble("genxml/pluginfreight", freight);


            orderData.Save();
        }

        protected override Func<IOrderRepository> GetFactory()
        {
            return () => new OrderRepository();
        }
    }
}