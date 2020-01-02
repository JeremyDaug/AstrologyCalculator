using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AstrologyCalculator.TimespanUnits.TimespansTab
{
    class TimespansTabViewModel : INotifyPropertyChanged
    {
        public TimespanUnitManager timespanUnitManager;

        public ObservableCollection<string> UnitNames { get; set; }

        private string currentUnitName;
        public string CurrentUnitName
        {
            get
            {
                return currentUnitName;
            }
            set
            {
                if(currentUnitName != value)
                {
                    currentUnitName = value;
                    OnPropertyChanged(nameof(CurrentUnitName));
                }
            }
        }

        private double currentUnitLength;
        public double CurrentUnitLength
        {
            get
            {
                return currentUnitLength;
            }
            set
            {
                if(currentUnitLength != value)
                {
                    currentUnitLength = value;
                    OnPropertyChanged(nameof(CurrentUnitLength));
                }
            }
        }

        private int currentUnitRank;
        public int CurrentUnitRank
        {
            get
            {
                return currentUnitRank;
            }
            set
            {
                if(currentUnitRank != value)
                {
                    currentUnitRank = value;
                    OnPropertyChanged(nameof(CurrentUnitRank));
                }
            }
        }

        #region Reactions

        public void SelectedUnitChanged(object sender, SelectionChangedEventArgs args)
        {
            var name = (string)args.AddedItems[0];

            CurrentUnitName = name;
            CurrentUnitLength = timespanUnitManager.GetLength(name);
            CurrentUnitRank = timespanUnitManager.GetRank(name);
        }

        public void UnitNameChanged(object sender, TextChangedEventArgs args)
        {

        }

        public void UnitLengthChanged(object sender, TextChangedEventArgs args)
        {

        }

        public void UnitRankChanged(object sender, TextChangedEventArgs args)
        {

        }



        #endregion Reactions

        public TimespansTabViewModel()
        {
            UnitNames = new ObservableCollection<string>();

            timespanUnitManager = new TimespanUnitManager();
            timespanUnitManager.SetDefaultValues();

            foreach(var unit in timespanUnitManager.AvailableUnits())
            {
                UnitNames.Add(unit);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
