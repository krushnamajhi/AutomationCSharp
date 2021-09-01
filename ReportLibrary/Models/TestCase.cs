using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary.Models
{
    public class TestCase 
    {
        public String TestCaseId { get; init; }
        public String TestCaseName { get; set; }
        public String ModuleName { get; set; }
        public String Status { get => getStatus(); }
        public String Browser { get; set; }
        public int Iteration { get; set; }
        public String Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan ElapsedTime => EndTime - StartTime;

        public int NoOfStepsPassed { get => testSteps.Where(x => x.Status == Models.Status.PASS).ToList().Count; }
        public int NoOfStepsFailed { get => testSteps.Where(x => x.Status == Models.Status.FAIL).ToList().Count; }

        public List<TestSteps> testSteps = new List<TestSteps>();

        public TestCase()
        {
            TestCaseId = Guid.NewGuid().ToString();
        }

        public TestCase(String TestCaseId)
        {
            this.TestCaseId = TestCaseId;
        }

        String getStatus()
        {
            try
            {
                if (testSteps.Any(x => x.Status.Equals(Models.Status.FAIL)))
                {
                    return Models.Status.FAIL;
                }
                else
                    return Models.Status.PASS;

            }
            catch (Exception)
            {
                return Models.Status.FAIL;
            }
        }

    }
}
