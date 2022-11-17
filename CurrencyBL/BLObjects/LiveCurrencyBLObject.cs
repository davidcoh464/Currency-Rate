using CurrencyDL.DLObjects;
using System;
using System.Collections.Generic;

namespace CurrencyBL.BLObjects
{
    public class LiveCurrencyBLObject
    {
        private LiveConverterDLObject DLObject;
        public LiveCurrencyBLObject()
        {
            DLObject = new LiveConverterDLObject();
        }

        public Dictionary<string, string> CurrencyInfo()
        {
            return DLObject.GetCurrencyInfo();
        }

        public double GetLiveCurrency(string source, string target)
        {
            try
            {
                return DLObject.getLiveData(source, target);
            }
            catch (Exception e) { throw e; }
        }

    }
}
