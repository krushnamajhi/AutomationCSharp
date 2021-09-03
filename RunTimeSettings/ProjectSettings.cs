using RunTimeSettings.ProjectSettingsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTimeSettings
{
    public class ProjectSettings
    {
        public Driversettings DriverSettings { get; set; }
        public TestSettings TestSettings { get; set; }
        public Reportsettings ReportSettings { get; set; }

        public void Load()
        {
            DriverConstants.DriverFolder = DriverSettings.DriverFolder.getPath();
            DriverConstants.DriverConfigFolder = DriverSettings.DriverConfigs.Folder.getPath();
            TestRunConstants.ResourceFolder = TestSettings.ResourceFolder.getPath();
            TestRunConstants.TestDataFolder = TestSettings.TestDataFolder.getPath();
            ReportConstants.ResultsFolder = ReportSettings.ResultsFolder.getPath();
            ReportConstants.ScreenShotFolder = ReportSettings.ScreenshotFolderName;
        }
    }
}
