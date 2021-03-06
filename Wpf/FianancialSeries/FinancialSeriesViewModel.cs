﻿using System.ComponentModel;
using System.Linq;
using LiveCharts.Geared;

namespace Geared.Wpf.FianancialSeries
{
    public class FinancialSeriesViewModel : INotifyPropertyChanged
    {
        private string[] _labels;
        private DataProviderPoint[] _data;
        private double _minAxisLimit;
        private double _maxAxisLimit;

        public FinancialSeriesViewModel()
        {
            _data = DataProvider.Get.ToArray();

            Last45Command = new RelayCommand(SetLast45);
            Last90Command = new RelayCommand(SetLast90);
            Last180Command = new RelayCommand(SetLast180);
            Last365Command = new RelayCommand(SetLast365);
            Last5YearsCommand = new RelayCommand(SetLast5Years);

            Values = _data.AsGearedValues();
            Labels = _data.Select(x => x.DateTime.ToString("dd MMM yy")).ToArray();

            SetLast45();
        }

        public RelayCommand Last45Command { get; set; }
        public RelayCommand Last90Command { get; set; }
        public RelayCommand Last180Command { get; set; }
        public RelayCommand Last365Command { get; set; }
        public RelayCommand Last5YearsCommand { get; set; }
        
        public GearedValues<DataProviderPoint> Values { get; set; }

        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }

        public double MinAxisLimit
        {
            get { return _minAxisLimit; }
            set
            {
                _minAxisLimit = value;
                OnPropertyChanged("MinAxisLimit");
            }
        }

        public double MaxAxisLimit
        {
            get { return _maxAxisLimit; }
            set
            {
                _maxAxisLimit = value;
                OnPropertyChanged("MaxAxisLimit");
            }
        }

        private void SetLast45()
        {
            MinAxisLimit = _data.Length - 45;
            MaxAxisLimit = _data.Length;
        }

        private void SetLast90()
        {
            MinAxisLimit = _data.Length - 90;
            MaxAxisLimit = _data.Length;
        }

        private void SetLast180()
        {
            MinAxisLimit = _data.Length - 180;
            MaxAxisLimit = _data.Length;
        }

        private void SetLast365()
        {
            MinAxisLimit = _data.Length - 365;
            MaxAxisLimit = _data.Length;
        }

        private void SetLast5Years()
        {
            //Auto scale min value according to data in chart
            MinAxisLimit = _data.Length-365*5;
            MaxAxisLimit = _data.Length;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
