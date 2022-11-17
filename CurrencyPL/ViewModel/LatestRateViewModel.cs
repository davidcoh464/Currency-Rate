using CurrencyPL.Command;
using CurrencyPL.Model;
using CurrencyPL.ViewModel;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CurrencyPL
{
    public class LatestRateViewModel : BaseViewModel
    {

        #region Properties
        public ObservableCollection<CurrencyBE.LatestRate> _LatestRateList;
        public ObservableCollection<CurrencyBE.LatestRate> LatestRateList
        {
            get { return _LatestRateList; }
            set
            {
                _LatestRateList = value;
                OnPropertyChanged("LatestRateList");
            }
        }
        public Dictionary<string, string> CurrencyInfo { get; set; }

        public LatestRateModel LRModel { get; set; }
        public CurrencyCommand ShowDataCommand { get; set; }
        #endregion

        #region Constractor
        public LatestRateViewModel()
        {
            LRModel = new LatestRateModel();
            LatestRateList = new ObservableCollection<CurrencyBE.LatestRate>();
            
            ShowDataCommand = new CurrencyCommand();
            ShowDataCommand.OneParemterEvent += GetData;
            
            CurrencyInfo = new Dictionary<string, string>();
            CurrencyInfo = LRModel.GetCurrencyInfo();
            foreach (var key in CurrencyInfo.Keys.ToList())
            {
                CurrencyInfo[key] = key + ": " + CurrencyInfo[key];
            }
        }
        #endregion

        #region Funtions
        private void GetData(string source)
        {
            if (source == null || !CurrencyInfo.ContainsKey(source))
            {
                MessageBox.Show("currency source dont exist","Missing data");
                return;
            }
            try
            {
                LRModel.Source = source;
                LatestRateList = LRModel.GetData().Select(x =>
                new CurrencyBE.LatestRate { CurrencyType = CurrencyInfo[x.CurrencyType], Rate = x.Rate }).ToObservableCollection();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,e.GetType().Name);
            }
        }
        #endregion
    }
}
