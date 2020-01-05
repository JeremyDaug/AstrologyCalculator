using NUnit.Framework;

namespace AstrologyCalculator.TimespanUnits.Tests
{
    public class TimespanUnitManagerShould
    {
        private TimespanUnitManager sut;

        private const string testFileName = "TestTimeUnits.xml";
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
            sut.LoadFrom(testFileName);

            Assert.That(sut.AvailableUnits().Contains(testUnitName), Is.True);

            Assert.That(sut.GetLength(testUnitName), Is.EqualTo(testUnitlength));
            Assert.That(sut.GetRank(testUnitName), Is.EqualTo(testUnitRank));
        }
    }
}