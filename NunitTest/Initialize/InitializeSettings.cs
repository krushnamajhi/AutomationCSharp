using Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportLibrary;
using RunTimeSettings;
using RunTimeSettings.ProjectSettingsModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest.Initialize
{
    public class InitializeSettings
    {
        Util util = new Util();


        public void InitializeResourcesObjects()
        {
            if (ConfigReader.IConfig == null)
            {
                ConfigReader._Build();
            }

            var testsettings = ConfigReader.IConfig.GetSection("TestSettings").Get<TestSettings>();
            TestRunConstants.ResourceFolder = testsettings.ResourceFolder.getPath();
            TestRunConstants.TestDataFolder = testsettings.TestDataFolder.getPath();

        }


        public void InitialiseVariables()
        {
            if (ConfigReader.IConfig == null)
            {
                ConfigReader._Build();
            }

            var driverSettings = ConfigReader.IConfig.GetSection("DriverSettings").Get<Driversettings>();
            var testsettings = ConfigReader.IConfig.GetSection("TestSettings").Get<TestSettings>();
            var reportsettings = ConfigReader.IConfig.GetSection("ReportSettings").Get<Reportsettings>();
            TestRunConstants.Environment = ConfigReader.IConfig.GetValue<String>("Environment");

            DriverConstants.DriverFolder = driverSettings.DriverFolder.getPath();
            DriverConstants.DriverConfigFolder = driverSettings.DriverConfigs.Folder.getPath();
            DriverConstants.Configfiles = driverSettings.DriverConfigs.ConfigFiles;

            TestRunConstants.ResourceFolder = testsettings.ResourceFolder.getPath();
            TestRunConstants.TestDataFolder = testsettings.TestDataFolder.getPath();

            ReportConstants.ResultsFolder = util.CreateResultsFolder(reportsettings.ResultsFolder.getPath(), "");
            ReportConstants.ScreenShotFolder = util.CreateDirectory(ReportConstants.ResultsFolder, reportsettings.ScreenshotFolderName);
        }

        public void ConfigureReportModel()
        {
            ReportConstants.reportModel.addSystemInfo("Environment", TestRunConstants.Environment);
            ReportConstants.reportModel.addSystemInfo("Machine", Environment.MachineName);
        }

    }
}
