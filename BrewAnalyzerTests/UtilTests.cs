using NUnit.Framework;
using Util;

namespace Tests
{
    public class UtilTests
    {
        StringDateWorker stringDateWorker;

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
    }
}