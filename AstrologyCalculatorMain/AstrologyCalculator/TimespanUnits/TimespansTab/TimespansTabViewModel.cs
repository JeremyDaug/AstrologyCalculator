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
    public class TimespansTabViewModel : INotifyPropertyChanged
    {
        private TimespansTabModel model;

        public IList<string> UnitNames
        {
            get
            {
                return model.CurrentAvailableUnits;
            }
        }

        public string CurrentUnitName
        {
            get
            {
                return model.CurrentUnitName;
            }
            set
            {
                if(model.CurrentUnitName != value)
                {
                    model.CurrentUnitName = value;
                    OnPropertyChanged(nameof(CurrentUnitName));
                }
            }
        }

        public double CurrentUnitLength
        {
            get
            {
                return model.CurrentUnitLength;
            }
            set
            {
                if(model.CurrentUnitLength != value)
                {
                    model.CurrentUnitLength = value;
                    OnPropertyChanged(nameof(CurrentUnitLength));
                }
            }
        }

        public int CurrentUnitRank
        {
            get
            {
                return model.CurrentUnitRank;
            }
            set
            {
                if(model.CurrentUnitRank != value)
                {
                    model.CurrentUnitRank = value;
                    OnPropertyChanged(nameof(CurrentUnitRank));
                }
            }
        }

        #region Reactions

        public void SelectedUnitChanged(object sender, SelectionChangedEventArgs args)
        {
            var name = (string)args.AddedItems[0];

            model.ChangeCurrentUnit(name);
        }

        #endregion Reactions

        public TimespansTabViewModel(TimespansTabModel model)
        {
            this.model = model;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
