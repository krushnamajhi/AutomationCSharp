using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary.Models
{
    public class TestSteps 
    {
        public String StepName { get; set; }
        public String StepDescription { get; set; }
        public String ScreenShotPath { get; set; }
        public String Status { get; set; }
        public DateTime ExecutionTime { get; set; }
        public AventStack.ExtentReports.Status getExtentStatus()
        {
            if (Status.Equals(Models.Status.PASS))
            {
                return AventStack.ExtentReports.Status.Pass;
            }
            else if (Status.Equals(Models.Status.FAIL))
            {
                return AventStack.ExtentReports.Status.Fail;
            }
            else if (Status.Equals(Models.Status.DONE))
            {
                return AventStack.ExtentReports.Status.Info;
            }
            else if (Status.Equals(Models.Status.ERROR))
            {
                return AventStack.ExtentReports.Status.Error;
            }
            return AventStack.ExtentReports.Status.Info;
        }
    }
}
