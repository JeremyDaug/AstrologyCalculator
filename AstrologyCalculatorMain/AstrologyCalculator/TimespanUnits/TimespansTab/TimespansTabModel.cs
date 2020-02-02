.using System.Collections.Generic;
using System.Linq;

namespace AstrologyCalculator.TimespanUnits.TimespansTab
{
    public class TimespansTabModel
    {
        public TimespansTabModel(TimespanUnitManager manager)
        {
            this.TimespansUnitManager = manager;
            if (CurrentAvailableUnits.Count() == 0)
                TimespansUnitManager.LoadDefault();
        }

        public TimespanUnitManager TimespansUnitManager;

        public string CurrentUnitName { get; set; }

        public double CurrentUnitLength { get; set; }

        public int CurrentUnitRank { get; set; }

        public IList<string> CurrentAvailableUnits
        {
            get
            {
                var result =  TimespansUnitManager.AvailableUnits();
                result.Sort();
                return result;
            }
        }

        public IList<string> AlternativeAvailableUnits
        {
            get
            {
                var result = CurrentAvailableUnits.Where(x => x != CurrentUnitName).ToList();
                result.Sort();
                return result;
            }
        }
                

        public double AlternativeUnitLength { get; internal set; }

        internal void ChangeCurrentUnit(string name)
        {
            if (!TimespansUnitManager.ContainsUnit(name))
                throw new KeyNotFoundException();
            CurrentUnitName = name;

            CurrentUnitLength = TimespansUnitManager.GetLength(name);
            CurrentUnitRank = TimespansUnitManager.GetRank(name);
        }

        public void SaveNewUnit()
        {
            TimespansUnitManager.AddOrOverride(CurrentUnitName, CurrentUnitLength, CurrentUnitRank);
        }

        public void DeleteUnit(string currentUnitName)
        {
            TimespansUnitManager.DeletUnit(currentUnitName);
        }

        public void SaveToFile(string file)
        {
            TimespansUnitManager.SaveTo(file);
        }

        internal void LoadFromFile(string file)
        {
            TimespansUnitManager.Clear();
            TimespansUnitManager.LoadFrom(file);
        }

        internal void LoadDefaults()
        {
            TimespansUnitManager.LoadDefault();
        }
    }
}
