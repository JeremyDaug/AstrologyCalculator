using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.TimespanUnits.TimespansTab
{
    public class TimespansTabModel
    {
        public TimespansTabModel(TimespanUnitManager manager)
        {
            this.timespanUnitManager = manager;
            if (CurrentAvailableUnits.Count() == 0)
                timespanUnitManager.LoadDefault();
        }

        private TimespanUnitManager timespanUnitManager;

        public string CurrentUnitName { get; set; }

        public double CurrentUnitLength { get; set; }

        public int CurrentUnitRank { get; set; }

        public IList<string> CurrentAvailableUnits => timespanUnitManager.AvailableUnits();

        internal void ChangeCurrentUnit(string name)
        {
            if (!timespanUnitManager.ContainsUnit(name))
                throw new KeyNotFoundException();
            CurrentUnitName = name;

            CurrentUnitLength = timespanUnitManager.GetLength(name);
            CurrentUnitRank = timespanUnitManager.GetRank(name);
        }

        public void CreateNewUnit(string unit)
        {
            timespanUnitManager.AddOrOverride(unit, 1, 1);

            CurrentUnitName = unit;
            CurrentUnitLength = 1;
            CurrentUnitRank = 1;
        }
    }
}
