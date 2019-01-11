using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
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
    public class TallyRepository : ServiceLocator<ITallyRepository, TallyRepository>, ITallyRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public Tally GetByTallyMan(int orderId)
        {
            return GetTallyManList(orderId)
                .OrderByDescending(m => Convert.ToDateTime(m.TallyStartDate))
                .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tally> GetTallyManList(int orderId)
        {
            NBrightBuyController nBrightBuy = new NBrightBuyController();
            List<Tally> tallies = new List<Tally>();
            OrderData orderData = new OrderData(orderId);

            var info = orderData.PurchaseInfo.XMLDoc.SelectNodes("genxml/tally/*");

            foreach (XmlNode xml in info)
            {
                var newInfo = new NBrightInfo
                {
                    XMLData = xml.OuterXml
                };
                tallies.Add(new Tally
                {
                    OrderId = newInfo.ItemID,
                    IsTally = newInfo.GetXmlPropertyBool("genxml/istally"),
                    ManId = newInfo.GetXmlPropertyInt("genxml/manid"),
                    TallyMan = newInfo.GetXmlProperty("genxml/tallyman"),
                    TallyStartDate = newInfo.GetXmlProperty("genxml/tallystartdate"),
                    TallyEndDate = newInfo.GetXmlProperty("genxml/tallyenddate")
                });
            }
            return tallies;
        }

        public OrderInfo GetTallyOrder(string ordernumber)
        {
            return GetOrderForTallyList().SingleOrDefault(m => m.OrderNumber == ordernumber);
        }

        public IEnumerable<OrderInfo> GetOrderForTallyList()
        {
            NBrightBuyController nBrightBuy = new NBrightBuyController();
            List<OrderInfo> orders = new List<OrderInfo>();

            string search = $" and ([xmldata].value('(genxml/currenttallyman)[1]', 'nvarchar(max)') = '{PortalSettings.Current.UserId}')   ";
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
        /// 更新理貨狀態
        /// </summary>
        /// <param name="orderId"></param>
        public void UpdateTally(int orderId, UserInfo user)
        {
            OrderData orderData = new OrderData(PortalSettings.Current.PortalId, orderId);
            orderData.PurchaseInfo.SetXmlProperty("genxml/currenttallyman",user.UserID.ToString());

            var tallyNode = orderData.PurchaseInfo.GetXmlNode("genxml/tally");

            if (tallyNode == string.Empty)
            {
                string strXml = "<tally>";
                strXml += "<genxml>";
                strXml += $"<istally>{true}</istally>";
                strXml += $"<manid>{user.UserID}</manid>";
                strXml += $"<tallyman>{user.DisplayName}</tallyman>";
                strXml += $"<tallystartdate>{DateTime.Now.ToString()}</tallystartdate>";
                strXml += $"<tallyenddate/>";
                strXml += "</genxml>";
                strXml += "</tally>";
                orderData.PurchaseInfo.AddXmlNode(strXml, "tally", "genxml");
            }
            else
            {
                string strXml = "<genxml>";
                strXml += $"<istally>{true}</istally>";
                strXml += $"<manid>{user.UserID}</manid>";
                strXml += $"<tallyman>{user.DisplayName}</tallyman>";
                strXml += $"<tallystartdate>{DateTime.Now.ToString()}</tallystartdate>";
                strXml += $"<tallyenddate/>";
                strXml += "</genxml>";
                orderData.PurchaseInfo.AddSingleNode("tally", strXml, "genxml");
                orderData.Save();
            }
            orderData.Save();
        }

        protected override Func<ITallyRepository> GetFactory()
        {
            return () => new TallyRepository();
        }
    }
}