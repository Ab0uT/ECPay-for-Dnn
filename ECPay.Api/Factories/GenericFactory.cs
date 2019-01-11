using DotNetNuke.Framework;
using ECPay.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ECPay.Api.Factories
{
    public class GenericFactory<TEntity> : ServiceLocator<IFactory<TEntity>, GenericFactory<TEntity>>,
        IFactory<TEntity> where TEntity : SortedDictionary<string, string>
    {

        protected override Func<IFactory<TEntity>> GetFactory()
        {
            return () => new GenericFactory<TEntity>();
        }

        public bool EnableSecurityProtocol { set { SecurityProtocol(value); } }

        public string PrintForECPay(string url, TEntity instance)
        {
            var model = AddCheckMacValue(instance);

            StringBuilder builder = new StringBuilder();

            builder.Append("<html><body>");
            builder.Append("<form name='postdata' target='NewFile' id='postdata' action='" + url + "' method='POST'>").AppendLine();
            foreach (var aa in model)
            {
                builder.Append("<input type='hidden' name='" + aa.Key + "' value='" + aa.Value + "'>").AppendLine();
            }
            builder.Append("</form>").AppendLine();
            builder.Append("<script> let theForm = document.forms['postdata'];  if (!theForm) { theForm = document.postdata; } theForm.submit(); </script>");
            builder.Append("</body></html>");

            return builder.ToString();
        }

        public async Task<APIResult> PostAsyncForECPay(string url, TEntity entity)
        {
            APIResult result = null;

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        HttpResponseMessage response = null;
                        var model = AddCheckMacValue(entity);
                        var formData = new FormUrlEncodedContent(model);
                        response = await client.PostAsync(url, formData);

                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string strResult = await response.Content.ReadAsStringAsync();

                                result = new APIResult
                                {
                                    Success = true,
                                    Response = strResult
                                };
                            }
                            else
                            {
                                result = new APIResult
                                {
                                    Success = false,
                                    Message = $"Error Code: {response.StatusCode}, Error Message: {response.RequestMessage}"
                                };
                            }
                        }
                        else
                        {
                            result = new APIResult
                            {
                                Success = false,
                                Message = "應用程式呼叫 API 發生異常"
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        result = new APIResult
                        {
                            Success = false,
                            Response = string.Empty,
                            Message = ex.Message,
                            Payload = ex
                        };
                    }
                }
            }
            return result;
        }

        #region private function
        private TEntity AddCheckMacValue(TEntity entity)
        {
            string macValue = string.Empty;
            string macHashKey = string.Empty;
            string macHashIV = string.Empty;

            foreach (var param in entity)
            {
                var key = param.Key;
                var value = param.Value;

                if (key != "HashKey" && key != "HashIV")
                {
                    macValue += $"&{key}={value}";
                }
                else
                {
                    if (key == "HashKey")
                        macHashKey = $"{key}={value}";
                    if (key == "HashIV")
                        macHashIV = $"&{key}={value}";
                }
            }
            string checkMacValue = macHashKey + macValue + macHashIV;
            string md5Value = Md5Value(checkMacValue);
            entity.Add("CheckMacValue", md5Value);

            entity.Remove("HashKey");
            entity.Remove("HashIV");

            return entity;
        }

        private string Md5Value(string value)
        {
            value = HttpUtility.UrlEncode(value).ToLower();
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("X2"));
            }

            return builder.ToString();
        }

        private void SecurityProtocol(bool enable = false)
        {
            if (enable)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                       SecurityProtocolType.Tls11 |
                                       SecurityProtocolType.Tls12;
        }
        #endregion
    }
}
