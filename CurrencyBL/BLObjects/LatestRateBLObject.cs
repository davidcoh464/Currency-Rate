using CurrencyBE;
using CurrencyDL.DLObjects;
using System;
using System.Collections.Generic;

namespace CurrencyBL.BLObjects
{
    public class LatestRateBLObject
    {
        private LatestRateDLObject DLObject;
        public LatestRateBLObject()
        {
            DLObject = new LatestRateDLObject();
        }

        public Dictionary<string, string> CurrencyInfo()
        {
            return DLObject.GetCurrencyInfo();
        }
        public double getConvertedValue(string source, string target)
        {
            return DLObject.getConvertedValue(source, target);
        }
        public List<LatestRate> getLatestRate(string source, string targets)
        {
            try
            {
                return DLObject.getLatestRate(source, targets);
            }
            catch (Exception e) { throw e; }
        }
    }
}
