using System;
using System.Collections.Generic;

namespace AstrologyCalculator.BodyEditor
{
    public class Body
    {
        // See https://en.wikipedia.org/wiki/Orbital_mechanics for all orbital needs

        /// <summary>
        /// The Gravitational Constant, used for calculations
        /// In units of m^3 / (kg s^2)
        /// </summary>
        public const double GravityConst = 6.67408E-11;

        #region Properties

        /// <summary>
        /// Name of the Body
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mass of the body in kg
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// The (average) radius of the body at it's surface.
        /// </summary>
        public double BodyRadius { get; set; }

        /// <summary>
        /// How long the day is from noon to noon.
        /// </summary>
        public double DayLength { get; set; }

        /// <summary>
        /// Parent body of the body, may be null if this body
        /// is the highest ranking body.
        /// </summary>
        public Body Parent { get; set; }

        /// <summary>
        /// An Accessor for the parent's mass.
        /// </summary>
        public double ParentMass
        {
            get
            {
                if(Parent != null)
                    return Parent.Mass;
                return 0;
            }
        }

        /// <summary>
        /// The Children of the body, may be null if it is childless.
        /// </summary>
        public IList<Body> Children { get; set; }

        /// <summary>
        /// A programatically defined name that is based on it's
        /// position in the system as a whole.
        /// </summary>
        public string PositionalName { get; set; }

        /// <summary>
        /// Not actually used for anything, but worth noting.
        /// </summary>
        public double SurfaceEscapeVelocity
        {
            get
            {
                return CalculateEscapeVelocity(BodyRadius);
            }
        }

        #endregion Properties

        #region OrbitalProps

        /// <summary>
        /// The Gravitational Parameter of the body and it's parent.
        /// </summary>
        public double GravitationalParameter => GravityConst * (Mass + ParentMass);

        /// <summary>
        /// The Current Radius of 
        /// </summary>
        public double CurrentRadius
        {
            get
            {
                return RadiusAtAnAngle(CurrentAngle);
            }
        }

        /// <summary>
        /// The Velocity of the body at our current point.
        /// </summary>
        public double CurrentVelocity
        {
            get
            {
                return VelocityAtAnAngle(CurrentAngle);
            }
        }

        /// <summary>
        /// The total time to complete a cycle.
        /// </summary>
        public double OrbitalPeriod
        {
            get
            {
                return 2 * Math.PI * Math.Sqrt(Math.Pow(SemiMajorAxis, 3) / GravitationalParameter);
            }
            set
            {
                SemiMajorAxis = Math.Pow(Math.Pow(value / (2 * Math.PI), 2) * GravitationalParameter, 1 / 3);
            }
        }

        /// <summary>
        /// The Closest distance to the main body in the orbit.
        /// </summary>
        public double Periapsis
        {
            get
            {
                return 2 * SemiMajorAxis - Apoapsis;
            }
            set
            {
                SemiMajorAxis = (value + Apoapsis) / 2;
                Eccentricity = (Apoapsis - Periapsis) / (Apoapsis + Periapsis);
            }
        }

        /// <summary>
        /// The furthest distance in the orbit.
        /// </summary>
        public double Apoapsis
        {
            get
            {
                return 2 * SemiMajorAxis - Periapsis;
            }
            set
            {
                SemiMajorAxis = (Periapsis + value) / 2;
                Eccentricity = (Apoapsis - Periapsis) / (Apoapsis + Periapsis);
            }
        }

        /// <summary>
        /// Get's the Minor Axis of the Elipse
        /// </summary>
        public double SemiMinorAxis
        {
            get
            {
                var eSqared = Math.Pow(Eccentricity, 2);
                var majorAxis = Math.Pow(SemiMajorAxis, 2);
                return Math.Pow( majorAxis * (1 - eSqared) , 0.5);
            }
        }

        /// <summary>
        /// The area of the orbit, for calculating movement.
        /// </summary>
        public double Area
        {
            get
            {
                return Math.PI * SemiMajorAxis * SemiMinorAxis;
            }
        }

        #endregion OrbitalProps

        public double MeanMotionMovement(double angle)
        {
            return MeanMotion() * angle;
        }

        public double MeanMotion()
        {
            return 2 * Math.PI / OrbitalPeriod;
        }

        public double VelocityAtAnAngle(double radAngle)
        {
            return Math.Sqrt(GravitationalParameter * (2 / RadiusAtAnAngle(radAngle) - 1 / SemiMajorAxis));
        }

        public double RadiusAtAnAngle(double radAngle)
        {
            return SemiMajorAxis * (1 - Math.Pow(Eccentricity, 2))
                / (1 + Eccentricity * Math.Cos(radAngle));
        }

        public double CalculateEscapeVelocity(double radius)
        {
            return Math.Sqrt(2 * GravityConst * Mass / radius);
        }

        public double CalculateOrbitalPeriod(double distance)
        {
            return 0;
        }

        #region KeplerianElements

        // Holdout region See: https://en.wikipedia.org/wiki/Orbital_elements#Keplerian

        /// <summary>
        /// How Elongated the orbit is compated to a circle.
        /// </summary>
        public double Eccentricity { get; set; }

        /// <summary>
        /// The Average of Periapsis and Apoapsis.
        /// </summary>
        /// <remarks>
        /// If <see cref="Eccentricity"/> is 0,
        /// then SemiMajor Axis is equivalent to orbital Radius.
        /// </remarks>
        public double SemiMajorAxis { get; set; }

        /// <summary>
        /// The angle from the plane of reference the orbit is rotated from.
        /// </summary>
        public double Inclination { get; set; }

        /// <summary>
        /// Where the inclination is rotated around
        /// (where the orbit contacts the reference plane)
        /// </summary>
        public double AscendingNode { get; set; }

        /// <summary>
        /// The angle offset of the pariapsis from the Ascending Node.
        /// </summary>
        public double ArgumentOfPeriapsis { get; set; }

        /// <summary>
        /// The angle from the periapsis that is where the body is.
        /// </summary>
        public double CurrentAngle { get; set; }

        /// <summary>
        /// The angle offset that the body starts at.
        /// </summary>
        public double TrueAnomaly { get; set; }

        #endregion KeplerianElements
    }
}
