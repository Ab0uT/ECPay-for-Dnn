using DotNetNuke.Entities.Portals;
using NBrightDNN;
using Nevoweb.DNN.NBrightBuy.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Models
{
    public static class Merchant
    {
        

        public static bool IsEnable => Info.GetXmlPropertyBool("genxml/dropdownlist/enable");

        public static string Type => Info.GetXmlProperty("genxml/dropdownlist/type");

        public static string HomeSubType => Info.GetXmlProperty("genxml/dropdownlist/homesubtype");

        public static bool IsCollection => Info.GetXmlPropertyBool("genxml/checkbox/iscollection");

        public static string MerchantID => Info.GetXmlProperty("genxml/textbox/merchantid");

        public static string HashKey => Info.GetXmlProperty("genxml/textbox/hashkey");

        public static string HashIV => Info.GetXmlProperty("genxml/textbox/hashkiv");

        public static string SenderName => Info.GetXmlProperty("genxml/textbox/sendername");

        public static string SenderPhone => Info.GetXmlProperty("genxml/textbox/senderphone");

        public static string SenderCellphone => Info.GetXmlProperty("genxml/textbox/sendercellphone");

        public static string SenderZipcode => Info.GetXmlProperty("genxml/textbox/senderzipcode");

        public static string SenderAddress => Info.GetXmlProperty("genxml/textbox/senderaddress");

        private static NBrightInfo Info => _nbrightInfo();

        private static NBrightInfo _nbrightInfo()
        {
            NBrightBuyController _nBright = new NBrightBuyController();

            return _nBright.GetByGuidKey(PortalSettings.Current.PortalId, -1, "SHIPPING", "OS_AllShipping");
        }
    }
}