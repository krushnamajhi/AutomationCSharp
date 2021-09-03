using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTimeSettings.ProjectSettingsModels
{
    [JsonObject("Driversettings")]
    public class Driversettings
    {
        [JsonProperty("DriverFolder")]
        public Folder DriverFolder { get; set; }

        [JsonProperty("DriverConfigs")]
        public DriverConfigs DriverConfigs { get; set; }
    }

}
