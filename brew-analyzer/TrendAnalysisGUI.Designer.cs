﻿namespace brew_analyzer
{
    partial class TrendAnalysisGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSetDates = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblSetDates = new System.Windows.Forms.Label();
            this.lstBoxBrews = new System.Windows.Forms.ListBox();
            this.lblBrewsListBox = new System.Windows.Forms.Label();
            this.lblSelectedBrew = new System.Windows.Forms.Label();
            this.txtSelectedBrew = new System.Windows.Forms.TextBox();
            this.btnCreateTrendChart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSetDates
            // 
            this.btnSetDates.Location = new System.Drawing.Point(100, 123);
            this.btnSetDates.Name = "btnSetDates";
            this.btnSetDates.Size = new System.Drawing.Size(129, 23);
            this.btnSetDates.TabIndex = 0;
            this.btnSetDates.Text = "Set Dates";
            this.btnSetDates.UseVisualStyleBackColor = true;
            this.btnSetDates.Click += new System.EventHandler(this.SetDate_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(100, 62);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(100, 91);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 2;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(27, 66);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 3;
            this.lblStartDate.Text = "Start Date";
            this.lblStartDate.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(27, 94);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "End Date";
            // 
            // lblSetDates
            // 
            this.lblSetDates.AutoSize = true;
            this.lblSetDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetDates.Location = new System.Drawing.Point(27, 26);
            this.lblSetDates.Name = "lblSetDates";
            this.lblSetDates.Size = new System.Drawing.Size(146, 20);
            this.lblSetDates.TabIndex = 5;
            this.lblSetDates.Text = "Set Trend Dates:";
            this.lblSetDates.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // lstBoxBrews
            // 
            this.lstBoxBrews.FormattingEnabled = true;
            this.lstBoxBrews.Location = new System.Drawing.Point(100, 210);
            this.lstBoxBrews.Name = "lstBoxBrews";
            this.lstBoxBrews.Size = new System.Drawing.Size(200, 95);
            this.lstBoxBrews.TabIndex = 6;
            this.lstBoxBrews.SelectedIndexChanged += new System.EventHandler(this.lstBoxBrews_SelectedIndexChanged);
            // 
            // lblBrewsListBox
            // 
            this.lblBrewsListBox.AutoSize = true;
            this.lblBrewsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrewsListBox.Location = new System.Drawing.Point(97, 191);
            this.lblBrewsListBox.Name = "lblBrewsListBox";
            this.lblBrewsListBox.Size = new System.Drawing.Size(101, 13);
            this.lblBrewsListBox.TabIndex = 7;
            this.lblBrewsListBox.Text = "Available Brews:";
            this.lblBrewsListBox.Click += new System.EventHandler(this.lblBrewsListBox_Click);
            // 
            // lblSelectedBrew
            // 
            this.lblSelectedBrew.AutoSize = true;
            this.lblSelectedBrew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedBrew.Location = new System.Drawing.Point(97, 318);
            this.lblSelectedBrew.Name = "lblSelectedBrew";
            this.lblSelectedBrew.Size = new System.Drawing.Size(93, 13);
            this.lblSelectedBrew.TabIndex = 8;
            this.lblSelectedBrew.Text = "Selected Brew:";
            // 
            // txtSelectedBrew
            // 
            this.txtSelectedBrew.Location = new System.Drawing.Point(100, 337);
            this.txtSelectedBrew.Name = "txtSelectedBrew";
            this.txtSelectedBrew.Size = new System.Drawing.Size(199, 20);
            this.txtSelectedBrew.TabIndex = 9;
            // 
            // btnCreateTrendChart
            // 
            this.btnCreateTrendChart.Location = new System.Drawing.Point(100, 374);
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
            this.label1.Location = new System.Drawing.Point(27, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Brew from list:";
            // 
            // TrendAnalysisGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateTrendChart);
            this.Controls.Add(this.txtSelectedBrew);
            this.Controls.Add(this.lblSelectedBrew);
            this.Controls.Add(this.lblBrewsListBox);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        

        private System.Windows.Forms.Button btnSetDates;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblSetDates;
        private System.Windows.Forms.ListBox lstBoxBrews;
        private System.Windows.Forms.Label lblBrewsListBox;
        private System.Windows.Forms.Label lblSelectedBrew;
        private System.Windows.Forms.TextBox txtSelectedBrew;
        private System.Windows.Forms.Button btnCreateTrendChart;
        private System.Windows.Forms.Label label1;
    }
}

