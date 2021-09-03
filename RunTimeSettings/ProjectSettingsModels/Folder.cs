using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTimeSettings.ProjectSettingsModels
{
    public class Folder
    {
        public bool DefaultPath { get; set; }
        public string Path { get; set; }


        public String getPath()
        {
            if (DefaultPath)
            {
                return getRootPath() + Path;
            }
            else
                return Path;
        }

        static String getSolutionPath()
        {
            return Directory.GetParent(System.IO.Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()))).Parent.FullName;
        }
        static String getRootPath()
        {
            return Directory.GetParent(System.IO.Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()))).FullName;
        }
    }
}
