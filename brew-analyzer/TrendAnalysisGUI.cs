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

        public TrendAnalysisGUI()
        {
            InitializeComponent();
            //SetController(trendAnalysisController);
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
                TrendAnalyzer trendAnalyzer = (TrendAnalyzer) subject;

                if (trendAnalyzer.NumberOfBrewsMessage.Length > 0)
                {
                    DialogResult result = MessageBox.Show(trendAnalyzer.NumberOfBrewsMessage);
                }
            }
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
    }
}
