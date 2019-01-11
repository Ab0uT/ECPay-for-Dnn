using Hugsys.ECPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.ViewModels
{
    public class OrderDetailViewModel
    {
        public OrderInfo Order { get; set; }

        public IEnumerable<Items> Items { get; set; }

        public Tally Tally { get; set; }
    }
}