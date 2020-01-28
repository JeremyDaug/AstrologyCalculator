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
        /// The Orbital of the body. If null it does not orbit and is the head of the system tree.
        /// </summary>
        public Orbital Orbital { get; private set; }

        /// <summary>
        /// The Mass of the body if the Orbital is null.
        /// </summary>
        private double _mass;

        /// <summary>
        /// The Mass of the body.
        /// </summary>
        public double Mass
        {
            get
            {
                if (Orbital == null) return _mass;

                return Orbital.BodyMass;
            }
            set
            {
                // Ensure it's valid.
                if (value <= 0) throw new ArgumentOutOfRangeException($"Body {Name}'s Mass cannot be less than or equal to 0.");

                // If Head set mass.
                if (Orbital == null) _mass = value;
                // Else update the orbit's body mass that we reference.
                else Orbital.BodyMass = value;

                // Then update all children's parent masses to be this mass.
                foreach (var child in Children) child.ParentMass = value;
            }
        }

        /// <summary>
        /// The (average) radius of the body at it's surface.
        /// </summary>
        public double BodyRadius { get; set; }

        /// <summary>
        /// How long the day is from noon to noon in seconds.
        /// </summary>
        public double DayLength { get; set; }

        /// <summary>
        /// How far the planet rotation is tilted from it's orbital plane.
        /// </summary>
        public double Tilt { get; set; }

        /// <summary>
        /// The Angular Velocity of the body around itself (in rad/sec).
        /// </summary>
        public double AngularVelocity
        {
            get
            {
                return 2 * Math.PI / DayLength;
            }
        }

        /// <summary>
        /// The moment of inertia of the body in kg r^2.
        /// </summary>
        public double MomentOfInertia
        {
            get
            {
                return 2 / 5 * Mass * BodyRadius * BodyRadius;
            }
        }

        /// <summary>
        /// The Angular Momentum of the Body in kg m ^2 / s.
        /// </summary>
        public double AngularMomentum
         {
            get
            {
                return AngularVelocity * MomentOfInertia;
            }
        }

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
                    return Orbital.ParentMass;
                return 0;
            }
            private set
            {
                Orbital.ParentMass = value;
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
        public string PositionalName
        {
            get
            {
                string result = "";
                if (Parent == null) return "1";

                result += Parent.PositionalName + ".";
                var index = Parent.Children.IndexOf(this).ToString();
                return result + index;
            }
        }

        public Body Head
        {
            get
            {
                if (Parent == null) return this;
                return Parent.Head;
            }
        }

        #endregion Properties
    }
}
