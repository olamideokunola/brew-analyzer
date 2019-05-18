using Analyzer;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace brew_analyzer
{
    public partial class TrendAnalysisGUI : Form, IObserver
    {

        private TrendAnalysisController trendAnalysisController;
        TrendAnalyzer trendAnalyzer;

        public TrendAnalysisGUI()
        {
            InitializeComponent();
           // SetController(trendAnalysisController);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void TrendAnalysisGUI_Load(object sender, EventArgs e)
        {
            
        }

        internal void SetController(TrendAnalysisController trendAnalysisController)
        {
            this.trendAnalysisController = trendAnalysisController;
            trendAnalysisController.AttachTrendAnalyzerObserver(this);
        }

        public void Update(Subject subject)
        {
            if (subject.GetType().ToString() == "Analyzer.TrendAnalyzer")
            {
                trendAnalyzer = (TrendAnalyzer) subject;

                if (trendAnalyzer.NumberOfBrewsMessage.Length > 0)
                {
                    DialogResult result = MessageBox.Show(trendAnalyzer.NumberOfBrewsMessage);
                }


            }
            
            if (subject.GetType().ToString() == "brew_analyzer.TrendAnalysisGuiModel")
            {
                TrendAnalysisGuiModel trendAnalysisGuiModel = (TrendAnalysisGuiModel)subject;
                UpdateBrewsListBox(trendAnalysisGuiModel.BrewsList);
            }
        }

        private void UpdateBrewsListBox(IList<string> brewsList)
        {
            IBrewsListAdapter brewsListAdapter = new BrewsListAdapter(this.lstBoxBrews, brewsList);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SetDate_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;
            trendAnalysisController.SetDates(startDate, endDate);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblBrewsListBox_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void lstBoxBrews_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
