using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingMethods
{
    public class ValidateMixTypeIDAttribute : RegularExpressionAttribute
    {
        public ValidateMixTypeIDAttribute(int pattern) : base(Regx(pattern))
        {

        }

        private static string Regx(int pattern)
        {
            return @"^[0-9a-zA-Z]{1," + pattern + "}$";
        }
    }
}
