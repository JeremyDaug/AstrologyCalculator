using AstrologyCalculator.BodyEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.MainDataframe
{
    public class MainDataframe
    {
        private static MainDataframe instance;
        public static MainDataframe Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainDataframe();
                return instance;
            }
        }

        public TimespanUnits.TimespanUnitManager TimeUnitManager { get; }

        /// <summary>
        /// TODO Rework this once a collection of bodies is needed.
        /// </summary>
        //public Body CurrentBody { get; }

        private MainDataframe()
        {
            TimeUnitManager = new TimespanUnits.TimespanUnitManager();
            //CurrentBody = new Body();
        }
    }
}
