using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenProject.Models
{
    public class LocatorModel
    {
        public String ObjectName { get; set; }
        public String ObjectID { get; set; }

        public void Load(Dictionary<String, String> ObjRepos,String ObjectName)
        {
            this.ObjectName = ObjectName;
            if (!string.IsNullOrEmpty(ObjectName))
            {
                this.ObjectID = ObjRepos[ObjectName];
            }
        }
    }
}
