using System;
using System.Collections.Generic;
using System.IO;
using BrewingModel.Settings;
using OfficeOpenXml;
using ProcessEquipmentParameters;
using ProcessFields.ProcessDurations;
using Util;

namespace BrewingModel.Datasources
{
    public class XlPeriod : Period
    {
        /// An abstraction representing the period in a datasource
        /// Period corresponds to a calender month
        /// It is associated with a workbook
        /// The workbook contains a brews process in the period

        ExcelPackage xlPackage;
        ExcelWorksheet xlRawDataWorksheet;
        ExcelWorksheet xlBrewingFormWorksheet;

        FileInfo fileInfo;
        private const string rawDataSheetName = "Raw data";
        private const string brewingFormSheetName = "Brewing forms";
        MyAppSettings appSettings = MyAppSettings.GetInstance();
        private string fileServerPath;
        private string folderSeparator;

        static WorkSheetSaver workSheetSaver;

        public string RawDataSheetName
        {
            get
            {
                return rawDataSheetName;
            }
        }

        public string BrewingFormSheetName
        {
            get
            {
                return brewingFormSheetName;
            }
        }

        public FileInfo FileInfo
        {
            get
            {
                return fileInfo;
            }
        }

        public ExcelWorksheet XlBrewingFormWorksheet
        {
            get
            {
                return xlBrewingFormWorksheet;
            }
        }

        public ExcelWorksheet XlRawDataWorksheet
        {
            get
            {
                return xlRawDataWorksheet;
            }
        }

        public XlPeriod(string year, Month month, string connectionString) : this(year, month.ToString(), connectionString)
        {
        }

        public XlPeriod(string year, string month, string connectionString)
        {
            fileServerPath = appSettings.FileServerPath;
            folderSeparator = appSettings.FolderSeparator;
            this.year = year;
            this.month = month;
            this.periodName = year + "-" + month;
            this.fileInfo = new FileInfo(connectionString + folderSeparator + year + folderSeparator + month + ".xlsx");
            xlPackage = new ExcelPackage(fileInfo);
            xlRawDataWorksheet = xlPackage.Workbook.Worksheets[rawDataSheetName];
            xlBrewingFormWorksheet = xlPackage.Workbook.Worksheets[brewingFormSheetName];

            workSheetSaver = new WorkSheetSaver(this);

        }

        public override void AddBrew(IBrew brew)
        {
            if (!_brews.ContainsKey(brew.BrewNumber + " - " + brew.StartDate)) // && !BrewInWorkSheet(brew))
            {             
                int nextColumnIndex = GetNextColumnIndex();
                //int newColumnIndex = _brews.Count + 3;
                //AddBrewToWorkSheet(brew, newColumnIndex);
                workSheetSaver.AddBrewToWorkSheet(brew, nextColumnIndex);
                _brews.Add(brew.BrewNumber + " - " + brew.StartDate, brew);
            }
        }

        private int GetNextColumnIndex()
        {
            int nextColumn = 3;
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet brewingFormWorksheet = xlPackage.Workbook.Worksheets[brewingFormSheetName];

                for (int column = 3; column <= brewingFormWorksheet.Dimension.End.Column; column++)

                {
                    if (brewingFormWorksheet.Cells[4, column].Value != null)
                    {
                        nextColumn++;
                    }
                }
            }

            return nextColumn;
        }

        public override IBrew GetBrew(string brewNumber)
        {
            if (_brews.ContainsKey(brewNumber)) // && !BrewInWorkSheet(brew))
            {
                return _brews[brewNumber];
            }
            else 
            {
                return new BrewProxy();
            }
        }

        public override IBrew GetBrewWithProcessParameters(IBrew brew)
        {
            return GetBrewParametersFromWorkSheet(brew);
        }

        public override void LoadBrews()
        {
            workSheetSaver.LoadBrewsFromWorkSheet();
        }

