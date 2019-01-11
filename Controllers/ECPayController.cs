using DotNetNuke.Web.Api;
using ECPay.Api.Factories;
using ECPay.Api.Models;
using ECPay.Api.ShippingTypes;
using Hugsys.ECPay.Components;
using Hugsys.ECPay.Models;
using Hugsys.ECPay.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hugsys.ECPay.Controllers
{
    public class ECPayController : DnnApiController
    {
        #region ctor

        private readonly IFactory<HomeDelivery> _homeDelivery;
        private readonly IFactory<StorePickup> _storePickup;
        private readonly IFactory<ReturnHomeDelivery> _returnHomeDelivery;
        private readonly IFactory<ReturnFami> _returnFami;
        private readonly IFactory<ReturnHiLife> _returnHiLife;
        private readonly IFactory<ReturnUniMart> _returnUniMart;
        private readonly IFactory<PrintTradeDocument> _tradeDocument;
        private readonly IFactory<SearchOrders> _searchOrder;
        private readonly IOrderRepository _orderRepository;

        public ECPayController()
        {
            _homeDelivery = GenericFactory<HomeDelivery>.Instance;
            _storePickup = GenericFactory<StorePickup>.Instance;
            _returnHomeDelivery = GenericFactory<ReturnHomeDelivery>.Instance;
            _returnFami = GenericFactory<ReturnFami>.Instance;
            _returnHiLife = GenericFactory<ReturnHiLife>.Instance;
            _returnUniMart = GenericFactory<ReturnUniMart>.Instance;
            _searchOrder = GenericFactory<SearchOrders>.Instance;
            _tradeDocument = GenericFactory<PrintTradeDocument>.Instance;
            _orderRepository = OrderRepository.Instance;

        }

        private string Host { get { return HttpContext.Current.Request.Url.Host; } }
        private string Http { get { return HttpContext.Current.Request.IsSecureConnection ? "https" : "http"; } }

        #endregion

        #region 當物流為宅配時
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">為了繼續保留訂單編號，在response的時候才會認得該物流的訂單</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public async Task<HttpResponseMessage> HomeDelivery(int orderId, HomeDelivery value)
        {
            if (Merchant.IsEnable == false && Merchant.Type != "B")
                return Request.CreateResponse(new { message = "請啟用物流或更改為 B2C 類型" });

            value.MerchantID = Merchant.MerchantID;
            value.HashKey = Merchant.HashKey;
            value.HashIV = Merchant.HashIV;
            value.SenderName = Merchant.SenderName;
            value.SenderPhone = Merchant.SenderPhone;
            value.SenderCellPhone = Merchant.SenderCellphone;
            value.MerchantTradeDate = DateTime.Now.ToString();
            value.IsCollection = Merchant.IsCollection ? "Y" : "N";
            value.SenderZipCode = Merchant.SenderZipcode;
            value.SenderAddress = Merchant.SenderAddress;
            value.ServerReplyURL = $"{Http}://{Host}/api/ECPay/ECPay/Message?orderId={orderId}";//Server 端回覆網址

            _homeDelivery.EnableSecurityProtocol = true;
            var request = await _homeDelivery.PostAsyncForECPay(Links.Create, value);

            if (request.Success)
            {
                if (request.Response.Substring(0, 1) != "0" && request.Response.Substring(0, 2) != "10")
                {
                    var json = ObjectConvert<StatusNotification>.QueryStringToJson(request.Response);

                    if (json.RtnCode == 300)
                    {
                        string info = await TradeInfo(json);

                        if (int.TryParse(info, out int temp))
                            _orderRepository.UpdateOrder(orderId, json.AllPayLogisticsID, "080", temp);

                        var ptd = new PrintTradeDocument
                        {
                            MerchantID = Merchant.MerchantID,
                            AllPayLogisticsID = json.AllPayLogisticsID,
                            PlatformID = "",
                            HashKey = Merchant.HashKey,
                            HashIV = Merchant.HashIV
                        };

                        string printHtml = _tradeDocument.PrintForECPay(Links.TradeDocument, ptd);

                        return Request.CreateResponse(new { html = printHtml });
                    }
                    else
                        return Request.CreateResponse(new { message = json.RtnMsg });
                }
                else
                    return Request.CreateResponse(new { message = request.Response });
            }
            else
                return Request.CreateResponse(new { message = request.Message });

        }
        #endregion

        #region 當物流為超商取貨時
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">為了繼續保留訂單編號，在response的時候才會認得該物流的訂單</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public async Task<HttpResponseMessage> StorePickup(int orderId, StorePickup value)
        {
            //預設值 不能做更改
            value.MerchantID = Merchant.MerchantID;
            value.HashKey = Merchant.HashKey;
            value.HashIV = Merchant.HashIV;
            value.SenderName = Merchant.SenderName;
            value.SenderPhone = Merchant.SenderPhone;
            value.SenderCellPhone = Merchant.SenderCellphone;
            value.MerchantTradeDate = DateTime.Now.ToString();
            value.IsCollection = Merchant.IsCollection ? "Y" : "N";
            value.ServerReplyURL = $"{Http}://{Host}/api/ECPay/ECPay/Message?orderId={orderId}";

            _storePickup.EnableSecurityProtocol = true;
            var request = await _storePickup.PostAsyncForECPay(Links.Create, value);

            if (request.Success)
            {
                if (request.Response.Substring(0, 1) != "0" && request.Response.Substring(0,2)!= "10" )
                {
                    var json = ObjectConvert<StatusNotification>.QueryStringToJson(request.Response);

                    if (json.RtnCode == 300)
                    {
                        string info = await TradeInfo(json);

                        if (int.TryParse(info, out int temp))
                            _orderRepository.UpdateOrder(orderId, json.AllPayLogisticsID, "080", temp);

                        var ptd = new PrintTradeDocument
                        {
                            MerchantID = Merchant.MerchantID,
                            AllPayLogisticsID = json.AllPayLogisticsID,
                            PlatformID = "",
                            HashKey = Merchant.HashKey,
                            HashIV = Merchant.HashIV
                        };

                        string printHtml = _tradeDocument.PrintForECPay(Links.TradeDocument, ptd);

                        return Request.CreateResponse(new { html = printHtml });
                    }
                    else
                        return Request.CreateResponse(new { message = json.RtnMsg });
                }
                else
                    return Request.CreateResponse(new { message = request.Response });
            }
            else
                return Request.CreateResponse(new { message = request.Message });

        }
        #endregion

        #region 當逆物流為宅配時
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">為了繼續保留訂單編號，在response的時候才會認得該物流的訂單</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public async Task<HttpResponseMessage> RHomeDelivery(int orderId, ReturnHomeDelivery value)
        {

            value.MerchantID = Merchant.MerchantID;
            value.HashKey = Merchant.HashKey;
            value.HashIV = Merchant.HashIV;
            value.SenderName = Merchant.SenderName;
            value.SenderPhone = Merchant.SenderPhone;
            value.SenderCellPhone = Merchant.SenderCellphone;
            value.SenderZipCode = Merchant.SenderZipcode;
            value.SenderAddress = Merchant.SenderAddress;
            value.ServerReplyURL = $"{Http}://{Host}/api/ECPayShipping/ECPay/ReturnMessage?orderId={orderId}";//Server 端回覆網址

            _returnHomeDelivery.EnableSecurityProtocol = true;
            var request = await _returnHomeDelivery.PostAsyncForECPay(Links.ReturnHome, value);

            if (request.Success)
            {
                if (request.Response.Substring(0, 1) == "1")
                    return Request.CreateResponse(new { success = request.Response.Substring(2) });
                else
                    return Request.CreateResponse(new { success = request.Response.Substring(2) });
            }
            else
                return Request.CreateResponse(new { success = request.Success });

        }
        #endregion

        #region 當逆物流為超商取貨時
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">為了繼續保留訂單編號，在response的時候才會認得該物流的訂單</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public async Task<HttpResponseMessage> RFami(int orderId, ReturnFami value)
        {


            //預設值 不能做更改
            value.MerchantID = Merchant.MerchantID;
            value.HashKey = Merchant.HashKey;
            value.HashIV = Merchant.HashIV;
            value.SenderName = Merchant.SenderName;
            value.SenderPhone = Merchant.SenderPhone;
            value.ServerReplyURL = $"{Http}://{Host}/api/ECPayShipping/ECPay/ReturnMessage?orderId={orderId}";//Server 端回覆網址

            _returnFami.EnableSecurityProtocol = true;
            var request = await _returnFami.PostAsyncForECPay(Links.ReturnFamiCVS, value);

            if (request.Success)
            {
                var result = request.Response.Split('|');

                if (result.Length > 1)
                    return Request.CreateResponse(new { RtnMerchantTradeNo = result[0], RtnOrderNo = result[1] });
                else
                    return Request.CreateResponse(new { ErrorMessage = result[0] });
            }
            else
                return Request.CreateResponse(new { success = request.Success });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">為了繼續保留訂單編號，在response的時候才會認得該物流的訂單</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public async Task<HttpResponseMessage> RUniMart(int orderId, ReturnUniMart value)
        {

            //預設值 不能做更改
            value.MerchantID = Merchant.MerchantID;
            value.HashKey = Merchant.HashKey;
            value.HashIV = Merchant.HashIV;
            value.SenderName = Merchant.SenderName;
            value.SenderPhone = Merchant.SenderPhone;
            value.ServerReplyURL = $"{Http}://{Host}/api/ECPayShipping/ECPay/ReturnMessage?orderId={orderId}";//Server 端回覆網址

            _returnFami.EnableSecurityProtocol = true;
            var request = await _returnUniMart.PostAsyncForECPay(Links.ReturnUniMartCVS, value);

            if (request.Success)
            {
                var result = request.Response.Split('|');

                if (result.Length > 1)
                    return Request.CreateResponse(new { RtnMerchantTradeNo = result[0], RtnOrderNo = result[1] });
                else
                    return Request.CreateResponse(new { ErrorMessage = result[0] });
            }
            else
                return Request.CreateResponse(new { success = request.Success });

        }

        #endregion

        #region 綠界回傳通知訊息
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Message(int orderId, StatusNotification value)
        {
            if (value.RtnCode == 2030 || value.RtnCode == 3024 || value.RtnCode == 3006)
                _orderRepository.UpdateOrder(orderId, value.MerchantTradeNo, "090");

            if (value.RtnCode == 2063 || value.RtnCode == 2073 || value.RtnCode == 3018)
                _orderRepository.UpdateOrder(orderId, value.MerchantTradeNo, "110");

            if (value.RtnCode == 3003)
                _orderRepository.UpdateOrder(orderId, value.MerchantTradeNo, "100");

            return Request.CreateResponse(value.RtnCode);
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage ReturnMessage(int orderId, ReturnStatusNotification value)
        {
            //2030 3024 3006 
            if (value.RtnCode == 2030 || value.RtnCode == 3024 || value.RtnCode == 3006)
                _orderRepository.UpdateOrder(orderId, value.RtnMerchantTradeNo, "090");

            if (value.RtnCode == 2063 || value.RtnCode == 2073 || value.RtnCode == 3018)
                _orderRepository.UpdateOrder(orderId, value.RtnMerchantTradeNo, "110");

            if (value.RtnCode == 3003)
                _orderRepository.UpdateOrder(orderId, value.RtnMerchantTradeNo, "100");

            return Request.CreateResponse(value.RtnCode);
        }



        #endregion

        private async Task<string> TradeInfo(StatusNotification sn)
        {
            SearchOrders so = new SearchOrders
            {
                MerchantID = sn.MerchantID,
                AllPayLogisticsID = sn.AllPayLogisticsID,
                TimeStamp = (Int32)DateTimeOffset.Now.ToUnixTimeSeconds(),
                HashKey = Merchant.HashKey,
                HashIV = Merchant.HashIV,
                PlatformID = string.Empty
            };

            _searchOrder.EnableSecurityProtocol = true;
            var request = await _searchOrder.PostAsyncForECPay(Links.TradeInfo, so);

            if (request.Success)
            {
                if (request.Response.Substring(0, 1) != "0")
                {
                    var json = ObjectConvert<TradeInfoResponse>.QueryStringToJson(request.Response);
                    return json.HandlingCharge.ToString();
                }
                else
                    return request.Response;
            }
            else
                return request.Message;
        }
    }
}
