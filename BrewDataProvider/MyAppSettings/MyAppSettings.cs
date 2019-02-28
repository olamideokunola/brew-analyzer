using System;
using System.Configuration;

namespace BrewingModel.Settings
{
    public class MyAppSettings
    {
        AppSettingsReader reader = new AppSettingsReader();

        //Singleton
        private static MyAppSettings _uniqueInstance = null;

        //lazy construction of instance
        public static MyAppSettings GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new MyAppSettings();
            }

            return _uniqueInstance;
        }

        private MyAppSettings()
        {
        }

        public string FileServerPath
        {
            get
            {
                string fileServerPath = (string)reader.GetValue("FileServerPath", typeof(string));
                return fileServerPath;
            }
        }

        public string ConnectionString
        {
            get
            {
                string connectionString = (string)reader.GetValue("ConnectionString", typeof(string));
                return connectionString;
            }
        }

        public string PeriodTemplateFilePath
        {
            get
            {
                string periodTemplateFilePath = (string)reader.GetValue("PeriodTemplateFilePath", typeof(string));
                return periodTemplateFilePath;
            }
        }

        public string ReportTemplateFilePath
        {
            get
            {
                string reportTemplateFilePath = (string)reader.GetValue("ReportTemplateFilePath", typeof(string));
                return reportTemplateFilePath;
            }
        }

        public string FolderSeparator
        {
            get
            {
                string folderSeparator = (string)reader.GetValue("FolderSeparator", typeof(string));
                return folderSeparator;
            }
        }
    }
}
