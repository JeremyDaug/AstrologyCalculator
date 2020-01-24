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
        #region AdditionalProperties

        /// <summary>
        /// Mass of the parent body being orbited in kg.
        /// </summary>
        public double ParentMass { get; set; }

        /// <summary>
        /// The mass of the body that is orbiting in kg.
        /// </summary>
        public virtual double BodyMass { get; set; }

        /// <summary>
        /// The Gravity Constant in units of m^3 / (kg s^2)
        /// </summary>
        public const double G = 6.67408E-11;

        /// <summary>
        /// The Gravitational parameter of the body and it's parent.
        /// </summary>
        public double GParam => G * (ParentMass + BodyMass);

        /// <summary>
        /// The Velocity of the body at the current point.
        /// </summary>
        public double CurrentVelocity
        {
            get
            {
                return VelocityAtAnAngle(CurrentAngle);
            }
        }

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

        /// <summary>
        /// Where the Barycenter (center of mass) of the system is.
        /// Measured in distance from the center of the parent body in meters.
        /// If the Barycenter is further from the center than the body's radius, 
        /// then the orbit would be considered binary.
        /// </summary>
        public double Barycenter
        {
            get
            {
                return SemiMajorAxis / (1 + (ParentMass / BodyMass));
            }
        }

        public Vector3D CurrentVelocityVector
        {
            get
            {
                var m = CurrentAngle;
                var r = CurrentRadius;
                var w = ArgumentOfPeriapsis;
                var om = AscendingNode;
                var i = Inclination;

                var p = Math.Sqrt(GParam * SemiMajorAxis) / CurrentRadius *
                    new Vector3D(-Math.Sin(EccentricAnomaly),
                                 Math.Sqrt(1-Eccentricity*Eccentricity) * Math.Cos(EccentricAnomaly), 
                                 0);

                var result = Rotate_Z(-om) * Rotate_x(-Inclination) * Rotate_Z(-w);
                return Vector3D.Multiply(p, result);
            }
        }

        #endregion AdditionalProperties

        #region HelperFunctions

        /// <summary>
        /// The Velocity of the Body at an angle in the orbit.
        /// </summary>
        /// <param name="radAngle">The position in orbit in Radians.</param>
        /// <returns>The Velocity of the body along the orbital path in m/s^2</returns>
        public double VelocityAtAnAngle(double radAngle)
        {
            return Math.Sqrt(GParam * (2 / RadiusAtAnAngle(radAngle) - 1 / SemiMajorAxis));
        }

        #endregion HelperFunctions
    }
}
