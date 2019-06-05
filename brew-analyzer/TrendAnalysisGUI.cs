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
using System.Windows.Forms.DataVisualization.Charting;

namespace brew_analyzer
{
    public class TrendAnalysisGUI : Form, IObserver
    {

        // GUI Elements
        private System.Windows.Forms.Button btnSetDates;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblSetDates;
        private System.Windows.Forms.ListBox lstBoxBrews;
        private System.Windows.Forms.Button btnCreateTrendChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWeeksInMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart trendChart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butNextDataSet;
        private System.Windows.Forms.Button butPreviousDataSet;


        // MVC Elements
        public TrendAnalysisController trendAnalysisController;
        TrendAnalyzer trendAnalyzer;

        Form dataLoadingProgressForm;


        public TrendAnalysisGUI()
        {
            InitializeComponent();
            SetupComponents();
        }

        private void InitializeComponent()
        {
            this.btnSetDates = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblSetDates = new System.Windows.Forms.Label();
            this.lstBoxBrews = new System.Windows.Forms.ListBox();
            this.btnCreateTrendChart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWeeksInMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtReportName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.trendChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.butNextDataSet = new System.Windows.Forms.Button();
            this.butPreviousDataSet = new System.Windows.Forms.Button();
        }

        private void SetupComponents()
        {
            // 
            // btnSetDates
            // 
            this.btnSetDates.Location = new System.Drawing.Point(847, 491);
            this.btnSetDates.Name = "btnSetDates";
            this.btnSetDates.Size = new System.Drawing.Size(129, 23);
            this.btnSetDates.TabIndex = 0;
            this.btnSetDates.Text = "Set Dates";
            this.btnSetDates.UseVisualStyleBackColor = true;
            this.btnSetDates.Visible = false;
            this.btnSetDates.Click += new System.EventHandler(this.SetDate_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(847, 430);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.Visible = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(847, 459);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 2;
            this.dtpEndDate.Visible = false;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(774, 434);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 3;
            this.lblStartDate.Text = "Start Date";
            this.lblStartDate.Visible = false;
            this.lblStartDate.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(774, 462);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "End Date";
            this.lblEndDate.Visible = false;
            // 
            // lblSetDates
            // 
            this.lblSetDates.AutoSize = true;
            this.lblSetDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetDates.Location = new System.Drawing.Point(22, 26);
            this.lblSetDates.Name = "lblSetDates";
            this.lblSetDates.Size = new System.Drawing.Size(175, 20);
            this.lblSetDates.TabIndex = 5;
            this.lblSetDates.Text = "Set Year and Month:";
            this.lblSetDates.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // lstBoxBrews
            // 
            this.lstBoxBrews.FormattingEnabled = true;
            this.lstBoxBrews.Location = new System.Drawing.Point(124, 187);
            this.lstBoxBrews.Name = "lstBoxBrews";
            this.lstBoxBrews.Size = new System.Drawing.Size(200, 95);
            this.lstBoxBrews.TabIndex = 6;
            this.lstBoxBrews.SelectedIndexChanged += new System.EventHandler(this.lstBoxBrews_SelectedIndexChanged);
            // 
            // btnCreateTrendChart
            // 
            this.btnCreateTrendChart.Location = new System.Drawing.Point(124, 479);
            this.btnCreateTrendChart.Name = "btnCreateTrendChart";
            this.btnCreateTrendChart.Size = new System.Drawing.Size(129, 23);
            this.btnCreateTrendChart.TabIndex = 10;
            this.btnCreateTrendChart.Text = "Create Trend Chart";
            this.btnCreateTrendChart.UseVisualStyleBackColor = true;
            this.btnCreateTrendChart.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Available Brews:";
            this.label1.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select Week to Trend:";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(35, 68);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(32, 13);
            this.lblYear.TabIndex = 13;
            this.lblYear.Text = "Year:";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(35, 104);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(40, 13);
            this.lblMonth.TabIndex = 14;
            this.lblMonth.Text = "Month:";
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(124, 65);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(73, 21);
            this.cmbYear.TabIndex = 15;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(124, 96);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(120, 21);
            this.cmbMonth.TabIndex = 16;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Create trend chart:";
            // 
            // cmbWeeksInMonth
            // 
            this.cmbWeeksInMonth.FormattingEnabled = true;
            this.cmbWeeksInMonth.Location = new System.Drawing.Point(124, 349);
            this.cmbWeeksInMonth.Name = "cmbWeeksInMonth";
            this.cmbWeeksInMonth.Size = new System.Drawing.Size(120, 21);
            this.cmbWeeksInMonth.TabIndex = 18;
            this.cmbWeeksInMonth.SelectedIndexChanged += new System.EventHandler(this.cmbWeeksInMonth_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Week in Month:";
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(124, 443);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(200, 20);
            this.txtReportName.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 446);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Report Name:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // trendChart
            // 

            this.trendChart.Location = new System.Drawing.Point(347, 65);
            this.trendChart.Name = "trendChart";
            this.trendChart.Size = new System.Drawing.Size(720, 457);
            this.trendChart.TabIndex = 22;
            this.trendChart.Text = "chart1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(343, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "View Trend Chart:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            //
            // butNextDataSet
            //
            this.butNextDataSet.Size = new System.Drawing.Size(25, 25);
            this.butNextDataSet.Location = new System.Drawing.Point(trendChart.Right - butNextDataSet.Width, trendChart.Top - butNextDataSet.Height);
            this.butNextDataSet.Name = "butNextDataSet";
            //this.butNextDataSet.TabIndex = ;
            this.butNextDataSet.Text = ">";
            this.butNextDataSet.UseVisualStyleBackColor = true;
            this.butNextDataSet.Click += new System.EventHandler(this.butNextDataSet_Click);
            //
            // butPreviousDataSet
            //
            this.butPreviousDataSet.Size = new System.Drawing.Size(25, 25);
            this.butPreviousDataSet.Location = new System.Drawing.Point(trendChart.Right - (butPreviousDataSet.Width*2), butNextDataSet.Top);
            this.butPreviousDataSet.Name = "butPreviousDataSet";
            //this.butPreviousDataSet.TabIndex = ;
            this.butPreviousDataSet.Text = "<";
            this.butPreviousDataSet.UseVisualStyleBackColor = true;
            this.butPreviousDataSet.Click += new System.EventHandler(this.butPreviousDataSet_Click);
            // 
            // TrendAnalysisGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 547);
            this.Controls.Add(this.butPreviousDataSet);
            this.Controls.Add(this.butNextDataSet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trendChart);
            //this.Controls.Add(this.label5);
            //this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbWeeksInMonth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateTrendChart);
            this.Controls.Add(this.lstBoxBrews);
            this.Controls.Add(this.lblSetDates);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.btnSetDates);
            this.Name = "TrendAnalysisGUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TrendAnalysisGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trendChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private void TrendAnalysisGUI_Load(object sender, EventArgs e)
        {
            SetupYearsCombo();
            SetupMonthsCombo();
            SetupFolderBrowserDialog();

            SetupTrendChart();

        }
        public void Update(Subject subject)
        {
            if (subject.GetType().ToString() == "Analyzer.TrendAnalyzer")
            {
                trendAnalyzer = (TrendAnalyzer)subject;

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



        private void SetupTrendChart()
        {
            // Setup series
            //trendChart = new Chart();
            trendChart.Series.Add("Series1");
            Series series = trendChart.Series["Series1"];

            // Setup chart type
            series.ChartType = SeriesChartType.Line;

            // Setup chart title
            trendChart.Titles.Add("title1");
            trendChart.Titles[0].Text = "";

            // Setup XAxis
            trendChart.ChartAreas.Add(new ChartArea());
            trendChart.ChartAreas[0].AxisX.LabelStyle.IntervalOffset = 1;
            trendChart.ChartAreas[0].AxisX.LabelStyle.Interval = 2;

            // Setup YAxis


            trendChart.ChartAreas[0].AxisY.IsStartedFromZero = false;

            //trendChart.ChartAreas[0].AxisY.LabelStyle.Format = "HH:mm";
            //trendChart.ChartAreas[0].AxisY.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
            trendChart.ChartAreas[0].AxisY.LabelStyle.Interval = 60;
        }

        private void UpdateTrendChartWithFirstSeriesDataSet()
        {
            // Reset chart
            ResetChart();

            // Get first dataset
            IDictionary<string, TimeSpan> seriesDataSet = trendAnalysisController.GetFirstTrendChartSeriesDataSet();

            // Get first chart title
            string chartTitle = trendAnalysisController.GetFirstTrendChartProcessParameter();

            UpdateTrendChart(chartTitle, seriesDataSet);
        }
        private void ResetChart()
        {
            // Reset chart
            trendChart.Series["Series1"].Points.Clear();
            trendChart.ChartAreas[0].AxisX.CustomLabels.Clear();
        }


        private void UpdateTrendChartWithNextSeriesDataSet()
        {
            // Reset chart
            ResetChart();

            // Get next chartdataset
            IDictionary<string, IDictionary<string, TimeSpan>> chartDataSet = trendAnalysisController.GetNextTrendChartSeriesDataSet();

            // Get seriesdataset
            IDictionary<string, TimeSpan> seriesDataSet = chartDataSet.ElementAt(0).Value;

            // Get chart title
            string chartTitle = chartDataSet.ElementAt(0).Key;

            UpdateTrendChart(chartTitle, seriesDataSet);
        }

        private void UpdateTrendChartWithPreviousSeriesDataSet()
        {
            // Reset chart
            ResetChart();

            // Get next chartdataset
            IDictionary<string, IDictionary<string, TimeSpan>> chartDataSet = trendAnalysisController.GetPreviousTrendChartSeriesDataSet();

            // Get seriesdataset
            IDictionary<string, TimeSpan> seriesDataSet = chartDataSet.ElementAt(0).Value;

            // Get chart title
            string chartTitle = chartDataSet.ElementAt(0).Key;

            UpdateTrendChart(chartTitle, seriesDataSet);
        }

        private void UpdateTrendChart(string chartTitle, IDictionary<string, TimeSpan> seriesDataSet)
        {
            TimeSpan ts;
            double yValue;

            foreach (string key in seriesDataSet.Keys)
            {
                ts = seriesDataSet[key];
                yValue = ts.Hours * 60 + ts.Minutes + ts.Seconds / 60;

                trendChart.Series["Series1"].Points.AddY(yValue);
            }

            // Add XAxis labels
            int i = 1;
            foreach (string key in seriesDataSet.Keys)
            {
                trendChart.ChartAreas[0].AxisX.CustomLabels.Add(0, i * 2, key);
                i++;
            }

            // Add Chart title
            trendChart.Titles[0].Text = chartTitle;

            trendChart.Invalidate();
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

        // Click events
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void butNextDataSet_Click(object sender, EventArgs e)
        {
            UpdateTrendChartWithNextSeriesDataSet();
        }

        private void butPreviousDataSet_Click(object sender, EventArgs e)
        {
            UpdateTrendChartWithPreviousSeriesDataSet();
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
            //DialogResult result = folderBrowserDialog1.ShowDialog();

            //if (result == DialogResult.OK)
            //{
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
            // trendAnalysisController.GenerateWeekReport();
            trendAnalysisController.GenerateWeekChartReport();

            // Report Completion
            MessageBox.Show("Report Generation Complete.", "Report message");

            // Update TrendChart
            UpdateTrendChartWithFirstSeriesDataSet();
            //}            
        }

        private void CreateDateRangeForMonth()
        {
            int year = (int)cmbYear.SelectedItem;
            Month month = (Month)Enum.Parse(typeof(Month), cmbMonth.SelectedItem.ToString());
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


    }
}
