﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace AstrologyCalculator.Body
{
    /// <summary>
    /// Used for defining arbitrary orbits. It does not take Mass or gravity into acount.
    /// Could be used to define any orbit (including powered orbit).
    /// </summary>
    public class Orbital
    {
        #region HelperFunctions

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
        /// The distance of the orbiter from it's parent
        /// </summary>
        /// <param name="radAngle">The angle in the orbit that the body is at.</param>
        /// <returns>The distance in m.</returns>
        public double RadiusAtAnAngle(double radAngle)
        {
            return SemiMajorAxis * (1 - Math.Pow(Eccentricity, 2))
                / (1 + Eccentricity * Math.Cos(radAngle));
        }

        #endregion HelperFunctions

        #region DerivedProperties

        /// <summary>
        /// The current position of the body relative to the parent.
        /// </summary>
        public Point3D CurrentPoint
        {
            get
            {
                // Using Point3D for this x is r, y is theta, z is phi
                // physics standard (radius, polar angle, azimuthal angle)
                var spherePoint = new Point3D(CurrentRadius, Math.PI/2, 0);
                // The Highest angle the orbit will reach.
                var highestAngle = Inclination;
                // The Offset of the angle it starts at the highest angle at.
                if ((AscendingNode - Math.PI/2) < 0)
                {
                    phaseOffset
                }
                var phaseOffset = AscendingNode

                var resultingPolarAngle = highestAngle * Math.Cos(CurrentAngle - phaseOffset);

                // Convert from spherical to cartesian.
                return new Point3D();
            }
        }

        /// <summary>
        /// Orbital Period in Seconds.
        /// </summary>
        public virtual double OrbitalPeriod { get; set; }

        /// <summary>
        /// The Mean Motion of an orbit in Radians
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

        #endregion DerivedProperties

        #region KeplerianElements

        /// <summary>
        /// How Elongated the orbit is compated to a circle.
        /// </summary>
        public double Eccentricity { get; set; }

        /// <summary>
        /// The Average of Periapsis and Apoapsis in meters.
        /// </summary>
        /// <remarks>
        /// If <see cref="Eccentricity"/> is 0,
        /// then SemiMajor Axis is equivalent to orbital Radius.
        /// </remarks>
        public double SemiMajorAxis { get; set; }

        /// <summary>
        /// The angle from the plane of reference the orbit is rotated from in radians.
        /// </summary>
        public double Inclination { get; set; }

        /// <summary>
        /// Where the inclination is rotated around in radians.
        /// (where the orbit contacts the reference plane)
        /// </summary>
        public double AscendingNode { get; set; }

        /// <summary>
        /// The angle offset of the pariapsis from the Ascending Node in radians.
        /// </summary>
        public double ArgumentOfPeriapsis { get; set; }

        /// <summary>
        /// The angle from the periapsis that is where the body is in radians.
        /// </summary>
        public double CurrentAngle { get; set; }

        /// <summary>
        /// The angle offset that the body starts at in radians.
        /// </summary>
        public double TrueAnomaly { get; set; }

        #endregion KeplerianElements
    }
}
