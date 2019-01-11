using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.ShippingMethods
{
    public class StringDictionaryAttribute : StringLengthAttribute
    {
        public StringDictionaryAttribute(int maximumLength) : base(maximumLength)
        {
        }

      
    }
}
