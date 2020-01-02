using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarLibrary
{
    public class Calendar
    {
        private readonly TimespanUnitManager timeUnits;

        public string CalendarName { get; set; }

        // day length
        private double dayLength;
        public double DayLength
        {
            get
            {
                if (timeUnits.UnitLength.Keys.Contains(DayUnitName))
                {
                    return timeUnits.Convert(timeUnits.BaseUnit, DayUnitName, dayLength);
                }
                else return dayLength;
                
            }
        }
        public string DayUnitName { get; set; }

        // month length
        // year length

        public Calendar()
        {
            timeUnits = new TimespanUnitManager();
            timeUnits.LoadDefault();
        }
    }
}
