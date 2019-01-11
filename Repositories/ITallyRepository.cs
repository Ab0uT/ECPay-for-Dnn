using DotNetNuke.Entities.Users;
using Hugsys.ECPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Repositories
{
    public interface ITallyRepository
    {
        OrderInfo GetTallyOrder(string ordernumber);

        IEnumerable<OrderInfo> GetOrderForTallyList();

        IEnumerable<Tally> GetTallyManList(int orderId);

        Tally GetByTallyMan(int orderid);

        void UpdateTally(int orderId, UserInfo user);
    }
}