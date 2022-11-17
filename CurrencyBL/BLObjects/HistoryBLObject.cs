using CurrencyBE;
using CurrencyDL.DLObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyBL.BLObjects
{
    public class HistoryBLObject
    {
        private HistoryDLObject DLObject;

        public HistoryBLObject()
        {
            DLObject = new HistoryDLObject();
        }

        public List<CurrencyHistory> getHistory(string source, DateTime startDate, DateTime endDate)
        {
            if ((endDate - startDate).TotalDays < 0)
                throw new DataMisalignedException("Start date cannot be greater than end date");
            if ((endDate - DateTime.Now).TotalDays > 0)
                throw new DataMisalignedException("End date cannot be greater than current date");
            List<CurrencyHistory> result = DLObject.getHistory(startDate, endDate);
            try
            {
                if (source == "ILS")
                    return (from s in result
                            select new CurrencyHistory
                            {
                                Date = s.Date,
                                EURValue = s.EURValue / s.ILSValue,
                                GBPValue = s.GBPValue / s.ILSValue,
                                ILSValue = 1,
                                AUDValue = s.AUDValue / s.ILSValue,
                                USDValue = 1 / s.ILSValue
                            }).ToList();
                if (source == "EUR")
                    return (from s in result
                            select new CurrencyHistory
                            {
                                Date = s.Date,
                                EURValue = 1,
                                GBPValue = s.GBPValue / s.EURValue,
                                ILSValue = s.ILSValue / s.EURValue,
                                AUDValue = s.AUDValue / s.EURValue,
                                USDValue = 1 / s.EURValue
                            }).ToList();
                if (source == "GBP")
                    return (from s in result
                            select new CurrencyHistory
                            {
                                Date = s.Date,
                                EURValue = s.EURValue / s.GBPValue,
                                GBPValue = 1,
                                ILSValue = s.ILSValue / s.GBPValue,
                                AUDValue = s.AUDValue / s.GBPValue,
                                USDValue = 1 / s.GBPValue
                            }).ToList();
                if (source == "AUD")
                    return (from s in result
                            select new CurrencyHistory
                            {
                                Date = s.Date,
                                EURValue = s.EURValue / s.AUDValue,
                                GBPValue = s.GBPValue / s.AUDValue,
                                ILSValue = s.ILSValue / s.AUDValue,
                                AUDValue = 1,
                                USDValue = 1 / s.AUDValue
                            }).ToList();
                return result;
            }
            catch (Exception e) { throw e; }
        }
    }
}
