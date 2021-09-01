using Helpers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using ReportLibrary;
using RunTimeSettings;
using SeleniumLibrary;
using SeleniumLibrary.Web;
using System;
using System.Collections.Generic;


namespace NunitTest.Base
{
    [TestFixture]
    public class DriverTestBase : BaseController
    {
        protected String browserName = "Chrome";

        Dictionary<String, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        protected IWebDriver driver { get => getDriver(); }
        SeleniumUtilities selUtil = new SeleniumUtilities();


        [SetUp]
        public void _setUp()
        {
            if (!Reportlogs.ContainsKey(TestKey))
            {
                Reportlogs.Add(TestKey, new ReportLogger(
                        TestContext.CurrentContext.Test.MethodName, TestContext.CurrentContext.Test.ClassName, selUtil.getBrowserName(driver)));
            }
            else
                Reportlogs[TestKey] = new ReportLogger(
                        TestContext.CurrentContext.Test.MethodName, TestContext.CurrentContext.Test.ClassName, selUtil.getBrowserName(driver));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConfigReader.IConfig.GetValue<int>("ElementTimeOut"));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(ConfigReader.IConfig.GetValue<int>("PageLoadTimeOut"));
        }

        [TearDown]
        public void EndTest()
        {
            String TestStatus = TestContext.CurrentContext.Result.Outcome.Status.ToString();

            if (TestStatus.ToLower().Contains("fail"))
            {
                reportLogger.Fail("Testcase Status", 
                    "TestCase Failed with Message: " + TestContext.CurrentContext.Result.Message + 
                    ", \n StackTrace: " + TestContext.CurrentContext.Result.StackTrace,
                    WebKeywords.Instance.getScreenshotPath(driver,reportLogger));
            }

            lock (EndLock)
            {
                ReportConstants.reportModel.addTestCase(reportLogger.GetTestCase());
                Reportlogs.Remove(TestKey);
                QuitDriver();
            }

        }



        private IWebDriver getDriver()
        {
            if (!Drivers.ContainsKey(TestKey))
            {
                Drivers.Add(TestKey, selUtil.InitializeDriver(browserName));
            }
            return Drivers[TestKey];
        }
        private void QuitDriver()
        {
            if (Drivers.ContainsKey(TestKey))
            {
                Drivers[TestKey].Quit();
                Drivers.Remove(TestKey);
            }
        }
    }
}
