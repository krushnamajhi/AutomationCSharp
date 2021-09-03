using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTimeSettings.ProjectSettingsModels
{
    public class TestSettings
    {
        public Folder ResourceFolder { get; set; }
        public Folder TestDataFolder { get; set; }
        public Folder TestScriptFolder { get; set; }
        public Folder ObjectRepositoryFolder { get; set; }
        public Folder TestSuiteFolder { get; set; }
        public Folder SingleSheet { get; set; }
    }

}