        public override void RemoveBrew(IBrew brew)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBrew(IBrew brew)
        {
            if (_brews.ContainsKey(brew.BrewNumber)) // && !BrewInWorkSheet(brew))
            {

                int columnIndex = GetColumnNumber(brew);

                workSheetSaver.AddBrewToWorkSheet(brew, columnIndex);
                _brews[brew.BrewNumber] = brew;
            }
        }


        // Worksheet methods
        internal void AddBrewToWorkSheet(IBrew brew, int columnIndex)
        {
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet brewingFormWorksheet = xlPackage.Workbook.Worksheets[brewingFormSheetName];

                SetBrewingFormData(brew, columnIndex);
                SetRawData(brew, columnIndex);
            }
        }

        private void SetRawData(IBrew brew, int newColumnIndex)
        {
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet rawDataWorksheet = xlPackage.Workbook.Worksheets[rawDataSheetName];
                // Set Headers
                rawDataWorksheet.Cells[1, newColumnIndex].Value = "";
                rawDataWorksheet.Cells[2, newColumnIndex].Value = brew.StartDate;
                rawDataWorksheet.Cells[3, newColumnIndex].Value = brew.BrandName;
                rawDataWorksheet.Cells[4, newColumnIndex].Value = brew.BrewNumber;
                rawDataWorksheet.Cells[5, newColumnIndex].Value = brew.StartTime;

                // Set process parameter values
                // Mash Copper process parameters
                rawDataWorksheet.Cells[6, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString());
                rawDataWorksheet.Cells[7, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInEndTime.ToString());
                rawDataWorksheet.Cells[8, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.ProteinRestEndTime.ToString());
                rawDataWorksheet.Cells[9, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp1EndTime.ToString());
                rawDataWorksheet.Cells[10, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.Rest1EndTime.ToString());
                rawDataWorksheet.Cells[11, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp2EndTime.ToString());
                rawDataWorksheet.Cells[12, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.Rest2EndTime.ToString());
                rawDataWorksheet.Cells[13, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.TransferToMtEndTime.ToString());
                rawDataWorksheet.Cells[14, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.ProteinRestTemperature.ToString());
                rawDataWorksheet.Cells[15, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp1Temperature.ToString());
                rawDataWorksheet.Cells[16, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp2Temperature.ToString());

                // Mash Tun process parameters
                rawDataWorksheet.Cells[17, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.MashingInStartTime.ToString());
                rawDataWorksheet.Cells[18, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.MashingInEndTime.ToString());
                rawDataWorksheet.Cells[19, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.ProteinRestEndTime.ToString());
                rawDataWorksheet.Cells[20, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.SacharificationRestEndTime.ToString());
                rawDataWorksheet.Cells[21, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.HeatingUpEndTime.ToString());
                rawDataWorksheet.Cells[22, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.MashTunReadyAt.ToString());
                rawDataWorksheet.Cells[23, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.ProteinRestTemperature.ToString());
                rawDataWorksheet.Cells[24, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.SacharificationRestTemperature.ToString());
                rawDataWorksheet.Cells[25, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.HeatingUpTemperature.ToString());

                // Mash Filter process parameters
                rawDataWorksheet.Cells[26, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.PrefillingStartTime.ToString());
                rawDataWorksheet.Cells[27, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.PrefillingEndTime.ToString());
                rawDataWorksheet.Cells[28, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.WeakWortTransferEndTime.ToString());
                rawDataWorksheet.Cells[29, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.MainMashFiltrationEndTime.ToString());
                rawDataWorksheet.Cells[30, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.SpargingEndTime.ToString());
                rawDataWorksheet.Cells[31, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.SpargingToWWTEndTime.ToString());
                rawDataWorksheet.Cells[32, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.ExtraSpargingEndTime.ToString());
                rawDataWorksheet.Cells[33, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.DrippingEndTime.ToString());
                rawDataWorksheet.Cells[34, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.SpentGrainDischargeEndTime.ToString());
                rawDataWorksheet.Cells[35, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.ReadyAtTime.ToString());

                // Holding Vessel process parameters
                rawDataWorksheet.Cells[36, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.HoldingVessel, HoldingVesselProcessParameters.FillingStartTime.ToString());
                rawDataWorksheet.Cells[37, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.HoldingVessel, HoldingVesselProcessParameters.TransferToWcEndTime.ToString());
                rawDataWorksheet.Cells[38, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.HoldingVessel, HoldingVesselProcessParameters.EmptyAtTime.ToString());

                // Wort Copper process parameters
                rawDataWorksheet.Cells[39, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.HeatingStartTime.ToString());
                rawDataWorksheet.Cells[40, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.HeatingEndTime.ToString());
                rawDataWorksheet.Cells[41, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.BoilingEndTime.ToString());
                rawDataWorksheet.Cells[42, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.ExtraBoilingEndTime.ToString());
                rawDataWorksheet.Cells[43, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.CastingStartTime.ToString());
                rawDataWorksheet.Cells[44, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.CastingEndTime.ToString());
                rawDataWorksheet.Cells[45, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeBeforeBoiling.ToString());
                rawDataWorksheet.Cells[46, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeAfterBoiling.ToString());

                // Whirpool process parameters
                rawDataWorksheet.Cells[47, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.CastingStartTime.ToString());
                rawDataWorksheet.Cells[48, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.CastingEndTime.ToString());
                rawDataWorksheet.Cells[49, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.RestingEndTime.ToString());
                rawDataWorksheet.Cells[50, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.CoolingEndTime.ToString());
                rawDataWorksheet.Cells[51, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.ReadyAtTime.ToString());

                SaveWorkSheet(xlPackage);

            }
        }

        internal IList<IDictionary<string, string>> GetExistingBrewNumbers()
        {
            IList<IDictionary<string, string>> existingBrewNumbers = new List<IDictionary<string, string>>();

            IDictionary<string, string> brewNumberAndDate = new Dictionary<string, string>();

            using (xlPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet brewingFormWorksheet = xlPackage.Workbook.Worksheets[brewingFormSheetName];

                for (int column = 3; column <= brewingFormWorksheet.Dimension.End.Column; column++)

                {
                    if (brewingFormWorksheet.Cells[4, column].Value != null)
                    {
                        brewNumberAndDate = new Dictionary<string, string>();
                        brewNumberAndDate.Add("BrewNumber", brewingFormWorksheet.Cells[4, column].Value.ToString());
                        brewNumberAndDate.Add("StartDate", brewingFormWorksheet.Cells[2, column].Value.ToString());
                        existingBrewNumbers.Add(brewNumberAndDate);
                    }
                }
            }

            return existingBrewNumbers;
        }

        private void SetBrewingFormData(IBrew brew, int newColumnIndex)
        {

            using (xlPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet brewingFormWorksheet = xlPackage.Workbook.Worksheets[brewingFormSheetName];

                // Set Headers
                brewingFormWorksheet.Cells[1, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[2, newColumnIndex].Value = brew.StartDate;
                brewingFormWorksheet.Cells[3, newColumnIndex].Value = brew.BrandName;
                brewingFormWorksheet.Cells[4, newColumnIndex].Value = brew.BrewNumber;
                brewingFormWorksheet.Cells[5, newColumnIndex].Value = brew.StartTime;

                // Set process parameter values
                // Mash Copper process parameters
                brewingFormWorksheet.Cells[6, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashCopperProcessDurations()[MashCopperProcessDurations.MashingInDuration.ToString()]);
                brewingFormWorksheet.Cells[7, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[8, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.ProteinRestTemperature.ToString());
                brewingFormWorksheet.Cells[9, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashCopperProcessDurations()[MashCopperProcessDurations.ProteinRestDuration.ToString()]);
                brewingFormWorksheet.Cells[10, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashCopperProcessDurations()[MashCopperProcessDurations.HeatingUp1Duration.ToString()]);
                brewingFormWorksheet.Cells[11, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp1Temperature.ToString());
                brewingFormWorksheet.Cells[12, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashCopperProcessDurations()[MashCopperProcessDurations.Rest1Duration.ToString()]);
                brewingFormWorksheet.Cells[13, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashCopperProcessDurations()[MashCopperProcessDurations.HeatingUp2Duration.ToString()]);
                brewingFormWorksheet.Cells[14, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp2Temperature.ToString());
                brewingFormWorksheet.Cells[15, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashCopperProcessDurations()[MashCopperProcessDurations.Rest2Duration.ToString()]);

                // Mash Tun process parameters
                brewingFormWorksheet.Cells[16, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashTunProcessDurations()[MashTunProcessDurations.MashingInDuration.ToString()]);
                brewingFormWorksheet.Cells[17, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[18, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.ProteinRestTemperature.ToString());
                brewingFormWorksheet.Cells[19, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashTunProcessDurations()[MashTunProcessDurations.ProteinRestDuration.ToString()]);
                brewingFormWorksheet.Cells[20, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashTunProcessDurations()[MashTunProcessDurations.SaccharificationDuration.ToString()]);
                brewingFormWorksheet.Cells[21, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[22, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.SacharificationRestTemperature.ToString());
                brewingFormWorksheet.Cells[23, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashTunProcessDurations()[MashTunProcessDurations.HeatingUpDuration.ToString()]);
                brewingFormWorksheet.Cells[24, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.HeatingUpTemperature.ToString());
                brewingFormWorksheet.Cells[25, newColumnIndex].Value = "";

                // Mash Filter process parameters
                brewingFormWorksheet.Cells[26, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashFilterProcessDurations()[MashFilterProcessDurations.MainWortFiltrationDuration.ToString()]);
                brewingFormWorksheet.Cells[27, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashFilterProcessDurations()[MashFilterProcessDurations.SpargingRestDuration.ToString()]);
                brewingFormWorksheet.Cells[28, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetMashFilterProcessDurations()[MashFilterProcessDurations.TotalFiltrationDuration.ToString()]);

                // Wort Copper process parameters
                brewingFormWorksheet.Cells[29, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetWortCopperProcessDurations()[WortCopperProcessDurations.HeatToBoilDuration.ToString()]);
                brewingFormWorksheet.Cells[30, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeBeforeBoiling.ToString());
                brewingFormWorksheet.Cells[31, newColumnIndex].Value = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeAfterBoiling.ToString());
                brewingFormWorksheet.Cells[32, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[33, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetWortCopperProcessDurations()[WortCopperProcessDurations.BoilingDuration.ToString()]);

                // Whirpool process parameters
                brewingFormWorksheet.Cells[34, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetWhirlpoolProcessDurations()[WhirlpoolProcessDurations.CastingDuration.ToString()]);
                brewingFormWorksheet.Cells[35, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetWhirlpoolProcessDurations()[WhirlpoolProcessDurations.RestDuration.ToString()]);
                brewingFormWorksheet.Cells[36, newColumnIndex].Value = GetTimeValueOrBlank(brew.GetWhirlpoolProcessDurations()[WhirlpoolProcessDurations.CoolingDuration.ToString()]);
                brewingFormWorksheet.Cells[37, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[38, newColumnIndex].Value = "";
                brewingFormWorksheet.Cells[39, newColumnIndex].Value = "";

                SetNumberFormats(brewingFormWorksheet, newColumnIndex);

                SaveWorkSheet(xlPackage);

            }
        }

        private object GetTimeValueOrBlank(TimeSpan t)
        {
            if(t == TimeSpan.Zero)
            {
                return "";
            }
            else
            {
                return t;
            }
        }

        private void SetNumberFormats(ExcelWorksheet brewingFormWorksheet, int newColumnIndex)
        {
            string durationFormat = "hh:mm:ss";

            // Set Headers
            //brewingFormWorksheet.Cells[1, newColumnIndex].Style.Numberformat.Format = "";
            //brewingFormWorksheet.Cells[2, newColumnIndex].Style.Numberformat.Format = brew.StartDate;
            //brewingFormWorksheet.Cells[3, newColumnIndex].Style.Numberformat.Format = brew.BrandName;
            //brewingFormWorksheet.Cells[4, newColumnIndex].Style.Numberformat.Format = brew.BrewNumber;
            //brewingFormWorksheet.Cells[5, newColumnIndex].Style.Numberformat.Format = brew.StartTime;

            // Set process parameter values
            // Mash Copper process parameters
            brewingFormWorksheet.Cells[6, newColumnIndex].Style.Numberformat.Format = durationFormat;
            //brewingFormWorksheet.Cells[7, newColumnIndex].Style.Numberformat.Format = "";
            //brewingFormWorksheet.Cells[8, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.ProteinRestTemperature.ToString());
            brewingFormWorksheet.Cells[9, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[10, newColumnIndex].Style.Numberformat.Format = durationFormat;
            //brewingFormWorksheet.Cells[11, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp1Temperature.ToString());
            brewingFormWorksheet.Cells[12, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[13, newColumnIndex].Style.Numberformat.Format = durationFormat;
            //brewingFormWorksheet.Cells[14, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp2Temperature.ToString());
            brewingFormWorksheet.Cells[15, newColumnIndex].Style.Numberformat.Format = durationFormat;

            // Mash Tun process parameters
            brewingFormWorksheet.Cells[16, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[17, newColumnIndex].Style.Numberformat.Format = "";
            //brewingFormWorksheet.Cells[18, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.ProteinRestTemperature.ToString());
            brewingFormWorksheet.Cells[19, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[20, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[21, newColumnIndex].Style.Numberformat.Format = "";
            //brewingFormWorksheet.Cells[22, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.SacharificationRestTemperature.ToString());
            brewingFormWorksheet.Cells[23, newColumnIndex].Style.Numberformat.Format = durationFormat;
            //brewingFormWorksheet.Cells[24, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.HeatingUpTemperature.ToString());
            brewingFormWorksheet.Cells[25, newColumnIndex].Style.Numberformat.Format = "";

            // Mash Filter process parameters
            brewingFormWorksheet.Cells[26, newColumnIndex].Style.Numberformat.Format = durationFormat;
            //brewingFormWorksheet.Cells[27, newColumnIndex].Style.Numberformat.Format = brew.GetMashFilterProcessDurations()[MashFilterProcessDurations.SpargingRestDuration.ToString()];
            brewingFormWorksheet.Cells[28, newColumnIndex].Style.Numberformat.Format = durationFormat;

            // Wort Copper process parameters
            brewingFormWorksheet.Cells[29, newColumnIndex].Style.Numberformat.Format = durationFormat;
            //brewingFormWorksheet.Cells[30, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeBeforeBoiling.ToString());
            //brewingFormWorksheet.Cells[31, newColumnIndex].Style.Numberformat.Format = brew.GetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeAfterBoiling.ToString());
            brewingFormWorksheet.Cells[32, newColumnIndex].Style.Numberformat.Format = "";
            brewingFormWorksheet.Cells[33, newColumnIndex].Style.Numberformat.Format = durationFormat;

            // Whirpool process parameters
            brewingFormWorksheet.Cells[34, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[35, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[36, newColumnIndex].Style.Numberformat.Format = durationFormat;
            brewingFormWorksheet.Cells[37, newColumnIndex].Style.Numberformat.Format = "";
            brewingFormWorksheet.Cells[38, newColumnIndex].Style.Numberformat.Format = "";
            brewingFormWorksheet.Cells[39, newColumnIndex].Style.Numberformat.Format = "";
        }

        private void SaveWorkSheet(ExcelPackage xlPackage)
        {
            using (xlPackage)
            {
                Byte[] bin = xlPackage.GetAsByteArray();

                FileInfo file = fileInfo;
                File.WriteAllBytes(file.FullName, bin);
                //return file.FullName;
            }
        }

        private int GetColumnNumber(IBrew brew)
        {
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                xlRawDataWorksheet = xlPackage.Workbook.Worksheets[rawDataSheetName];

                //for (int column = 3; column <= xlRawDataWorksheet.Dimension.Columns; column++ )
                for (int column = 3; column <= xlRawDataWorksheet.Dimension.End.Column; column++)

                    {
                        if (xlRawDataWorksheet.Cells[4, column].Value != null && brew.BrewNumber == xlRawDataWorksheet.Cells[4, column].Value.ToString())
                    {
                        return column; 
                    }
                }
                return 0;
            }
        }
        
        //public int GetBrewingFormSheetColumnNumber(IBrew brew)
        //{
        //    using (xlPackage = new ExcelPackage(fileInfo))
        //    {
        //        xlBrewingFormWorksheet = xlPackage.Workbook.Worksheets[brewingFormSheetName];

        //        for (int column = 3; column <= xlBrewingFormWorksheet.Dimension.Columns; column++)
        //        {
        //            if (xlBrewingFormWorksheet.Cells[4, column].Value != null && brew.BrewNumber == xlBrewingFormWorksheet.Cells[4, column].Value.ToString())
        //            {
        //                return column; 
        //            }
        //        }
        //        return 0;
        //    }
        //}

        private Brew GetBrewParametersFromWorkSheet(IBrew brew)
        {
            int columnIndex = GetColumnNumber(brew);
            Brew brewWithParameters = new Brew();
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                xlRawDataWorksheet = xlPackage.Workbook.Worksheets[rawDataSheetName];

                if(columnIndex > 0)
                {
                    ExcelWorksheet rawDataWorksheet = xlPackage.Workbook.Worksheets[rawDataSheetName];
                    // Get Headers
                    string startDate = rawDataWorksheet.Cells[2, columnIndex].Value.ToString();
                    string brandName = rawDataWorksheet.Cells[3, columnIndex].Value.ToString();
                    string brewNumber = rawDataWorksheet.Cells[4, columnIndex].Value.ToString();
                    string startTime = rawDataWorksheet.Cells[5, columnIndex].Value.ToString();

                    brewWithParameters = new Brew(startDate, brandName, brewNumber);
                    //brew.StartTime = startTime;

                    // Get process parameter values
                    // Mash Copper process parameters
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString(), rawDataWorksheet.Cells[6, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInEndTime.ToString(), rawDataWorksheet.Cells[7, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.ProteinRestEndTime.ToString(), rawDataWorksheet.Cells[8, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp1EndTime.ToString(), rawDataWorksheet.Cells[9, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.Rest1EndTime.ToString(), rawDataWorksheet.Cells[10, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp2EndTime.ToString(), rawDataWorksheet.Cells[11, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.Rest2EndTime.ToString(), rawDataWorksheet.Cells[12, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.TransferToMtEndTime.ToString(), rawDataWorksheet.Cells[13, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.ProteinRestTemperature.ToString(), rawDataWorksheet.Cells[14, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp1Temperature.ToString(), rawDataWorksheet.Cells[15, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.HeatingUp2Temperature.ToString(), rawDataWorksheet.Cells[16, columnIndex].Value.ToString());

                    // Mash Tun process parameters
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.MashingInStartTime.ToString(), rawDataWorksheet.Cells[17, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.MashingInEndTime.ToString(), rawDataWorksheet.Cells[18, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.ProteinRestEndTime.ToString(), rawDataWorksheet.Cells[19, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.SacharificationRestEndTime.ToString(), rawDataWorksheet.Cells[20, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.HeatingUpEndTime.ToString(), rawDataWorksheet.Cells[21, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.MashTunReadyAt.ToString(), rawDataWorksheet.Cells[22, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.ProteinRestTemperature.ToString(), rawDataWorksheet.Cells[23, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.SacharificationRestTemperature.ToString(), rawDataWorksheet.Cells[24, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashTun, MashTunProcessParameters.HeatingUpTemperature.ToString(), rawDataWorksheet.Cells[25, columnIndex].Value.ToString());

                    // Mash Filter process parameters
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.PrefillingStartTime.ToString(), rawDataWorksheet.Cells[26, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.PrefillingEndTime.ToString(), rawDataWorksheet.Cells[27, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.WeakWortTransferEndTime.ToString(), rawDataWorksheet.Cells[28, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.MainMashFiltrationEndTime.ToString(), rawDataWorksheet.Cells[29, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.SpargingEndTime.ToString(), rawDataWorksheet.Cells[30, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.SpargingToWWTEndTime.ToString(), rawDataWorksheet.Cells[31, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.ExtraSpargingEndTime.ToString(), rawDataWorksheet.Cells[32, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.DrippingEndTime.ToString(), rawDataWorksheet.Cells[33, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.SpentGrainDischargeEndTime.ToString(), rawDataWorksheet.Cells[34, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.MashFilter, MashFilterProcessParameters.ReadyAtTime.ToString(), rawDataWorksheet.Cells[35, columnIndex].Value.ToString());

                    // Holding Vessel process parameters
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.HoldingVessel, HoldingVesselProcessParameters.FillingStartTime.ToString(), rawDataWorksheet.Cells[36, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.HoldingVessel, HoldingVesselProcessParameters.TransferToWcEndTime.ToString(), rawDataWorksheet.Cells[37, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.HoldingVessel, HoldingVesselProcessParameters.EmptyAtTime.ToString(), rawDataWorksheet.Cells[38, columnIndex].Value.ToString());

                    // Wort Copper process parameters
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.HeatingStartTime.ToString(), rawDataWorksheet.Cells[39, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.HeatingStartTime.ToString(), rawDataWorksheet.Cells[40, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.BoilingEndTime.ToString(), rawDataWorksheet.Cells[41, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.ExtraBoilingEndTime.ToString(), rawDataWorksheet.Cells[42, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.CastingStartTime.ToString(), rawDataWorksheet.Cells[43, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.CastingEndTime.ToString(), rawDataWorksheet.Cells[44, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeBeforeBoiling.ToString(), rawDataWorksheet.Cells[45, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.WortCopper, WortCopperProcessParameters.VolumeAfterBoiling.ToString(), rawDataWorksheet.Cells[46, columnIndex].Value.ToString());

                    // Whirpool process parameters
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.CastingStartTime.ToString(), rawDataWorksheet.Cells[47, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.CastingEndTime.ToString(), rawDataWorksheet.Cells[48, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.RestingEndTime.ToString(), rawDataWorksheet.Cells[49, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.CoolingEndTime.ToString(), rawDataWorksheet.Cells[50, columnIndex].Value.ToString());
                    brewWithParameters.SetProcessParameterValue(ProcessEquipment.Whirlpool, WhirlpoolProcessParameters.ReadyAtTime.ToString(), rawDataWorksheet.Cells[51, columnIndex].Value.ToString());


                }

                return brewWithParameters;
            }
        }

        internal void LoadBrewsFromWorkSheet()
        {
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                if (xlPackage.Workbook.Worksheets[rawDataSheetName] != null)
                {
                    xlRawDataWorksheet = xlPackage.Workbook.Worksheets[rawDataSheetName];

                    string brewDate;
                    string brandname;
                    string brewNumber;
                    string startTime;

                    int column;
                    _brews.Clear();

                    //for (int columnIndex = 3; columnIndex <= xlRawDataWorksheet.Dimension.Columns; columnIndex++)
                    for (int columnIndex = 3; columnIndex <= xlRawDataWorksheet.Dimension.End.Column; columnIndex++)
                    {
                        column = columnIndex;
                        if (xlRawDataWorksheet.Cells[4, column].Value != null)
                        {
                            brewDate = xlRawDataWorksheet.Cells[2, column].Value.ToString();
                            brandname = xlRawDataWorksheet.Cells[3, column].Value.ToString();
                            brewNumber = xlRawDataWorksheet.Cells[4, column].Value.ToString();
                            startTime = xlRawDataWorksheet.Cells[5, column].Value.ToString();

                            IBrew brew = new BrewProxy(brewDate, brandname, brewNumber);

                            _brews.Add(brew.BrewNumber + " - " + brew.StartDate, brew);
                        }

                    }
                }
            }
        }

    }
}