using Analyzer;
using BrewDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace brew_analyzer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create DataProvider
            IDataProvider dataProvider = new BrewDataProvider.BrewDataProvider();

            // Create Facade Controller for Trend Analysis
            IAnalyzer analyzerFacade = new AnalysisFacade(dataProvider);
            // Create GuiModel
            TrendAnalysisGuiModel trendAnalysisGuiModel = new TrendAnalysisGuiModel();

            // Create MVC Controller for GUI
            TrendAnalysisController trendAnalysisController = new TrendAnalysisController(analyzerFacade, trendAnalysisGuiModel);

            // Create GUI View
            TrendAnalysisGUI trendAnalysisGUI = new TrendAnalysisGUI();
            // Set MVC Controller
            trendAnalysisGUI.SetController(trendAnalysisController);

            Application.Run(trendAnalysisGUI);

            
        }
    }
}
