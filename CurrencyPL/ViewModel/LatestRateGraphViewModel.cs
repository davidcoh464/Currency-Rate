using CurrencyPL.Command;
using CurrencyPL.Model;
using CurrencyPL.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CurrencyPL
{
    public class LatestRateGraphViewModel : BaseViewModel
    {
        #region Constructor
        public LatestRateGraphViewModel()
        {
            LRModel = new LatestRateModel();
            ShowDataCommand = new CurrencyCommand();
            ShowDataCommand.OneParemterEvent += GetData;

            CurrencyInfo = LRModel.GetCurrencyInfo();
            foreach (var key in CurrencyInfo.Keys.ToList())
                CurrencyInfo[key] = key + ": " + CurrencyInfo[key];

            List<BoolStringClass> values = new List<BoolStringClass>();
            foreach (var x in CurrencyInfo.Values)
                values.Add(new BoolStringClass { IsSelected = false, TheText = x });

            CheckBoxList = new ObservableCollection<BoolStringClass>(values);
        }

        #endregion

        #region Function
        private void GetData(string source)
        {
            if (source == null || !CurrencyInfo.ContainsKey(source))
            {
                MessageBox.Show("currency source dont exist", "Missing data");
                return;
            }
            try
            {
                string targets = "";
                foreach (var x in CheckBoxList)
                {
                    if (x.IsSelected)
                        targets += "," + x.TheText.Substring(0, 3);
                }
                if (targets == "")
                {
                    MessageBox.Show("You must select at least one target", "Missing data");
                    return;
                }
                targets = targets.Remove(0, 1);
                if (LRModel.Source == source && LRModel.Target == targets)
                    return;
                LRModel.Source = source;
                LRModel.Target = targets;
                LatestRateList = new ObservableCollection<CurrencyBE.LatestRate>(LRModel.GetData()
                    .OrderByDescending(l => l.Rate));
            }
            catch (Exception e) { MessageBox.Show(e.Message, e.GetType().Name); }
        }
        #endregion

        #region Properties

        private ObservableCollection<BoolStringClass> _CheckBoxList;
        public ObservableCollection<BoolStringClass> CheckBoxList
        {
            get { return _CheckBoxList; }
            set
            {
                _CheckBoxList = value;
                OnPropertyChanged("CheckBoxList");
            }
        }

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
    }

    #region Class For CheckBox
    public class BoolStringClass
    {
        public string TheText { get; set; }
        public bool IsSelected { get; set; }

        public override string ToString() { return TheText; }
    }
    #endregion
}