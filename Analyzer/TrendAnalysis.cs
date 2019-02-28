using BrewDataProvider;

namespace Analyzer
{
    class TrendAnalysis
    {
        private IDataProvider dataProvider;

        public TrendAnalysis(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }


    }
}
