using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTimeSettings.ProjectSettingsModels
{
    [JsonObject("Reportsettings")]
    public class Reportsettings
    {
        [JsonProperty("ResultsFolder")]
        public Folder ResultsFolder { get; set; }

        [JsonProperty("ScreenshotFolderName")]
        public string ScreenshotFolderName { get; set; }
    }
}
