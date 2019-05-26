using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
//using System.Threading.Tasks;
using OfficeOpenXml;

namespace BrewingModel.Datasources
{
    public class WorkSheetSaver
    {
        XlPeriod period;
        //private IList<Task> saveTasks = new List<Task>();
        private ExcelPackage xlPackage;
        ExcelWorksheet xlRawDataWorksheet;
        ExcelWorksheet xlBrewingFormWorksheet;

        FileInfo fileInfo;

        public WorkSheetSaver(XlPeriod xlPeriod)
        {
            period = xlPeriod;
            this.fileInfo = xlPeriod.FileInfo;
            this.xlPackage = new ExcelPackage(xlPeriod.FileInfo);
            this.xlRawDataWorksheet = xlPackage.Workbook.Worksheets[xlPeriod.RawDataSheetName];
            this.xlBrewingFormWorksheet = xlPackage.Workbook.Worksheets[xlPeriod.BrewingFormSheetName];
        }

        public void AddBrewToWorkSheet(IBrew brew, int columnIndex)
        {
            Monitor.Enter(period);

            period.AddBrewToWorkSheet(brew, columnIndex);

            Monitor.Exit(period);
        }

        public void LoadBrewsFromWorkSheet()
        {
            Monitor.Enter(period);

            period.LoadBrewsFromWorkSheet();

            Monitor.Exit(period);
        }

    }
}
