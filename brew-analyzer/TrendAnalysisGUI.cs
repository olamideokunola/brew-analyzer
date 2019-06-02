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
            SetupFolderBrowserDialog();
        }

        private void SetupFolderBrowserDialog()
        {
            folderBrowserDialog1.Description = "Select the destination folder for the trend report.";
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal;
        }

        private void SetupMonthsCombo()
        {
            //IList<string> monthsList = new List<string>();
            cmbMonth.DataSource = Enum.GetNames(typeof(Month));
        }

        private void SetupWeeksInMonthCombo()
        {
            cmbWeeksInMonth.DataSource = trendAnalysisController.GetWeeksInMonth();
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
            CreateDateRangeForMonth();

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileDestination = folderBrowserDialog1.SelectedPath;
                trendAnalysisController.SetFileDestination(fileDestination);

                string reportName = txtReportName.Text;
                trendAnalysisController.SetReportName(reportName);

                trendAnalysisController.LoadBrews();

                // if number of brews to add is more than zero start data download
                int numberOfBrewsToAdd = trendAnalysisController.GetNumberOfBrewsToAdd();

                // Open download form to download brews from brews file server
                if (numberOfBrewsToAdd > 0)
                {
                    dataLoadingProgressForm = new DataLoadProgressForm(trendAnalysisController);
                    dataLoadingProgressForm.ShowDialog();
                }

                // Generate Report
                trendAnalysisController.GenerateWeekReport();

                // Report Completion
                // MessageBox messageBox
            }            
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
            int monthInt = (int)month;
            int endDay = DateTime.DaysInMonth(year, monthInt);

            trendAnalysisController.SetStartDate(new DateTime(year, monthInt, 1));
            trendAnalysisController.SetEndDate(new DateTime(year, monthInt, endDay));

            trendAnalysisController.SetMonth(month);
            trendAnalysisController.SetYear(year);

            trendAnalysisController.SetDates();

            trendAnalysisController.SetWeeksInMonth(month, year);

            SetupWeeksInMonthCombo();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.SelectedItem != null)
            {
                CreateDateRangeForMonth();
            }
        }

        private void cmbWeeksInMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string weekString = cmbWeeksInMonth.SelectedItem.ToString();
            int week = int.Parse(weekString.Substring(5, weekString.Length - 5));
            trendAnalysisController.SetSelectedWeek(week);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
