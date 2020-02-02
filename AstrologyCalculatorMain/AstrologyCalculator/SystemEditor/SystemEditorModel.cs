using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstrologyCalculator.Body;

namespace AstrologyCalculator.SystemEditor
{
    public class SystemEditorModel
    {
        private readonly MainDataframe.MainDataframe main;

        public IList<string> AllPlanets { get; set; }
        public IList<string> PlanetChildren { get; set; }

        public Body.Body Body { get; set; }

        public string CurrentBody { get; set; }
    }
}
