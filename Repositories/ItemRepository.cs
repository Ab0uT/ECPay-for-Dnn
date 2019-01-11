using DotNetNuke.Framework;
using Hugsys.ECPay.Models;
using NBrightDNN;
using Nevoweb.DNN.NBrightBuy.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Hugsys.ECPay.Repositories
{
    public class ItemRepository : ServiceLocator<IItemRepository, ItemRepository>, IItemRepository
    {
        public Items GetByItem()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Items> GetItemList(int orderId)
        {
            List<Items> items = new List<Items>();
            OrderData orderData = new OrderData(orderId);

            var info = orderData.PurchaseInfo.XMLDoc.SelectNodes("genxml/items/*");

            foreach (XmlNode xml in info)
            {
                var newInfo = new NBrightInfo
                {
                    XMLData = xml.OuterXml
                };

                items.Add(new Items
                {
                    ProductName = newInfo.GetXmlProperty("genxml/productname"),
                    Qty = newInfo.GetXmlPropertyInt("genxml/qty"),
                    Model = newInfo.GetXmlProperty("genxml/taxratecode"),
                    ProductPrice = Convert.ToDecimal(newInfo.GetXmlPropertyDouble("genxml/appliedcost")),
                    ProductTotal = Convert.ToDecimal(newInfo.GetXmlPropertyDouble("genxml/appliedtotalcost"))
                });
            }
            return items;
        }

        protected override Func<IItemRepository> GetFactory()
        {
            return () => new ItemRepository();
        }
    }
}