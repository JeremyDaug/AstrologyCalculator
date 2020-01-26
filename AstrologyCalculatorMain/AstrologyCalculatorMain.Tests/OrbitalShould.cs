using AstrologyCalculator.Body;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace AstrologyCalculatorMain.Tests
{
    [TestFixture]
    public class OrbitalShould
    {
        private Orbital sut;

        [SetUp]
        public void Setup()
        {
            // Using Earth - Moon Orbit for sanity reasons.
            sut = new Orbital
            {
                ParentMass = 5.97237e24,
                BodyMass = 7.342e22,
                SemiMajorAxis = 384399000,
                Eccentricity = 0.0549,
                Inclination = Orbital.DegreeToRadian(5.145),
                AscendingNode = 0,
                ArgumentOfPeriapsis = 0,
                TrueAnomaly = 0
            };
        }

        [Test]
        public void StateVectorsAndElementsMatchCorrectly()
        {
            Assert.That(sut.PositionVector, Is.Not.EqualTo(new Vector3D(0, 0, 0)));
            Assert.That(sut.VelocityVector, Is.Not.EqualTo(new Vector3D(0, 0, 0)));

            var marginOfErrorAllowed = 0.001;

            var Error = Math.Abs((sut.VVelocity - sut.KVelocity )/ sut.VVelocity);

            Assert.That(Error, Is.LessThan(marginOfErrorAllowed));
        }

        [Test]
        public void DegreesToRadianConversion()
        {
            Assert.That(Orbital.DegreeToRadian(180), Is.EqualTo(Math.PI));
        }
    }
}
