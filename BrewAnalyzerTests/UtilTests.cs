using BrewingModel.Datasources;
using NUnit.Framework;
using Util;

namespace Tests
{
    public class UtilTests
    {
        StringDateWorker stringDateWorker;
        DateHelper dateHelper;

        [SetUp]
        public void Setup()
        {
            stringDateWorker = StringDateWorker.GetInstance();
        }

        [Test]
        public void TestGetMonthNumber()
        {
            Assert.AreEqual("01", stringDateWorker.GetMonthNumber("January"));
            // Assert.Pass(,);
        }

        [Test]
        public void TestGetTwoDigitNumber()
        {
            Assert.AreEqual("02", stringDateWorker.GetTwoDigitNumber("2"));
            // Assert.Pass(,);
        }

        [Test]
        public void TestGetWeeksInMonth()
        { 
            Assert.AreEqual(5, DateHelper.GetWeeksInMonth(Month.January, 2019).Count);
            // Assert.Pass(,);
        }
    }
}
