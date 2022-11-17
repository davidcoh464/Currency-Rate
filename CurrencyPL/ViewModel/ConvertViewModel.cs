using CurrencyPL.Command;
using CurrencyPL.Model;
using CurrencyPL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CurrencyPL
{
    public class ConvertViewModel : BaseViewModel
    {
        #region Constructor
        public ConvertViewModel()
        {
            CurrencyModel = new ConvertModel();

            ConvertCommand = new CurrencyCommand();
            ConvertCommand.OneParemterEvent += OnclickConvert;

            CurrencyInfo = CurrencyModel.GetCurrencyInfo();
            foreach (var key in CurrencyInfo.Keys.ToList())
            {
                CurrencyInfo[key] = key + ": " + CurrencyInfo[key];
            }
            SourceUpdated = TargetUpdated = false;
            UnitValue = 1;
            SourceAmount = "1";
            SourceType = "USD";
            TargetType = "ILS";
        }
        #endregion

        #region Functions

        private void OnclickConvert(string obj)
        {
            if (obj == "swap")
                SwapData();
            else
                GetData();
        }
        private void SwapData()
        {
            var tempSource = SourceType;
            SourceType = TargetType;
            TargetType = tempSource;
            if (CurrencyModel.SourceType != TargetType || CurrencyModel.TargetType != SourceType)
            {
                GetData();
                return;
            }

            if (UnitValue != 0)
                UnitValue = 1 / UnitValue;
            UpdateModel();

            double source_double;
            if (!double.TryParse(SourceAmount, out source_double))
                source_double = 1;
            SourceAmount = source_double.ToString();
            SourceUpdated = true;
            TargetAmount = (source_double * UnitValue).ToString();
            UpdateDescription();
        }
        
        private void UpdateModel()
        {
            CurrencyModel.SourceType = SourceType;
            CurrencyModel.TargetType = TargetType;
            CurrencyModel.UnitValue = UnitValue;
        }

        private void UpdateDescription()
        {
            if (!CurrencyInfo.ContainsKey(SourceType))
            {
                MessageBox.Show("Source dont exsit");
                return;
            }
            if (!CurrencyInfo.ContainsKey(TargetType))
            {
                MessageBox.Show("Target dont exsit");
                return;
            }
            Description = SourceAmount + " " + CurrencyInfo[SourceType].Substring(5)
                + " is equal to " + TargetAmount + " " + CurrencyInfo[TargetType].Substring(5);
        }
        private void GetData()
        {
            if (SourceType == null || !CurrencyInfo.ContainsKey(SourceType))
            {
                MessageBox.Show("Source dont exist", "Missing data");
                return;
            }
            if (TargetType == null || !CurrencyInfo.ContainsKey(TargetType))
            {
                MessageBox.Show("Target dont exsit", "Missing data");
                return;
            }

            try
            {
                UpdateModel();
                CurrencyModel.GetData();
                UnitValue = CurrencyModel.UnitValue;
                double source_double;
                if (!double.TryParse(SourceAmount, out source_double))
                    source_double = 1;
                SourceAmount = source_double.ToString();
                SourceUpdated = true;
                TargetAmount = (source_double * UnitValue).ToString();
                UpdateDescription();
            }
            catch (Exception e) { MessageBox.Show(e.Message, e.GetType().Name); }
        }

        #endregion

        #region Properties
        private ConvertModel CurrencyModel { get; set; }
        private double UnitValue { get; set; }
        private bool SourceUpdated;
        private bool TargetUpdated;
        private string _SourceAmount;
        private string _SourceType;
        private string _TargetType;
        private string _TargetAmount;
        private string _Description;

        public Dictionary<string, string> CurrencyInfo { get; set; }
        public CurrencyCommand ConvertCommand { get; set; }
        public string Description
        {
            get { return _Description; }

            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }
        public string SourceType
        {
            get { return _SourceType; }
            set
            {
                if (value != _SourceType)
                {
                    _SourceType = value;
                    OnPropertyChanged("SourceType");
                }
            }
        }
        public string TargetType
        {
            get { return _TargetType; }
            set
            {
                if (value != _TargetType)
                {
                    _TargetType = value;
                    OnPropertyChanged("TargetType");
                }
            }
        }
        public string SourceAmount
        {
            get { return _SourceAmount; }
            set
            {
                if (!SourceUpdated && value != _SourceAmount)
                {
                    if (value != null && value != "" && value[value.Length - 1] == '.')
                        _SourceAmount = value;
                    else if (double.TryParse(value, out double double_val))
                    {
                        SourceUpdated = true;
                        _SourceAmount = Math.Round(double_val, 4).ToString();
                        TargetAmount = (UnitValue * double_val).ToString();
                    }
                    else
                        _SourceAmount = value;
                    OnPropertyChanged("SourceAmount");
                }
                SourceUpdated = TargetUpdated = false;
            }
        }
        public string TargetAmount
        {
            get { return _TargetAmount; }
            set
            {
                if (!TargetUpdated && value != _TargetAmount)
                {
                    if (value != null && value != "" && value[value.Length - 1] == '.')
                        _TargetAmount = value;
                    else if (double.TryParse(value, out double double_val))
                    {
                        TargetUpdated = true;
                        _TargetAmount = Math.Round(double_val, 4).ToString();
                        if (UnitValue != 0)
                            SourceAmount = (double_val / UnitValue).ToString();
                        else
                            SourceAmount = "0";
                    }
                    else
                        _TargetAmount = value;
                    OnPropertyChanged("TargetAmount");
                }
                SourceUpdated = TargetUpdated = false;
            }
        }
        #endregion
    }
}
