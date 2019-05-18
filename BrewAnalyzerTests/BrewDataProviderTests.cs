using NUnit.Framework;
using BrewDataProvider;
using System.Collections.Generic;
using BrewDataProvider.ActiveBrewMonitor;
using BrewDataProvider.BrewMonitor;
using System;

namespace Tests
{
    public class BrewDataProviderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetBrewFileNames()
        {
            ArrayBrewsGetter arrayBrewsGetter = new ArrayBrewsGetter();
            IList<string> years = new List<string>();
            years.Add("2018");

            int startYear = 2018;
            int endYear = 2018;
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2018, 12, 31);

            int startDay = startDate.Day;
            int endDay = endDate.Day;

            IFileParser fileParser = new FileParser();
            BrewLoaderAndMaker brewLoaderAndMaker = new BrewLoaderAndMaker(fileParser);

            //arrayBrewsGetter.GetBrewsInYears(years, startYear, endYear, startDay, endDay, brewLoaderAndMaker);
            string[] names = arrayBrewsGetter.GetBrewFileNames(years);

            Assert.Equals(names.Length, 1);
            //Assert.Pass();
        }
    }
}