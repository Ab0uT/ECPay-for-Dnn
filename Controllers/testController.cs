using DotNetNuke.Web.Api;
using ECPay.Api.Factories;
using ECPay.Api.Models;
using ECPay.Api.ShippingTypes;
using Hugsys.ECPay.Components;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hugsys.ECPay.Controllers
{
    public class TestController : DnnApiController
    {
        private IFactory<StorePickup> _storePickup;
        private IFactory<SearchOrders> _searchOrder;

        public TestController()
        {
            _storePickup = GenericFactory<StorePickup>.Instance;
            _searchOrder = GenericFactory<SearchOrders>.Instance;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> test()
        {
            var sp = new StorePickup
            {
                MerchantID = "2000132",
                GoodsAmount = 110,
                IsCollection = "N",
                LogisticsSubType = "UNIMART",
                LogisticsType = "CVS",
                MerchantTradeDate = $"{DateTime.Now.ToString()}",
                ReceiverCellPhone = "0983337029",
                ReceiverName = "Superuser",
                ReceiverStoreID = "991182",
                SenderCellPhone = "0912345678",
                SenderName = "六加天",
                SenderPhone = "041234567",
                ServerReplyURL = "http://example.com",
                HashKey = "5294y06JbISpM5x9",
                HashIV = "v77hoKGq4kWxNNIS",
                PlatformID = ""
            };
            _storePickup.EnableSecurityProtocol = true;
            var html = await _storePickup.PostAsyncForECPay(ECPayTestURL.SHIPPING_ORDER, sp);
            var result = ObjectConvert<StatusNotification>.QueryStringToJson(html.Response);
            return Request.CreateResponse(new { result });

        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Response(StatusNotification notification)
        {
            return Request.CreateResponse(new { notification });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> HelloWorld()
        {
            var sp = new SearchOrders
            {
                MerchantID = "2000132",
                AllPayLogisticsID = "158805",
                TimeStamp = (Int32)DateTimeOffset.Now.ToUnixTimeSeconds(),
                HashKey = "5294y06JbISpM5x9",
                HashIV = "v77hoKGq4kWxNNIS",
                PlatformID = ""
            };
            if (ModelState.IsValid)
            {
                _searchOrder.EnableSecurityProtocol = true;
                var response = await _searchOrder.PostAsyncForECPay(ECPayTestURL.QUERY_LOGISTICS_INFO, sp);

                if (response.Success)
                {
                    if (response.Response.Substring(0, 1) != "0")
                    {
                        var json = ObjectConvert<TradeInfoResponse>.QueryStringToJson(response.Response);
                        return Request.CreateResponse(new { json });
                    }
                    else
                    {
                        var result = new
                        {
                            message = response.Response.Substring(2)
                        };
                        return Request.CreateResponse(new { result });
                    }
                }
                else
                    return Request.CreateResponse(new { response });
            }

            else
                return Request.CreateResponse(new { message = false });

        }

        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage QueryString()
        {
            string querystring = "1|AllPayLogisticsID=158981&BookingNote=&CheckMacValue=5C5ECFEFB08C0525408D32C6538114DA&CVSPaymentNo=&CVSValidationNo=&GoodsAmount=110&LogisticsSubType=UNIMART&LogisticsType=CVS&MerchantID=2000132&MerchantTradeNo=GW1901041548427509&ReceiverAddress=&ReceiverCellPhone=0983337029&ReceiverEmail=&ReceiverName=Superuser&ReceiverPhone=&RtnCode=300&RtnMsg=訂單處理中(已收到訂單資料)&UpdateStatusDate=2019/01/04 15:48:42";
            var sf = querystring.Substring(2);
            var collection = HttpUtility.ParseQueryString(sf);
            string json = JsonConvert.SerializeObject(collection.AllKeys.ToDictionary(m => m, m => collection[m]));
            var result = JsonConvert.DeserializeObject<StatusNotification>(json);


            return Request.CreateResponse(new { result });
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage GetStoreInfo(MapResponse map)
        {
            return Request.CreateResponse(new { value = map });
    }
    }

  

    public class APIResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Payload { get; set; }
    }

    public class MapResponse
    {
        public string MerchantID { get; set; }

        public string MerchantTradeNo { get; set; }

        public string LogisticsSubType { get; set; }

        public string CVSStoreID { get; set; }

        public string CVSStoreName { get; set; }

        public string CVSAddress { get; set; }

        public string CVSTelephone { get; set; }

        public string CVSOutSide { get; set; }

        public string ExtraData { get; set; }
    }
}