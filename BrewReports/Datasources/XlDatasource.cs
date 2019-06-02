using System;
using System.Collections.Generic;
using System.IO;
using BrewingModel;
using OfficeOpenXml;
using Util;
using System.Threading;

namespace BrewingModel.Datasources
{
    public class XlDatasource : Datasource
    {

        private ExcelPackage xlExcelPackage;

        public XlDatasource(string connectionString, string templatePath)
        {
            //template = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}period_template.xlsx");
            this.template = new FileInfo(templatePath);
            this.connectionString = connectionString;
            //this.connectionString = $"{AppDomain.CurrentDomain.BaseDirectory}";
        }

        public XlDatasource()
        {
        }

        public override void AddPeriod(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(xlPeriod.FileInfo))
            {
                if (!periods.ContainsKey(period.PeriodName))
                {
                    CreateNewPeriodWorkBook(period);
                    periods.Add(period.PeriodName, period);
                }
            }
        }

        public override Period CreatePeriod(IBrew brew)
        {
            if (brew.Year != "" && brew.Month != "")
            { 
                string year = brew.Year;
                string month = brew.Month;

                return CreatePeriod(year, month);
            }
            else
            {
                return null;
            }
        }

        public override Period CreatePeriod(string year, Month month)
        {
            Period period = new XlPeriod(year, month.ToString(), this.connectionString);
            return period;
        }

        public override Period CreatePeriod(string year, string month)
        {
            Period period = new XlPeriod(year, month, this.connectionString);
            return period;
        }

        public override void DeletePeriod(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(xlPeriod.FileInfo))
            {
                if (periods.ContainsKey(period.PeriodName) && xlPeriod.FileInfo.Exists)
                {
                    DeletePeriodWorkBook(period);
                    periods.Remove(period.PeriodName);
                }
            }
        }

        private void DeletePeriodWorkBook(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(xlPeriod.FileInfo))
            {
                Byte[] bin = xlExcelPackage.GetAsByteArray();

                FileInfo file = xlPeriod.FileInfo;
                File.Delete(file.FullName);
            }
        }

        // TODO: Create LoadPeriod for lazy loading
        public override Period LoadPeriod(string year, string month)
        {
            Period period = new XlPeriod(year, month, connectionString);
            period.LoadBrews();
            string periodName = year + "-" + month;
            if (!periods.ContainsKey(periodName))
            {
                periods.Add(periodName, period);
            }
            else
            {
                periods[periodName] = period;
            }
            return period;
        }

        public override void LoadPeriods()
        {
            DirectoryInfo dir = new DirectoryInfo(ConnectionString);

            // Get year directories
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                string year = subDir.Name;
                // Get months in the directory
                foreach (FileInfo file in subDir.GetFiles())
                {
                    int startIndex = file.Name.LastIndexOf(file.Extension, StringComparison.CurrentCulture);
                    string month = file.Name.Remove(startIndex);
                    Period period = new XlPeriod(year, month, connectionString);
                    period.LoadBrews();
                    string periodName = year + "-" + month;
                    if(!periods.ContainsKey(periodName))
                    {
                        periods.Add(periodName, period);
                    }
                    else
                    {
                        periods[periodName] = period;
                    }
                }
            }
        }

        public override void UpdatePeriod(Period period)
        {
            throw new NotImplementedException();
        }

        private string CreateNewPeriodWorkBook(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(template, true))
            {
                Byte[] bin = xlExcelPackage.GetAsByteArray();

                FileInfo file = xlPeriod.FileInfo;
                // if year folder does not exist create it
                CheckOrCreatePeriodFolder(period);
                File.WriteAllBytes(file.FullName, bin);
                return file.FullName;
            }
        }

        private void CheckOrCreatePeriodFolder(Period period)
        {
            DirectoryInfo dir = new DirectoryInfo(ConnectionString);
            DirectoryInfo yearDir = GetPeriodFolder(period);
            // if year folder does not exist, create it
            if (yearDir == null)
            {
                dir.CreateSubdirectory(period.Year);
            }
        }

        private DirectoryInfo GetPeriodFolder(Period period)
        {
            DirectoryInfo dir = new DirectoryInfo(ConnectionString);
            // Check year directories
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                if(subDir.Name == period.Year)
                {
                    return subDir;
                }
            }
            return null;
        }

        public override IBrew GetBrewWithProcessParameters(IBrew brew)
        {
            using (xlExcelPackage = new ExcelPackage(template, true))
            {
                string brewNumber = brew.BrewNumber;
                string startDate = brew.StartDate;

                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                string year = stringDateWorker.GetYear(startDate);
                string month = stringDateWorker.GetMonth(startDate);

                Period period = CreatePeriod(year, month);

                return period.GetBrewWithProcessParameters(brew);
            }
        }

        public override Period GetPeriod(IBrew brew)
        {
            return GetPeriod(brew.Year, brew.Month);
        }

        public override Period GetPeriod(string year, Month month)
        {            
            return GetPeriod(year, month.ToString());
        }

        public Period GetPeriod(string year, string month)
        {
            string periodName = year + "-" + month;
            if (periods.ContainsKey(periodName))
            {
                //Period period = periods[periodName];
                Period period = LoadPeriod(year, month);
                period.LoadBrews();
                return period;
            }
            else
            {
                return null;
            }
        }

        // Thread safe SaveBrew
        public override string SaveBrew(IBrew brew)
        {
            Period period;
            // if brewnumber and start date are valid, then go ahead and save brew
            //if (brew.BrewNumber.Length > 0 && brew.BrandName.Length > 0 && brew.StartDate.Length > 0)
            if (brew.BrewNumber.Length > 0  && brew.StartDate.Length > 0)
                {
                period = GetPeriod(brew);
                // If period exists update or add brew to period
                if (period != null)
                {
                    // If existing period contains the key then update brew in period
                    if (period.Brews.ContainsKey(brew.BrewNumber + " - " + brew.StartDate))
                    {
                        period.UpdateBrew(brew);
                    }
                    // If existing period does not contains the key then create brew in period
                    else
                    {
                        period.AddBrew(brew);
                    }
                    return "Success";
                }
                // If period does not exists, create period, add it to period list & add brew to it
                else
                {
                    period = CreatePeriod(brew);
                    AddPeriod(period);
                    period.AddBrew(brew);
                    return "Success";
                }
            }
            // if brewnumber, brand and start date are not valid, retrun failure
            else
            {
                return "Failure";
            }
        }

        public override IList<IDictionary<string, string>> GetExistingBrewNumbers(Month month, int year)
        {
            XlPeriod period = (XlPeriod)GetPeriod(year.ToString(), month);
            if (period != null)
            {
                return period.GetExistingBrewNumbers();
            }

            return null;
        }
    }

}
