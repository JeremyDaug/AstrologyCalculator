using System;
using System.Collections.Generic;

namespace AstrologyCalculator.Body
{
    public class Body
    {
        // See https://en.wikipedia.org/wiki/Orbital_mechanics for all orbital needs

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
                return 0;
            }
        }

        #endregion Properties
    }
}
