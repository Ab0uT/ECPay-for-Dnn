using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay.Components
{
    public static class ObjectConvert<T> where T : class
    {
        public static T QueryStringToJson(string queryString)
        {
            string subStr = queryString.Substring(2);
            NameValueCollection collection = HttpUtility.ParseQueryString(subStr);
            string serialize = JsonConvert.SerializeObject(
                collection.AllKeys.ToDictionary(k => k, k => collection[k])
                );
            T json = JsonConvert.DeserializeObject<T>(serialize);

            return json;
        }
    }
}