using System;
using System.Collections.Generic;

namespace AstrologyCalculator
{
    public class Body
    {
        #region Backing Fields
        private double periapsis;
        private double apoapsis;
        private double eccentricity;
        private double inclination;
        private double ascendingNodeLongitude;
        private double argumentOfPeriapsis;
        private double semimajorAxis;
        #endregion

        // Structural and Independent Data

        /// <summary>
        /// Mass of the body in kg
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Radius of the body in km
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// What kind of body it is.
        /// </summary>
        public BodyType BodyType { get; set; }

        /// <summary>
        /// If the body is the center of the system.
        /// </summary>
        public bool Head { get; set; }

        /// <summary>
        /// The parent, dominant body which this orbits.
        /// Null if Head is true.
        /// </summary>
        public Body Parent { get; set; }

        /// <summary>
        /// The bodies which orbit this body. May be empty
        /// </summary>
        public IList<Body> Children { get; set; }

        #region Keplerian Elements
        // Orbital Elements the most detailed options

        /// <summary>
        /// Orbital Radius
        /// </summary>
        public double SemimajorAxis { get => semimajorAxis; set => semimajorAxis = value; }

        #region Radius Subcomponents
        /// <summary>
        /// Closest distance from the parent.
        /// </summary>
        public double Periapsis
        {
            get { return periapsis; }
            set
            {
                periapsis = value;
                UpdateSemiMajorAxis();
                UpdateEccentricity();
            }
        }

        /// <summary>
        /// Furthest distance from the parent.
        /// </summary>
        public double Apoapsis { get => apoapsis; set => apoapsis = value; }
        #endregion

        /// <summary>
        /// Distortion from a perfectly circlular orbit.
        /// </summary>
        public double Eccentricity { get => eccentricity; set => eccentricity = value; }

        /// <summary>
        /// Degree rotation from Reference plane at <see cref="AscendingNodeLongitude"/>.
        /// </summary>
        public double Inclination { get => inclination; set => inclination = value; }

        /// <summary>
        /// Where the inclination rotates around.
        /// </summary>
        public double AscendingNodeLongitude { get => ascendingNodeLongitude; set => ascendingNodeLongitude = value; }

        /// <summary>
        /// Where along the orbital plane the <see cref="Periapsis"/> is.
        /// </summary>
        public double ArgumentOfPeriapsis { get => argumentOfPeriapsis; set => argumentOfPeriapsis = value; }

        /// <summary>
        /// Where the body starts in it's orbit at time 0.
        /// </summary>
        public double Anomaly { get; set; }

        #endregion

        #region Time Based Orbital Parameters

        /// <summary>
        /// How long it takes to orbit.
        /// </summary>
        public double OrbitPeriod { get; set; }

        #endregion

        private void UpdateSemiMajorAxis()
        {
            SemimajorAxis = (Periapsis + Apoapsis) / 2;
        }

        private void UpdateEccentricity()
        {
            eccentricity = 1 - 2 / ((Apoapsis / Periapsis) + 1);
        }
    }
}
