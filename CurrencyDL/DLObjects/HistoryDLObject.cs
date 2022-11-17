using CurrencyBE;
using CurrencyDL.JsonConverter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CurrencyDL.DLObjects
{
    public class HistoryDLObject
    {
        private string UrlHistory;
        public HistoryDLObject()
        {
            UrlHistory = "https://api.exchangerate.host/timeseries?";
        }

        #region History
        public List<CurrencyHistory> getHistory(DateTime startDate, DateTime endDate)
        {
            using (var ctx = new CurrencyDB())
            {
                try
                {
                    if (ctx.Database.Exists() && ctx.USD_History.Any())
                    {
                        string maxDate = ctx.USD_History.Max(p => p.Date);
                        DateTime maxDateTime = DateTime.Parse(maxDate);
                        //Adding to the database from the maximum date currently
                        //in the database to the current date
                        if ((DateTime.Now - maxDateTime).TotalDays > 1)
                            addToDB(maxDateTime.AddDays(1), DateTime.Now);
                    }
                    else // if database dont exists or empty
                    {
                        try
                        {
                            addToDB(new DateTime(2004, 1, 1), DateTime.Now);
                        }
                        catch (Exception)
                        {
                            throw new WebException("Sorvice is not avilablle," +
                               "\nPlease check your internet conection");
                        }
                    }
                }
                catch (Exception) { }
                string start = startDate.ToString("yyyy-MM-dd");
                string end = endDate.ToString("yyyy-MM-dd");
                return (from s in ctx.USD_History
                        where string.Compare(s.Date, start) >= 0 && string.Compare(s.Date, end) <= 0
                        orderby s.Date
                        select new CurrencyHistory
                        {
                            Date = s.Date,
                            EURValue = s.EURValue,
                            GBPValue = s.GBPValue,
                            ILSValue = s.ILSValue,
                            AUDValue = s.AUDValue,
                            USDValue = 1
                        }).ToList();
            }
        }
        #endregion

        #region History private
        private void findMissingData()
        {
            using (var ctx = new CurrencyDB())
            {
                List<string> DBdata = (from s in ctx.USD_History
                                       orderby s.Date
                                       select s.Date
                                          ).ToList();
                for (int i = 0; i < DBdata.Count - 1; i++)
                {
                    DateTime currrent = DateTime.Parse(DBdata[i]).AddDays(1);
                    DateTime next = DateTime.Parse(DBdata[i + 1]);
                    if (next != currrent)
                        addToDB(currrent, next.AddDays(-1));
                }
            }
        }
        private void addToDB(DateTime startDate, DateTime endDate)
        {
            while ((endDate - startDate).TotalDays > 365)
            {
                string start = startDate.ToString("yyyy-MM-dd");
                startDate = startDate.AddYears(1);
                string end = startDate.ToString("yyyy-MM-dd");
                insertHistoryToDB(start, end);
                startDate = startDate.AddDays(1);
            }
            insertHistoryToDB(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        }
        private void insertHistoryToDB(string startDate, string endDate)
        {
            var linkdata = UrlHistory + "start_date=" + startDate + "&end_date=" + endDate +
               "&base=" + "USD" + "&symbols=ILS,EUR,AUD,GBP";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(linkdata);
                JsonToHistoryInfo myHistoryInfo = JsonConvert.DeserializeObject<JsonToHistoryInfo>(json);
                if (myHistoryInfo.Rates == null)
                {
                    throw new WebException("Sorvice is not avilablle," +
                        "\nPlease check your internet conection");

                }
                using (var ctx = new CurrencyDB())
                {
                    foreach (var history in myHistoryInfo.Rates)
                    {
                        var coin = new USDHistory();
                        coin.Date = history.Key;
                        if (history.Value.TryGetValue("EUR", out double EURvalue))
                            coin.EURValue = EURvalue;
                        else
                            break;
                        if (history.Value.TryGetValue("ILS", out double ILSvalue))
                            coin.ILSValue = ILSvalue;
                        else
                            break;
                        if (history.Value.TryGetValue("GBP", out double GBPvalue))
                            coin.GBPValue = GBPvalue;
                        else
                            break;
                        if (history.Value.TryGetValue("AUD", out double AUDvalue))
                            coin.AUDValue = AUDvalue;
                        else
                            break;

                        ctx.USD_History.Add(coin);
                    }
                    ctx.SaveChanges();
                }
            }
        }
        #endregion
    }
}
