
using CurrencyBE;
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
    public class HistoryViewModel : BaseViewModel
    {
        #region Constructor
        public HistoryViewModel()
        {
            HistoryModel = new HistoryModel();
            StartDate = DateTime.Now.AddYears(-1);
            EndDate = DateTime.Now;
            CurrencyType = "USD: United States Dollar";
            DisplayMethod = "Monthly";
           
            ShowChartCommand = new CurrencyCommand();
            ShowChartCommand.WithoutParameterEvent += GetData;

            SelectDisplayMethodCommand = new CurrencyCommand();
            SelectDisplayMethodCommand.OneParemterEvent += SelectDisplayDataMethod;
            
            DisplayLinePlot = DisplayDataGrid = false;
            DisplayHistogram = true;
        }
        #endregion

        #region Functions
        private void SelectDisplayDataMethod(string obj)
        {
            DisplayLinePlot = DisplayHistogram = DisplayDataGrid = false;
            if (obj == "Line")
                DisplayLinePlot = true;
            else if (obj == "Histogram")
                DisplayHistogram = true;
            else
                DisplayDataGrid = true;
        }

        private void GetData()
        {
            _CurrencyType = _CurrencyType.Split(':')[0];
            try
            {
                HistoryModel.StartDate = StartDate;
                HistoryModel.EndDate = EndDate;
                HistoryModel.Source = CurrencyType;
                //cal the model to update its list from dataBase
                HistoryModel.GetData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), e.GetType().Name);
                return;
            }
            //Groups the currency list according to the chosen display method
            int i = 4; // yyyy
            if (DisplayMethod == "Monthly")
                i = 7; // yyyy-MM
            else if (DisplayMethod == "Daily") // returns the list without grouping
            {
                Histories = new ObservableCollection<CurrencyHistory>(HistoryModel.CurrencyHistories);
                return;
            }
            try
            {
                List<CurrencyHistory> resulte = new List<CurrencyHistory>();
                foreach (var group in HistoryModel.CurrencyHistories.GroupBy(p => p.Date.Substring(0, i)))
                {
                    resulte.Add(new CurrencyHistory
                    {
                        Date = group.Key,
                        USDValue = group.Average(p => p.USDValue),
                        EURValue = group.Average(p => p.EURValue),
                        GBPValue = group.Average(p => p.GBPValue),
                        AUDValue = group.Average(p => p.AUDValue),
                        ILSValue = group.Average(p => p.ILSValue)
                    });
                }
                Histories = new ObservableCollection<CurrencyHistory>(resulte);
            }
            catch (Exception) { }
        }
        #endregion

        #region Properties
        private ObservableCollection<CurrencyHistory> _History;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _CurrencyType;
        private bool _DisplayHistogram;
        private bool _DisplayLinePlot;
        private bool _DisplayDataGrid;
        public ObservableCollection<CurrencyHistory> Histories
        {
            get { return _History; }
            set
            {
                _History = value;
                OnPropertyChanged("Histories");
            }
        }
        public HistoryModel HistoryModel { get; set; }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                // if the value is smaller than avilibe api start date
                if ((value - new DateTime(2004, 1, 1)).TotalDays < 0)
                    _StartDate = new DateTime(2004, 1, 1);
                else
                    _StartDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                //if value is greater than current date
                if ((value - DateTime.Now).TotalDays > 0)
                    _EndDate = DateTime.Now;
                else
                    _EndDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        public string CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (value != null)
                {
                    _CurrencyType = value;
                    OnPropertyChanged("CurrencyType");
                   
                }
            }
        }
        public string DisplayMethod { get; set; }
        public bool DisplayLinePlot
        {
            get { return _DisplayLinePlot; }
            set
            {
                _DisplayLinePlot = value;
                OnPropertyChanged("DisplayLinePlot");
            }
        }
        public bool DisplayHistogram
        {
            get { return _DisplayHistogram; }
            set
            {
                _DisplayHistogram = value;
                OnPropertyChanged("DisplayHistogram");
            }
        }
        public bool DisplayDataGrid
        {
            get { return _DisplayDataGrid; }
            set
            {
                _DisplayDataGrid = value;
                OnPropertyChanged("DisplayDataGrid");
            }
        }
        public CurrencyCommand ShowChartCommand { get; set; }
        public CurrencyCommand SelectDisplayMethodCommand { get; set; }
        #endregion
    }
}
