using DotNetNuke.Web.Api;
using ECPay.Api.Factories;
using ECPay.Api.Models;
using Hugsys.ECPay.Models;
using Hugsys.ECPay.Repositories;
using Hugsys.ECPay.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Hugsys.ECPay.Controllers
{
    public class StockController : DnnApiController
    {
        #region ctor
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ITallyRepository _tallyRepository;
        private readonly IFactory<SearchOrders> _searchOrders;
        private readonly IFactory<PrintTradeDocument> _tradeDocument;


        public StockController()
        {
            _orderRepository = OrderRepository.Instance;
            _itemRepository = ItemRepository.Instance;
            _tallyRepository = TallyRepository.Instance;
            _searchOrders = GenericFactory<SearchOrders>.Instance;
            _tradeDocument = GenericFactory<PrintTradeDocument>.Instance;
        }

        #endregion


        /// <summary>
        /// 取得所有訂單
        /// </summary>
        /// <param name="state">選擇訂單哪一種狀態</param>
        /// <returns></returns>
        [HttpGet]
        [DnnAuthorize]
        public HttpResponseMessage GetOrderStateList(string state)
        {
            var orderInfos = _orderRepository.GetOrderStateList(state);
            List<OrderDetailViewModel> viewModel = new List<OrderDetailViewModel>();
            foreach (var i in orderInfos)
            {
                viewModel.Add(new OrderDetailViewModel
                {
                    Order = i,
                    Items = _itemRepository.GetItemList(i.OrderId),
                    Tally = _tallyRepository.GetByTallyMan(i.OrderId)
                });
            }

            var response = new
            {
                value = viewModel.AsEnumerable()
            };

            return Request.CreateResponse(response);
        }

        /// <summary>
        /// 取得該單筆訂單
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        [DnnAuthorize]
        public HttpResponseMessage GetOrder(string ordernumber)
        {
            string state = "040";
            var value = _orderRepository.GetByOrderNumber(ordernumber, state);
            var items = _itemRepository.GetItemList(value.OrderId);

            var response = new
            {
                value,
                items
            };

            return Request.CreateResponse(response);
        }

        [HttpGet]
        [DnnAuthorize]
        public HttpResponseMessage GetTallyOrder(string ordernumber)
        {
            var value = _tallyRepository.GetTallyOrder(ordernumber);
            var items = _itemRepository.GetItemList(value.OrderId);

            int type = 0;

            if (Merchant.IsEnable)
            {
                if (Merchant.Type == "C")
                    type = 1;
            }
            else
                type = 2;
          

            return Request.CreateResponse(new
            {
                value,
                items,
                isCollection = Merchant.IsCollection,
                shippingType = type
            });
        }

        /// <summary>
        /// 取得該理貨人員清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [DnnAuthorize]
        public HttpResponseMessage GetTallyManList()
        {
            //取得物流訂單清單
            var tallyInfos = _tallyRepository.GetOrderForTallyList();


            //從openstore 裡的XML資料 轉換成 Model
            List<OrderDetailViewModel> viewModel = new List<OrderDetailViewModel>();
            foreach (var i in tallyInfos)
            {
                var addViewModel = new OrderDetailViewModel
                {
                    Order = i,
                    Items = _itemRepository.GetItemList(i.OrderId)
                };

                if (_tallyRepository.GetByTallyMan(i.OrderId) != null)
                {
                    addViewModel.Tally = _tallyRepository.GetByTallyMan(i.OrderId);
                    viewModel.Add(addViewModel);
                }
            }


            // 取自該理貨人員的需要清單
            var getByTallyMan = viewModel
                .Where(m => m.Tally.ManId == UserInfo.UserID)
                .OrderByDescending(m => m.Tally.TallyStartDate).AsEnumerable();

            var response = new
            {
                value = getByTallyMan
            };

            return Request.CreateResponse(response);
        }

        /// <summary>
        /// 紀錄理貨人員(啟用理貨後就會記錄訂單裡)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public HttpResponseMessage TallyRecord(int orderId)
        {
            var user = UserInfo;

            _tallyRepository.UpdateTally(orderId, user);

            var response = new
            {
                success = true
            };
            return Request.CreateResponse(response);
        }



        /// <summary>
        /// 列印託運單
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [DnnAuthorize]
        public HttpResponseMessage PrintTradeDocument(string orderNumber)
        {
            // 先取得訂單裡 紀錄一筆的物流編號
            var value = _orderRepository.GetByOrderNumber(orderNumber, "080");

            var tdModel = new PrintTradeDocument
            {
                MerchantID = Merchant.MerchantID,
                AllPayLogisticsID = value.AllPayLogisticsID, //物流編號
                PlatformID = "",
                HashKey = Merchant.HashKey,
                HashIV = Merchant.HashIV
            };

            // 取得 tdModel 裡的值後 傳送至列印流程
            var printHtml = _tradeDocument.PrintForECPay(Links.TradeDocument, tdModel);

            var response = new
            {
                html = printHtml
            };

            return Request.CreateResponse(response);
        }
    }
}