using Hugsys.ECPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Items> GetItemList(int orderId);

        Items GetByItem();
    }
}