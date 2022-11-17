using CurrencyBL.BLObjects;
using System;
using System.Collections.Generic;

namespace CurrencyPL.Model
{
    public class ConvertModel
    {
        private LatestRateBLObject BlObject { get; set; }
        public ConvertModel()
        {
            BlObject = new LatestRateBLObject();
        }

        public void GetData()
        {
            try
            {
                UnitValue = BlObject.getConvertedValue(SourceType, TargetType);
            }
            catch (Exception e) { throw e; }
        }
        public Dictionary<string, string> GetCurrencyInfo()
        {
            return BlObject.CurrencyInfo();
        }
        public double UnitValue { get; set; }
        public string SourceType { get; set; }
        public string TargetType { get; set; }
    }
}
