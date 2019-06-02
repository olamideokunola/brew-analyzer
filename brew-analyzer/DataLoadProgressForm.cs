using BrewingModel;
using BrewingModel.Datasources;
using BrewingModel.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace brew_analyzer
{
    public partial class DataLoadProgressForm : Form
    {
        private TrendAnalysisController trendAnalysisController;

        DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance();

        MyAppSettings appSettings = MyAppSettings.GetInstance();

        Datasource datasource;

        int numberOfBrewsToAdd;


        public DataLoadProgressForm(TrendAnalysisController trendAnalysisController)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            this.trendAnalysisController = trendAnalysisController;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DataLoadProgressForm_Load(object sender, EventArgs e)
        {
            string conStr = appSettings.ConnectionString;
            string tempPath = appSettings.PeriodTemplateFilePath;

            // Create Datasource for report
            datasource = new XlDatasource(conStr, tempPath);
            datasourceHandler.Datasource = datasource;

            // Perform a time consuming operation and report progress.
            IList<IBrew> brews = trendAnalysisController.GetBrews();

            Month month = trendAnalysisController.GetMonth();
            int year = trendAnalysisController.GetYear();

            // Get brews numbers already in datasource
            if (datasourceHandler.GetExistingBrewNumbers(month, year) != null)
            {
                IList<IDictionary<string, string>> existingBrewNumbers = datasourceHandler.GetExistingBrewNumbers(month, year);
                numberOfBrewsToAdd = brews.Count - existingBrewNumbers.Count;
            }
            else
            {
                numberOfBrewsToAdd = brews.Count;
            }

            //Setup progress bar
            // Display the ProgressBar control.
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            progressBar1.Maximum = numberOfBrewsToAdd;
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar1.Step = 1;

            lblLoadingStatus.Visible = true;

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }

        }

        // This event handler is where the time-consuming work is done.

        // This event handler updates the progress.
        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //    progressBar1.Value = e.ProgressPercentage;
        //    //progressBar1.Value = e.ProgressPercentage;
        //}

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            IList<IBrew> newBrews;

            // Perform a time consuming operation and report progress.
            IList<IBrew> brews = trendAnalysisController.GetBrews();
            Month month = trendAnalysisController.GetMonth();
            int year = trendAnalysisController.GetYear();

            if (datasourceHandler.GetExistingBrewNumbers(month, year) != null)
            {
                IList<IDictionary<string, string>> existingBrewNumbers = datasourceHandler.GetExistingBrewNumbers(month, year);

                int numberOfBrewsToAdd = brews.Count - existingBrewNumbers.Count;

                newBrews = datasourceHandler.RemoveExistingBrews(brews, existingBrewNumbers);
            }
            else
            {
                int numberOfBrewsToAdd = brews.Count;

                newBrews = brews;
            }

            int i = 0;
            foreach (IBrew brew in newBrews)
            {

                i++;
                datasourceHandler.SaveBrew(brew);
                System.Threading.Thread.Sleep(100);
                //worker.ReportProgress(1/brews.Count);
                backgroundWorker1.ReportProgress(i);

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblLoadingStatus.Text = ("Loading " + e.ProgressPercentage.ToString() + " of " + numberOfBrewsToAdd.ToString() + " brews...");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                lblLoadingStatus.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                lblLoadingStatus.Text = "Error: " + e.Error.Message;
            }
            else
            {
                lblLoadingStatus.Text = "Done, all brews loaded!";
            }
        }

        //// This event handler updates the progress.
        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    progressBar1.Value = e.ProgressPercentage;
        //    //progressBar1.PerformStep();
        //    //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        //}

        //// This event handler deals with the results of the background operation.
        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //if (e.Cancelled == true)
        //    //{
        //    //    resultLabel.Text = "Canceled!";
        //    //}
        //    //else if (e.Error != null)
        //    //{
        //    //    resultLabel.Text = "Error: " + e.Error.Message;
        //    //}
        //    //else
        //    //{
        //    //    resultLabel.Text = "Done!";
        //    //}
        //}


    }
}
