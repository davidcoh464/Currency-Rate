using CurrencyBL.BLObjects;
using System;
using System.Collections.Generic;

namespace CurrencyPL.Model
{
    public class LiveCurrencyModel
    {
        private LiveCurrencyBLObject BLObject { get; set; }

        public LiveCurrencyModel()
        {
            BLObject = new LiveCurrencyBLObject();
        }
        public void GetLiveCurrency()
        {
            try
            {
                LiveDataValue = BLObject.GetLiveCurrency(Source, Target);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Dictionary<string, string> GetCurrencyInfo()
        {
            return BLObject.CurrencyInfo();
        }

        public string Source { get; set; }
        public string Target { get; set; }

        public double LiveDataValue { get; set; }
    }
}
