using CurrencyBE;
using CurrencyDL.JsonConverter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CurrencyDL.DLObjects
{
    public class LatestRateDLObject
    {

        private string UrlLatestRate;
        private CurrencyInfo CurrencyInfo;

        public LatestRateDLObject()
        {
            UrlLatestRate = "https://api.exchangerate.host/latest?";
            CurrencyInfo = new CurrencyInfo();
        }

        #region Currency Info
        public Dictionary<string, string> GetCurrencyInfo()
        {
            return CurrencyInfo.GetCurrencyInfo();
        }
        #endregion

        #region Latest Rate
        public List<LatestRate> getLatestRate(string source, string targets)
        {
            var linkdata = UrlLatestRate + "base=" + source;
            if (targets != "")
                linkdata += "&symbols=" + targets;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(linkdata);
                    JsonToLatestRate myRate = JsonConvert.DeserializeObject<JsonToLatestRate>(json);
                    return (from rate in myRate.Rates
                            select new LatestRate
                            {
                                CurrencyType = rate.Key,
                                Rate = rate.Value
                            }).ToList();
                }
                catch (Exception)
                {
                    throw new WebException("Please check your internet connection");
                }
            }
        }
        #endregion

        #region Convert
        public double getConvertedValue(string source, string target)
        {
            string linkdata = UrlLatestRate + "base=" + source + "&symbols=" + target;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(linkdata);
                    JsonToLatestRate myRate = JsonConvert.DeserializeObject<JsonToLatestRate>(json);
                    return myRate.Rates[target];
                }

                catch (Exception) { throw new WebException("Please check your internet connection"); }
            }
        }
        #endregion
    }
}
