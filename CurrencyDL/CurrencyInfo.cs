using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyDL
{
    internal class CurrencyInfo
    {
        private string UrlCurrencyInfo;
        public CurrencyInfo()
        {
            UrlCurrencyInfo = "countries.json";
        }
        public Dictionary<string, string> GetCurrencyInfo()
        {
            using (var reader = new StreamReader(UrlCurrencyInfo))
            {
                var jsonFromFile = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFromFile);
            }
        }
    }
}
