using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace AstrologyCalculator.Body
{
    /// <summary>
    /// Used for defining arbitrary orbits.
    /// Could be used to define any orbit (including powered orbit).
    /// </summary>
    public class Orbital
    {
        #region BackingProperties

        private double _eccentricity;
        private double _semiMajorAxis;
        private double _inclination;
        private double _ascendingNode;
        private double _argumentOfPeriapsis;
        private double _currentAngle;
        private double _trueAnomaly;
        private Vector3D _positionVector;
        private Vector3D _velocityVector;
        private double _parentMass;
        private double _bodyMass;

        #endregion BackingProperties

        #region HelperFunctions

        public static double DegreeToRadian(double d)
        {
            return d * Math.PI / 180;
        }

        /// <summary>
        /// Rotate about x axis
        /// </summary>
        /// <param name="angle">The Angle of rotation</param>
        /// <returns>The Rotation Matrix</returns>
        protected Matrix3D Rotate_x(double angle)
        {
            return new Matrix3D(1, 0, 0, 0,
                                0, Math.Cos(angle), -Math.Sin(angle), 0,
                                0, Math.Sin(angle), Math.Cos(angle), 0,
                                0, 0, 0, 1);
        }

        /// <summary>
        /// Rotate about y axis
        /// </summary>
        /// <param name="angle">The Angle of rotation</param>
        /// <returns>The Rotation Matrix</returns>
        protected Matrix3D Rotate_y(double angle)
        {
            return new Matrix3D(Math.Cos(angle), 0, Math.Sin(angle), 0,
                                0, 1, 0, 0,
                                -Math.Sin(angle), 1, Math.Cos(angle), 0,
                                0, 0, 0, 1);
        }

        /// <summary>
        /// Rotate about z axis
        /// </summary>
        /// <param name="angle">The Angle of rotation</param>
        /// <returns>The Rotation Matrix</returns>
        protected Matrix3D Rotate_Z(double angle)
        {
            return new Matrix3D(Math.Cos(angle), -Math.Sin(angle), 0, 0,
                                Math.Sin(angle), Math.Cos(angle), 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1);
        }

        /// <summary>
        /// The average angular movement given a set period of time.
        /// </summary>
        /// <param name="time">The length of time (in seconds) we are measuring.</param>
        /// <returns>The Radian angle it would move in time.</returns>
        public double MeanMotionMovement(double time)
        {
            return MeanMotion * time;
        }

        /// <summary>
        /// The Mean Anomaly (the fraction of an orbit's period that has elapsed since Periapsis), in Radians.
        /// </summary>
        /// <param name="seconds"> The time since the </param>
        /// <returns>The angular distance from Periapsis.</returns>
        public double MeanAnomaly(double seconds)
        {
            return MeanMotion * seconds;
        }

        /// <summary>
        /// The distance of the orbiter from it's parent
        /// </summary>
        /// <param name="radAngle">The angle in the orbit that the body is at.</param>
        /// <returns>The distance in m.</returns>
        public double RadiusAtAnAngle(double radAngle)
        {
            return SemiMajorAxis * (1 - Math.Pow(Eccentricity, 2))
                / (1 + Eccentricity * Math.Cos(radAngle));
        }

        /// <summary>
        /// The Natural Orbital Period, Assuming an Unpowered orbit.
        /// </summary>
        /// <returns>The Natural Orbital Period</returns>
        public double NaturalOrbitalPeriod()
        {
            return 2 * Math.PI * Math.Sqrt(Math.Pow(SemiMajorAxis, 3) / GParam);
        }

        /// <summary>
        /// The Velocity of the Body at an angle in the orbit.
        /// </summary>
        /// <param name="radAngle">The position in orbit in Radians.</param>
        /// <returns>The Velocity of the body along the orbital path in m/s^2</returns>
        public double VelocityAtAnAngle(double radAngle)
        {
            return Math.Sqrt(GParam * (2 / RadiusAtAnAngle(radAngle) - 1 / SemiMajorAxis)) * OrbitalPeriod / NaturalOrbitalPeriod();
        }

        /// <summary>
        /// Gives the inner product of two vectors
        /// </summary>
        /// <param name="a">Vector 1</param>
        /// <param name="b">Vector 2</param>
        /// <returns>The Inner Product</returns>
        private double InnerProduct(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        #endregion HelperFunctions

        #region DerivedProperties

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

        /// <summary>
        /// The Angle from the center of the Ellipse to the current point.
        /// </summary>
        public double EccentricAnomaly
        {
            get
            {
                var E = Math.Sqrt(1 - Eccentricity * Eccentricity) * Math.Sin(CurrentAngle)
                    / Eccentricity + Math.Cos(CurrentAngle);
                return Math.Atan(E);
            }
        }

        /// <summary>
        /// Orbital Period in Seconds.
        /// </summary>
        public virtual double OrbitalPeriod { get; set; }

        /// <summary>
        /// The Mean Angular Motion of an orbit in Radians
        /// </summary>
        public double MeanMotion { get { return 2 * Math.PI / OrbitalPeriod; } }

        /// <summary>
        /// The Distance to the parent based on the current angle.
        /// </summary>
        public double CurrentRadius
        {
            get
            {
                return RadiusAtAnAngle(CurrentAngle);
            }
        }

        /// <summary>
        /// Get's the Minor Axis of the Elipse in meters.
        /// </summary>
        public double SemiMinorAxis
        {
            get
            {
                var eSqared = Math.Pow(Eccentricity, 2);
                var majorAxis = Math.Pow(SemiMajorAxis, 2);
                return Math.Pow(majorAxis * (1 - eSqared), 0.5);
            }
        }

        /// <summary>
        /// The area of the orbit, for calculating movement in m^2.
        /// </summary>
        public double Area
        {
            get
            {
                return Math.PI * SemiMajorAxis * SemiMinorAxis;
            }
        }

        /// <summary>
        /// The Closest distance to the main body in the orbit in m.
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
        /// The furthest distance in the orbit in m.
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
        /// Gets the Acceleration towards the parent body in 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public double AccelerationAtRadius(double r)
        {
            return G * ParentMass / Math.Pow(r, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public double ParentMassBasedOnForceAtRadius(double a, double r)
        {
            return a * r * r / G;
        }

        /// <summary>
        /// The Velocity of the body at the current point.
        /// </summary>
        public double VVelocity
        {
            get
            {
                return VelocityVector.Length;
            }
            set
            {
                var mod = value / VelocityVector.Length;
                VelocityVector = mod * VelocityVector;
            }
        }

        public double KVelocity
        {
            get
            {
                return Math.Sqrt(GParam * (2 / CurrentRadius - 1 / SemiMajorAxis));
            } 
            set
            {
                SemiMajorAxis = GParam * CurrentRadius / (CurrentRadius * value * value - 2 * GParam);
            }
        }

        #endregion DerivedProperties

        #region UpdateFunctions

        private void VectorsUpdated()
        {
            // https://downloads.rene-schwarz.com/download/M002-Cartesian_State_Vectors_to_Keplerian_Orbit_Elements.pdf
            // From State Vector to Keplerian Elements
            var orbMomentumVec = Vector3D.CrossProduct(PositionVector, VelocityVector);
            var eccVec = Vector3D.CrossProduct(VelocityVector, orbMomentumVec) / GParam
                - PositionVector / PositionVector.Length;

            var transposition = new Vector3D(0, 0, 1);
            var nToAscNode = Vector3D.CrossProduct(new Vector3D(0, 0, 1), orbMomentumVec);

            if (InnerProduct(PositionVector, VelocityVector) >= 0)
                _trueAnomaly = Math.Acos(InnerProduct(eccVec, PositionVector) / (eccVec.Length * PositionVector.Length));
            else
                _trueAnomaly = 2 * Math.PI - Math.Acos(InnerProduct(eccVec, PositionVector) / (eccVec.Length * PositionVector.Length));

            _inclination = Math.Acos(orbMomentumVec.Z / orbMomentumVec.Length);

            _eccentricity = eccVec.Length;

            if (nToAscNode.Y >= 0) _ascendingNode = Math.Acos(nToAscNode.Y / nToAscNode.Length);
            else _ascendingNode = 2 * Math.PI - Math.Acos(nToAscNode.Y / nToAscNode.Length);

            if (eccVec.Z >= 0) _argumentOfPeriapsis = Math.Acos(InnerProduct(nToAscNode, eccVec) / (nToAscNode.Length * eccVec.Length));
            else _argumentOfPeriapsis = 2 * Math.PI - Math.Acos(InnerProduct(nToAscNode, eccVec) / (nToAscNode.Length * eccVec.Length));

            var EccAn = 2 * Math.Atan(Math.Tan(TrueAnomaly / 2) /
                Math.Sqrt((1 + Eccentricity) / (1 - Eccentricity)));

            _semiMajorAxis = 1 / (2 / PositionVector.Length - VelocityVector.LengthSquared / GParam);
        }

        private void ElementsUpdated()
        {
            // https://downloads.rene-schwarz.com/download/M001-Keplerian_Orbit_Elements_to_Cartesian_State_Vectors.pdf

            var v = Math.Sqrt(GParam * SemiMajorAxis) / CurrentRadius *
                new Vector3D(-Math.Sin(EccentricAnomaly),
                             Math.Sqrt(1 - Eccentricity * Eccentricity) * Math.Cos(EccentricAnomaly),
                             0);

            var rotation = Rotate_Z(-AscendingNode) * Rotate_x(-Inclination) * Rotate_Z(-ArgumentOfPeriapsis);
            _velocityVector = Vector3D.Multiply(v, rotation);

            var p = new Vector3D(CurrentRadius * Math.Cos(CurrentAngle), CurrentRadius * Math.Sin(CurrentAngle), 0);

            _positionVector = Vector3D.Multiply(p, rotation);
        }

        private void GravityUpdated()
        {
            VectorsUpdated();
            ElementsUpdated();
        }

        #endregion UpdateFunctions

        #region StateVectorElements

        public Vector3D VelocityVector
        {
            get { return _velocityVector; }
            set
            {
                if (_velocityVector == value)
                    return;
                _velocityVector = value;
                VectorsUpdated();
            }
        }

        public Vector3D PositionVector
        {
            get { return _positionVector; }
            set
            {
                if (_positionVector == value)
                    return;
                _positionVector = value;
                VectorsUpdated();
            }
        }

        #endregion StateVectorElements

        #region GravitationalProperties

        /// <summary>
        /// The Gravitational parameter of the body and it's parent.
        /// </summary>
        public double GParam => G * (ParentMass + BodyMass);

        /// <summary>
        /// Mass of the parent body being orbited in kg.
        /// </summary>
        public double ParentMass
        {
            get { return _parentMass; }
            set
            {
                if (_parentMass == value)
                    return;
                _parentMass = value;
                GravityUpdated();
            }
        }

        /// <summary>
        /// The mass of the body that is orbiting in kg.
        /// </summary>
        public double BodyMass
        {
            get { return _bodyMass; }
            set
            {
                if (_bodyMass == value)
                    return;
                _bodyMass = value;
                GravityUpdated();
            }
        }

        /// <summary>
        /// The Gravity Constant in units of m^3 / (kg s^2)
        /// </summary>
        public const double G = 6.67408E-11;

        #endregion GravitationalProperties

        #region KeplerianElements

        /// <summary>
        /// How Elongated the orbit is compated to a circle.
        /// </summary>
        public double Eccentricity
        {
            get { return _eccentricity; }
            set
            {
                if (_eccentricity == value)
                    return;
                _eccentricity = value;
                ElementsUpdated();
            }
        }

        /// <summary>
        /// The Average of Periapsis and Apoapsis in meters.
        /// </summary>
        /// <remarks>
        /// If <see cref="Eccentricity"/> is 0,
        /// then SemiMajor Axis is equivalent to orbital Radius.
        /// </remarks>
        public double SemiMajorAxis
        {
            get { return _semiMajorAxis; }
            set
            {
                if (_semiMajorAxis== value)
                    return;
                _semiMajorAxis = value;
                ElementsUpdated();
            }
        }

        /// <summary>
        /// The angle from the plane of reference the orbit is rotated from in radians.
        /// </summary>
        public double Inclination
        {
            get { return _inclination; }
            set
            {
                if (_inclination == value)
                    return;
                _inclination = value;
                ElementsUpdated();
            }
        }

        /// <summary>
        /// Where the inclination is rotated around in radians.
        /// (where the orbit contacts the reference plane)
        /// </summary>
        public double AscendingNode
        {
            get { return _ascendingNode; }
            set
            {
                if (_ascendingNode == value)
                    return;
                _ascendingNode = value;
                ElementsUpdated();
            }
        }

        /// <summary>
        /// The angle offset of the pariapsis from the Ascending Node in radians.
        /// </summary>
        public double ArgumentOfPeriapsis
        {
            get { return _argumentOfPeriapsis; }
            set
            {
                if (_argumentOfPeriapsis == value)
                    return;
                _argumentOfPeriapsis = value;
                ElementsUpdated();
            }
        }

        /// <summary>
        /// The angle from the periapsis that is where the body is in radians. Position at time t.
        /// </summary>
        public double CurrentAngle
        {
            get { return _currentAngle; }
            set
            {
                if (_currentAngle == value)
                    return;
                _currentAngle = value;
                ElementsUpdated();
            }
        }

        /// <summary>
        /// The angle offset that the body starts at in radians. Position at t_0.
        /// </summary>
        public double TrueAnomaly
        {
            get { return _trueAnomaly; }
            set
            {
                if (_trueAnomaly == value)
                    return;
                _trueAnomaly = value;
                ElementsUpdated();
            }
        }

        #endregion KeplerianElements
    }
}
