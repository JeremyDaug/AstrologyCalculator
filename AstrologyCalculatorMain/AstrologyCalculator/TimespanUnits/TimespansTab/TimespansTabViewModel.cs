using Microsoft.Win32;
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

        private string selectedUnit;
        public string SelectedUnitName
        {
            get { return selectedUnit; }
            set
            {
                if (selectedUnit != value)
                {
                    selectedUnit = value;
                    OnPropertyChanged(nameof(SelectedUnitName));
                }
            }
        }

        private ObservableCollection<string> unitNames;
        public ObservableCollection<string> UnitNames
        {
            get
            {
                return unitNames;
            }
            private set
            {
                if(value != null)
                {
                    unitNames = value;
                    OnPropertyChanged(nameof(UnitNames));
                }
            }
        }

        private string selectedAltUnit;
        public string SelectedAlternativeUnitName
        {
            get { return selectedAltUnit; }
            set
            {
                if (selectedAltUnit != value)
                {
                    selectedAltUnit = value;
                    OnPropertyChanged(nameof(SelectedAlternativeUnitName));
                }
            }
        }

        private ObservableCollection<string> alternativeUnitNames;
        public ObservableCollection<string> AlternativeUnitNames
        {
            get
            {
                return alternativeUnitNames;
            }
            set
            {
                alternativeUnitNames = value;
                OnPropertyChanged(nameof(AlternativeUnitNames));
            }
        }

        public double AlternativeUnitLength
        {
            get
            {
                return model.AlternativeUnitLength;
            }
            set
            {
                if(model.AlternativeUnitLength != value)
                {
                    model.AlternativeUnitLength = value;
                    OnPropertyChanged(nameof(AlternativeUnitLength));
                }
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

        #region Commands

        public ICommand CreateUnitCommand { get; set; }

        public ICommand SaveUnitCommand { get; set; }

        public ICommand DeleteUnitCommand { get; set; }

        public ICommand SaveToFileCommand { get; set; }

        public ICommand LoadFromFileCommand { get; set; }

        public ICommand LoadDefaultUnitsCommand { get; set; }

        #endregion Commands

        #region Reactions

        public void SelectedUnitChanged(object sender, SelectionChangedEventArgs args)
        {

        }

        public void AlternativeUnitChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                AlternativeUnitLength = 0;
            }
            else
            {
                var name = (string)e.AddedItems[0];
                SelectedAlternativeUnitName = name;
                AlternativeUnitLength = model.TimespansUnitManager.InUnitsOf(SelectedAlternativeUnitName, CurrentUnitLength);
            }
        }

        #endregion Reactions

        public void NewTimespanUnit()
        {
            CurrentUnitName = "";
            CurrentUnitLength = 1;
            CurrentUnitRank = 1;
        }

        public void SaveNewTimespan()
        {
            model.SaveNewUnit();

            if (CurrentUnitName != SelectedUnitName)
            {
                model.DeleteUnit(selectedUnit);
                SelectedUnitName = CurrentUnitName;
                UpdateUnitLists();
            }
            else
            {
                UpdateUnitLists();
            }
        }

        public void DeleteUnit()
        {
            model.DeleteUnit(CurrentUnitName);
            SelectedUnitName = model.CurrentAvailableUnits[0];
            UpdateUnitLists();
        }

        private void UpdateUnitLists()
        {
            UnitNames = new ObservableCollection<string>(model.CurrentAvailableUnits);
            AlternativeUnitNames = new ObservableCollection<string>(model.AlternativeAvailableUnits);
        }

        private bool CanSave()
        {
            if (string.IsNullOrWhiteSpace(CurrentUnitName))
                return false;

            if (CurrentUnitLength <= 0)
                return false;

            if (CurrentUnitRank < 0)
                return false;

            return true;
        }

        private bool CanDelete()
        {
            if (string.IsNullOrWhiteSpace(CurrentUnitName))
                return false;
            if (!model.CurrentAvailableUnits.Contains(CurrentUnitName))
                return false;

            return true;
        }

        private void SaveToFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Create or select file to save to.";
            dialog.Filter = "Xml Files (*.xml)|*.xml";

            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                var file = dialog.FileName;
                model.SaveToFile(file);
            }

        }

        private void LoadFromFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select Existing File";
            dialog.Filter = "Xml Files (*.xml)|*.xml";

            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                var file = dialog.FileName;
                model.LoadFromFile(file);
                SelectedAlternativeUnitName = model.AlternativeAvailableUnits[0];
                SelectedUnitName = model.CurrentAvailableUnits[0];
                UpdateUnitLists();
            }
        }

        private void LoadDefaults()
        {
            model.LoadDefaults();
            UpdateUnitLists();
        }

        public TimespansTabViewModel(TimespansTabModel model)
        {
            this.model = model;
            unitNames = new ObservableCollection<string>(model.CurrentAvailableUnits);
            CreateUnitCommand = new RelayCommand(() => NewTimespanUnit(), () => true);
            SaveUnitCommand = new RelayCommand(() => SaveNewTimespan(), () => CanSave());
            DeleteUnitCommand = new RelayCommand(() => DeleteUnit(), () => CanDelete());
            SaveToFileCommand = new RelayCommand(() => SaveToFile(), () => true);
            LoadFromFileCommand = new RelayCommand(() => LoadFromFile(), () => true);
            LoadDefaultUnitsCommand = new RelayCommand(() => LoadDefaults(), () => true);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if(string.Equals(propertyName, nameof(AlternativeUnitLength)))
            {
                // Alternative length has changed, update the current length to match
                if (selectedAltUnit != null)
                    CurrentUnitLength = AlternativeUnitLength * model.TimespansUnitManager.GetLength(SelectedAlternativeUnitName);
            }
            else if (string.Equals(propertyName, nameof(CurrentUnitLength)))
            {
                if (string.Equals(CurrentUnitName, selectedAltUnit))
                {
                    SelectedAlternativeUnitName = null;
                }
                // Current Unit Length has changed, update alternative.
                if (selectedAltUnit != null)
                {
                    AlternativeUnitLength = model.TimespansUnitManager.InUnitsOf(SelectedAlternativeUnitName, CurrentUnitLength);
                }
            }
            else if (string.Equals(propertyName, nameof(SelectedUnitName)))
            {
                // Selected Unit Changed
                if (SelectedUnitName != null)
                {

                    CurrentUnitName = SelectedUnitName;
                    CurrentUnitLength = model.TimespansUnitManager.GetLength(SelectedUnitName);
                    CurrentUnitRank = model.TimespansUnitManager.GetRank(SelectedUnitName);

                    AlternativeUnitNames = new ObservableCollection<string>(model.AlternativeAvailableUnits);

                    if(model.AlternativeAvailableUnits.Count() > 0)
                        SelectedAlternativeUnitName = model.AlternativeAvailableUnits[0];
                }

                CurrentUnitLength = model.TimespansUnitManager.GetLength(SelectedUnitName);
                CurrentUnitRank = model.TimespansUnitManager.GetRank(SelectedUnitName);
            }
            else if (string.Equals(propertyName, nameof(SelectedAlternativeUnitName)))
            {
                // Alt unit changed
                if (SelectedAlternativeUnitName != null)
                    AlternativeUnitLength = model.TimespansUnitManager.InUnitsOf(SelectedAlternativeUnitName, CurrentUnitLength);
            }
            else if (string.Equals(propertyName, nameof(CurrentUnitName)))
            {
                // Current Unit Name Changed
            }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
