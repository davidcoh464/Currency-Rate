using CurrencyPL.Command;
using CurrencyPL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyPL
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor
        public MainViewModel()
        {
            MainVMCommand = new CurrencyCommand();
            MainVMCommand.OneParemterEvent += UpdateNavigationView;
            PressedHistory = PressedRate = PressedRateGraph = PressedLive = false;
            PressedConvert = true;
            Navigation = new ConvertViewModel();
        }
        #endregion

        #region properties
        private BaseViewModel _Navigation;
        public BaseViewModel Navigation
        {
            get => _Navigation;
            set
            {
                _Navigation = value;
                OnPropertyChanged("Navigation");
            }
        }
        public CurrencyCommand MainVMCommand { get; set; }

        private bool _PressedHistory;
        public bool PressedHistory
        {
            get { return _PressedHistory; }
            set
            {
                if (value != _PressedHistory)
                {
                    _PressedHistory = value;
                    OnPropertyChanged("PressedHistory");
                }
            }
        }

        private bool _PressedRate;
        public bool PressedRate
        {
            get { return _PressedRate; }
            set
            {
                if (value != _PressedRate)
                {
                    _PressedRate = value;
                    OnPropertyChanged("PressedRate");
                }
            }
        }

        private bool _PressedRateGraph;
        public bool PressedRateGraph
        {
            get { return _PressedRateGraph; }
            set
            {
                if (value != _PressedRateGraph)
                {
                    _PressedRateGraph = value;
                    OnPropertyChanged("PressedRateGraph");
                }
            }
        }

        private bool _PressedLive;
        public bool PressedLive
        {
            get { return _PressedLive; }
            set
            {
                if (value != _PressedLive)
                {
                    _PressedLive = value;
                    OnPropertyChanged("PressedLive");
                }
            }
        }

        private bool _PressedConvert;
        public bool PressedConvert
        {
            get { return _PressedConvert; }
            set
            {
                if (value != _PressedConvert)
                {
                    _PressedConvert = value;
                    OnPropertyChanged("PressedConvert");
                }
            }
        }

        #endregion

        #region Function

        private void UpdateNavigationView(string obj)
        {
            PressedHistory = PressedRate = PressedRateGraph = PressedLive = PressedConvert = false;
            if (Navigation != null && Navigation.GetType().Name == "LiveCurrencyViewModel")
            {
                (Navigation as LiveCurrencyViewModel)._Timer.Stop();
            }
            if (obj == "Latest Rate")
            {
                PressedRate = true;
                Navigation = new LatestRateViewModel();
            }
            else if (obj == "History")
            {
                PressedHistory = true;
                Navigation = new HistoryViewModel();
            }
            else if (obj == "Live")
            {
                PressedLive = true;
                Navigation = new LiveCurrencyViewModel();
            }
            else if (obj == "Latest Rate Graph")
            {
                PressedRateGraph = true;
                Navigation = new LatestRateGraphViewModel();
            }
            else if (obj == "Convert")
            {
                PressedConvert = true;
                Navigation = new ConvertViewModel();
            }
        }

        #endregion
    }
}
