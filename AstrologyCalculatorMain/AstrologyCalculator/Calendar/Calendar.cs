using AstrologyCalculator.TimespanUnits;

namespace AstrologyCalculator.Calendar
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
                return dayLength;
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
