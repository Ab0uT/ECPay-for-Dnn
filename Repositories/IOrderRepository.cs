using Hugsys.ECPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderInfo> GetOrderStateList(string type);

        OrderInfo GetByOrderNumber(string ordernumber, string type);

        void UpdateOrder(int orderId, string logisticsId, string type, int freight = 0);

        void AddFreight(int orderId, string type);

    }

}