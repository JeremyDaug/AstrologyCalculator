using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.Body
{
    /// <summary>
    /// An orbit that is based on a body of negligible mass that is moving through a
    /// powered orbit. An orbit that is being pushed along at a higher speed.
    /// </summary>
    public class PoweredOrbit : NaturalOrbital
    {
        /// <summary>
        /// Mass of the Parent body we are orbiting around.
        /// </summary>
        public override double BodyMass => 0;

        
    }
}
