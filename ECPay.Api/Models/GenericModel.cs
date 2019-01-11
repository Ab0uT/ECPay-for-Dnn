using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPay.Api.Models
{
    public class GenericModel: SortedDictionary<string,string>
    {
        #region public function
        public string GetString(string name)
        {
            if (ContainsKey(name))
            {
                return this[name];
            }
            return string.Empty;
        }

        public int GetInt(string name, int defaultValue = 0)
        {
            var result = defaultValue;
            if (ContainsKey(name))
            {
                if (int.TryParse(name, out int temp))
                {
                    result = temp;
                }
            }
            return result;
        }

        public void SetString(string name, string value)
        {
            if (ContainsKey(name))
            {
                this[name] = value;
            }
            else
            {
                Add(name, value);
            }
        }

        public void SetInt(string name, int value)
        {
            SetString(name, value.ToString());
        }
        #endregion

        #region model
        /// <summary>
        /// 廠商編號
        /// </summary>
        [Required]
        [StringLength(10)]
        public string MerchantID
        {
            get { return GetString("MerchantID"); }
            set { SetString("MerchantID", value); }
        }

        [Required]
        public string HashKey
        {
            get { return GetString("HashKey"); }
            set { SetString("HashKey", value); }
        }

        [Required]
        public string HashIV
        {
            get { return GetString("HashIV"); }
            set { SetString("HashIV", value); }
        }
        #endregion
    }
}
