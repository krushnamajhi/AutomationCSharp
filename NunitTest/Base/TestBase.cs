using Helpers;
using NUnit.Framework;
using ReportLibrary;
using ReportLibrary.Models;
using RunTimeSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest.Base
{
    [TestFixture]
    public class TestBase : BaseController
    {
        [SetUp]
        public void Setup()
        {
            if (!Reportlogs.ContainsKey(TestKey))
            {
                Reportlogs.Add(TestKey, new ReportLogger(
                        TestContext.CurrentContext.Test.MethodName, TestContext.CurrentContext.Test.ClassName,""));
            }
            else
                Reportlogs[TestKey] = new ReportLogger(
                        TestContext.CurrentContext.Test.MethodName, TestContext.CurrentContext.Test.ClassName,""); 

        }

        [TearDown]
        public void EndTest()
        {
            String TestStatus = TestContext.CurrentContext.Result.Outcome.Status.ToString();

            if (TestStatus.ToLower().Contains("fail"))
            {
                reportLogger.Fail("Testcase Status",
                    "TestCase Failed due to Error: " + TestContext.CurrentContext.Result.Message + 
                    " \n Error: " + TestContext.CurrentContext.Result.StackTrace);
            }
            lock (EndLock)
            {
                ReportConstants.reportModel.addTestCase(reportLogger.GetTestCase());
                Reportlogs.Remove(TestKey);
            }
        }

    }
}
