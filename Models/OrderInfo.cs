using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Models
{
    public class OrderInfo
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public string OrderStatus { get; set; }

        public string Shipping { get; set; }

        public string LogisticsType { get; set; }

        public string LogisticsSubType { get; set; }

        public string StoreId { get; set; }

        public string StoreName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string Unit { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public decimal Freight { get; set; }

        public int Total { get; set; }

        public string AllPayLogisticsID { get; set; }

        public bool Print { get; set; }

    }

    public class Items
    {
        [JsonIgnore]
        public int OrderId { get; set; }

        public int Qty { get; set; }

        public string ProductName { get; set; }

        public string Model { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductTotal { get; set; }

    }

    public class Tally
    {
        [JsonIgnore]
        public int? OrderId { get; set; }

        public bool? IsTally { get; set; } = false;

        public int? ManId { get; set; } = 0;

        public string TallyMan { get; set; } = string.Empty;

        public string TallyStartDate { get; set; } = string.Empty;

        public string TallyEndDate { get; set; } = string.Empty;

    }
}