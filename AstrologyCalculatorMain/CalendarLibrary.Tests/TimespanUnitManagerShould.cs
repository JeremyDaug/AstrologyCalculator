using NUnit.Framework;

namespace CalendarLibrary.Tests
{
    public class TimespanUnitManagerShould
    {
        private TimespanUnitManager sut;

        private const string testFileName = "TestTimeUnits.xml";
        private const string testUnitName = "galactic_startard_week";
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
            var testUnit = "galactic_standard_week";
            sut.Add(testUnit, 15, 1);

            sut.SaveTo(testFileName);
        }
    }
}