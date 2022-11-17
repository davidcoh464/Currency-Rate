using CurrencyBL.BLObjects;
using CurrencyBE;
using System;
using System.Collections.Generic;

namespace CurrencyPL.Model
{
    public class LatestRateModel
    {
        private LatestRateBLObject BLObject;
        
        public LatestRateModel()
        {
            BLObject = new LatestRateBLObject();
            Target = "";
        }

        public List<LatestRate> GetData()
        {
            try
            {
                return BLObject.getLatestRate(Source,Target);
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

    }
}
