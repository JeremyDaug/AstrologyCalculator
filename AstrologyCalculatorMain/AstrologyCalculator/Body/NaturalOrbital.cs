using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AstrologyCalculator.Body
{
    /// <summary>
    /// An orbital that is bound by gravity and should be considered 'natural'.
    /// </summary>
    public class NaturalOrbital : Orbital
    {
        /// <summary>
        /// The orbital period of the body in seconds.
        /// Adusts the SemiMajorAxis of the orbit to remain consistent.
        /// </summary>
        public override double OrbitalPeriod
        {
            get
            {
                return 2 * Math.PI * Math.Sqrt(Math.Pow(SemiMajorAxis, 3) / GParam);
            }
            set
            {
                SemiMajorAxis = Math.Pow(Math.Pow(value / (2 * Math.PI), 2) * GParam, 1 / 3);
            }
        }
    }
}
