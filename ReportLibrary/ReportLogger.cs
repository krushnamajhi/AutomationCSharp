using Helpers.SeriLog;
using Newtonsoft.Json;
using ReportLibrary.Models;
using Serilog;
using System;
using System.Linq;

namespace ReportLibrary
{
    public class ReportLogger
    {
        private static readonly ILogger logger = LoggerConfig.Logger;


        TestCase testCase = new TestCase();

        public ReportLogger(String TestCaseName, String ModuleName, String Browser)
        {
            testCase.TestCaseName = TestCaseName;
            testCase.ModuleName = ModuleName;
            testCase.Browser = Browser;
            testCase.Iteration = 1;
            testCase.StartTime = DateTime.Now;

            logger.Here().Information($"Initiated TestCase : {TestCaseName}, {ModuleName}");


        }

        public void Pass(String StepName, String StepDescription, String Screenshot = "")
        {
            Log(Status.PASS, StepName, StepDescription, Screenshot);
        }

        public void Fail(String StepName, String StepDescription, String Screenshot = "")
        {
            Log(Status.FAIL, StepName, StepDescription, Screenshot);
        }

        public void Error(String StepName, String StepDescription, String Screenshot = "")
        {
            Log(Status.ERROR, StepName, StepDescription, Screenshot);
        }

        public void Info(String StepName, String StepDescription, String Screenshot = "")
        {
            Log(Status.DONE, StepName, StepDescription, Screenshot);
        }

        private void Log(String Status, String StepName, String StepDescription, String Screenshot)
        {
            logger.Here().
                Information($"Logging to Report : Status: {Status}, Step: {StepName} => {StepDescription}, Screenshot: {Screenshot}");


            testCase.testSteps.Add(new TestSteps()
            {
                Status = Status,
                StepName = StepName,
                StepDescription = StepDescription,
                ScreenShotPath = Screenshot,
                ExecutionTime = DateTime.Now
            });
        }

        public TestCase GetTestCase()
        {
            return testCase;
        }

        public void AddDescription(String desc) { testCase.Description = desc; }

        public String GetTestCaseString()
        {
            return JsonConvert.SerializeObject(testCase, Formatting.Indented);
        }

        public bool AreEqual(String Expected, String Actual, String ErrorMessage = null) 
        {
            bool Equal = Expected.Equals(Actual);
            if (Equal)
            {
                Pass("Verifying Text", $"Expected: '{Expected}', Actual: '{Actual}', Both Text are Equal");
            }
            else
            {
                if (ErrorMessage ==null)
                {
                    Fail("Verifying Text", $"Expected: '{Expected}', Actual: '{Actual}', Both Text are not Equal");
                }
                else
                {
                    Fail("Verifying Text", ErrorMessage);
                }
            }
            return Equal;
        }

        public bool CheckEquation(Func<String, String, bool> Equation, bool ExpectedResult, String PassMessage = null, String FailMessage = null)
        {
            bool Equal = Equation.Equals(ExpectedResult);
            if (Equal)
            {
                if (PassMessage == null)
                {
                    Pass("Verifying Values", $"Expected: '{ExpectedResult}', Actual: '{Equation}', Both are Equal");
                }
                else
                {
                    Pass("Verifying Values", PassMessage);
                }
            }
            else
            {
                if (FailMessage == null)
                {
                    Fail("Verifying Text", $"Expected: '{ExpectedResult}', Actual: '{Equation}', Both Text are not Equal");
                }
                else
                {
                    Fail("Verifying Text", FailMessage);
                }
            }
            return Equal;
        }
    }
}
