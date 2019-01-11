using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class APIResult
    {
        public bool Success { get; set; }

        public string Response { get; set; }

        public string Message { get; set; }

        public object Payload { get; set; }
    }
}
