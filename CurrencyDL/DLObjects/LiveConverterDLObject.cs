using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace CurrencyDL.DLObjects
{
    public class LiveConverterDLObject
    {
        private string UrlLiveData;
        private CurrencyInfo CurrencyInfo;
        public LiveConverterDLObject()
        {
            UrlLiveData = "https://www.google.com/finance/quote/";
            CurrencyInfo = new CurrencyInfo();
        }

        #region Currency Info
        public Dictionary<string, string> GetCurrencyInfo()
        {
            return CurrencyInfo.GetCurrencyInfo();
        }
        #endregion

        #region Live
        public double getLiveData(string source, string target)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string tempUrl = UrlLiveData + source + "-" + target;
                    using (HttpResponseMessage response = client.GetAsync(tempUrl).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = content.ReadAsStringAsync().Result;
                            result = getBetween(result, "YMlKec fxKbKc\">", "<");
                            if (double.TryParse(result, out double OutVal))
                                return OutVal;
                            else
                                throw new WebException("Service is not available," +
                                    "\nPlease check your internet connection");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new WebException("Please check your internet connection");
                }
            }
        }

        private string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        #endregion

    }
}
