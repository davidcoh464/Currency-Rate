using CurrencyPL.Command;
using CurrencyPL.Model;
using CurrencyPL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;

namespace CurrencyPL
{
    public class LiveCurrencyViewModel : BaseViewModel
    {

        #region Constructor
        public LiveCurrencyViewModel()
        {
            CurrencyModel = new LiveCurrencyModel();
            CurrencyCommand = new CurrencyCommand();
            CurrencyCommand.WithoutParameterEvent += GetData;
            _Timer = new Timer(15 * 1000); // update every 15 second
            _Timer.Elapsed += Timer_Elapsed;
            CurrencyInfo = CurrencyModel.GetCurrencyInfo();
            foreach (var key in CurrencyInfo.Keys.ToList())
            {
                CurrencyInfo[key] = key + ": " + CurrencyInfo[key];
            }
        }
        #endregion

        #region Functions
        private void GetData()
        {
            if (Source == null || !CurrencyInfo.ContainsKey(Source))
            {
                MessageBox.Show("Source dont exist", "Missing data");
                _Timer.Stop();
                return;
            }
            if (Target == null || !CurrencyInfo.ContainsKey(Target))
            {
                MessageBox.Show("Target dont exsit", "Missing data");
                _Timer.Stop();
                return;
            }
            if (Source == Target)
            {
                _Timer.Stop();
                CurrencyModel.Source = Source;
                CurrencyModel.Target = Target;
                CurrencyModel.LiveDataValue = LiveDataValue = 1;
                _Timer.Start();
                return;
            }
            try
            {
                _Timer.Stop();
                CurrencyModel.Source = Source;
                CurrencyModel.Target = Target;
                CurrencyModel.GetLiveCurrency();
                LiveDataValue = CurrencyModel.LiveDataValue;
                _Timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _Timer.Stop();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GetData();
        }
        
        #endregion

        #region Properties
        public Dictionary<string, string> CurrencyInfo { get; set; }
        public CurrencyCommand CurrencyCommand { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        private LiveCurrencyModel CurrencyModel { get; set; }
        public Timer _Timer { get; set; }

        private double _LiveDataValue;
        public double LiveDataValue
        {
            get { return _LiveDataValue; }
            set
            {
                _LiveDataValue = value;
                OnPropertyChanged("LiveDataValue");
            }
        }
        #endregion
    }
}
