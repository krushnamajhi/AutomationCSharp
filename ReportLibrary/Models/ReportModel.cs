using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary.Models
{
    public class ReportModel
    {
        private Dictionary<String, String> _SystemInformation = new Dictionary<string, string>();
        public String Status { get => getStatus(); }
        public Dictionary<string, string> SystemInformation { get => _SystemInformation; private set => _SystemInformation = value; }

        public DateTime StartTime { get => testCases.Min(x => x.StartTime); }
        public DateTime EndTime { get => testCases.Max(x => x.EndTime); }
        public TimeSpan ElapsedTime => EndTime - StartTime;


        public List<TestCase> testCases = new List<TestCase>();

        public void addTestCase(TestCase testCase)
        {
            TestCase t = new TestCase(testCase.TestCaseId)
            {
                TestCaseName = testCase.TestCaseName,
                ModuleName = testCase.ModuleName,
                Browser = testCase.Browser,
                Description = testCase.Description,
                StartTime = testCase.StartTime,
                EndTime = DateTime.Now,
                Iteration = testCases.Where(x => x.TestCaseName == testCase.TestCaseName && x.ModuleName == testCase.ModuleName).Count() + 1
            };

            foreach (var step in testCase.testSteps)
            {
                t.testSteps.Add(new TestSteps()
                {
                    Status = step.Status,
                    StepName = step.StepName,
                    StepDescription = step.StepDescription,
                    ScreenShotPath = step.ScreenShotPath
                });
            }
            testCases.Add(t);
        }

        public void addSystemInfo(string Key, String Value)
        {
            if (SystemInformation.ContainsKey(Key))
            {
                SystemInformation[Key] = Value;
            }
            else
            {
                SystemInformation.Add(Key, Value);
            }
        }

        string getStatus()
        {
            if (testCases.Any(x => x.Status.Equals("FAIL")))
                return "FAIL";
            else
                return "PASS";
        }
    }
}
