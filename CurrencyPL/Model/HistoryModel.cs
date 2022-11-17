using CurrencyBL.BLObjects;
using CurrencyBE;
using System;
using System.Collections.Generic;

namespace CurrencyPL.Model
{
    public class HistoryModel
    {
        public List<CurrencyHistory> CurrencyHistories { get; set; }
        private HistoryBLObject BlObject { get; set; }
        public HistoryModel()
        {
            CurrencyHistories = new List<CurrencyHistory>();
            BlObject = new HistoryBLObject();
        }
        public void GetData()
        {
            try
            {
                CurrencyHistories = BlObject.getHistory(Source, StartDate, EndDate);
            }
            catch (Exception e) { throw e; }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Source { get; set; }
    }
}
