using AstrologyCalculatorMain.Tests.Properties;
using NUnit.Framework;
using System.IO;

namespace AstrologyCalculator.TimespanUnits.Tests
{
    public class TimespanUnitManagerShould
    {
        private TimespanUnitManager sut;
        private const string testTimeUnits = "D:\\Projects\\AstrologyCalculator\\AstrologyCalculatorMain\\AstrologyCalculatorMain.Tests\\TestTimeUnits.xml";
        private const string testFileSaveName = "TestTimeSaveUnits.xml";
        private const string testUnitName = "galactic_standard_week";
        private const double testUnitlength = 15;
        private const double testUnitRank = 1;

        [SetUp]
        public void Setup()
        {
            sut = new TimespanUnitManager();
        }

        [Test]
        public void SaveDataToXmlFile()
        {
            sut.AddOrOverride(testUnitName, 15, 1);

            sut.SaveTo(testFileSaveName);
        }

        [Test]
        public void LoadDataFromXmlFile()
        {
            sut.LoadFrom(testTimeUnits);

            Assert.That(sut.AvailableUnits().Contains(testUnitName), Is.True);

            Assert.That(sut.GetLength(testUnitName), Is.EqualTo(testUnitlength));
            Assert.That(sut.GetRank(testUnitName), Is.EqualTo(testUnitRank));
        }
    }
}