using Analyzer;
using BrewingModel;
using BrewingModel.Datasources;
using BrewingModel.Reports;
using BrewingModel.Settings;
using ObserverSubject;
using System;
using System.Collections;
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

        public TrendAnalysisController trendAnalysisController;
        TrendAnalyzer trendAnalyzer;

        Form dataLoadingProgressForm;

        public TrendAnalysisGUI()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void TrendAnalysisGUI_Load(object sender, EventArgs e)
        {
            SetupYearsCombo();
            SetupMonthsCombo();
        }

        private void SetupMonthsCombo()
        {
            IList<string> monthsList = new List<string>();
            cmbMonth.DataSource = Enum.GetNames(typeof(Month));
        }

        private void SetupYearsCombo()
        {
            IList<int> yearsList = new List<int>();

            int thisYear = DateTime.Today.Year;

            for (int i = 2018; i <= thisYear + 10; i++)
            {
                yearsList.Add(i);
            }

            cmbYear.DataSource = yearsList;
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
            trendAnalysisController.SetDates();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblBrewsListBox_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //DateTime startDate = dtpStartDate.Value;
            //DateTime endDate = dtpEndDate.Value;

            CreateDateRangeForMonth();
            trendAnalysisController.SetFileDestination("");

            trendAnalysisController.RunAnalysis();

            // Create Datasource for report
            DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance();

            MyAppSettings appSettings = MyAppSettings.GetInstance();

            string conStr = appSettings.ConnectionString;
            string tempPath = appSettings.PeriodTemplateFilePath;

            Datasource datasource = new XlDatasource(conStr, tempPath);
            datasourceHandler.Datasource = datasource;


            //IList<IBrew> brews = trendAnalysisController.GetBrews();

            ////Setup progress bar
            //// Display the ProgressBar control.
            //progressBar1.Visible = true;
            //// Set Minimum to 1 to represent the first file being copied.
            //progressBar1.Minimum = 1;
            //// Set Maximum to the total number of files to copy.
            //progressBar1.Maximum = brews.Count ;
            //// Set the initial value of the ProgressBar.
            //progressBar1.Value = 1;
            //// Set the Step property to a value of 1 to represent each file being copied.
            //progressBar1.Step = 1;

            dataLoadingProgressForm = new DataLoadProgressForm(trendAnalysisController);
            dataLoadingProgressForm.ShowDialog();

            //if (backgroundWorker1.IsBusy != true)
            //{
            //    // Start the asynchronous operation.
            //    backgroundWorker1.RunWorkerAsync();
            //}


            //foreach (IBrew brew in brews)
            //{
            //    datasourceHandler.SaveBrew(brew);

            //    //progressBar1.Step = 20;
            //    progressBar1.PerformStep();

            //}

            // Generate Report
            ReportGenerator reportGenerator = new XlReportGenerator();
            //reportGenerator.LoadPeriods();

            string yearStr = cmbYear.SelectedItem.ToString();
            Month month = (Month)Enum.Parse(typeof(Month), cmbMonth.SelectedItem.ToString());

            reportGenerator.CreateReport(yearStr, month, "testingReport", "C:\\Users\\Olamide Okunola\\Documents");
        }

        private void lstBoxBrews_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedItem != null)
            {
                CreateDateRangeForMonth();
            }
        }

        private void CreateDateRangeForMonth()
        {
            int year = (int) cmbYear.SelectedItem;
            Month month = (Month) Enum.Parse(typeof(Month), cmbMonth.SelectedItem.ToString());
            int monthInt = (int)month + 1;
            int endDay = DateTime.DaysInMonth(year, monthInt);

            trendAnalysisController.SetStartDate(new DateTime(year, monthInt, 1));
            trendAnalysisController.SetEndDate(new DateTime(year, monthInt, endDay));

            trendAnalysisController.SetDates();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.SelectedItem != null)
            {
                CreateDateRangeForMonth();
            }
        }




    }
}
