using Helpers;
using Helpers.SeriLog;
using KeywordDrivenProject.Models;
using KeywordDrivenProject.SupportLibraries;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ReportLibrary;
using RunTimeSettings;
using RunTimeSettings.ProjectSettingsModels;
using SeleniumLibrary;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeywordDrivenProject
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialSteps.InitialiseAppSettings();
            InitialSteps initialSteps = new InitialSteps();
            var TestCaseModalList = initialSteps.getTestCaseModalList();

            if (ConfigReader.IConfig.GetValue<bool>("RunTestScriptValidator"))
            {
                //TestScriptValidator scriptValidator = new TestScriptValidator();
                //scriptValidator.ValidateTestScript(TestCaseModalList);
            }
            else
            {
                int noOfNodes = TestCaseModalList.Count <= ConfigReader.IConfig.GetValue<int>("Number of Browsers") ?
                      TestCaseModalList.Count : ConfigReader.IConfig.GetValue<int>("Number of Browsers");
                ExecuteWeb executeWeb = new ExecuteWeb();
                List<List<TestCaseModal>> mapped = new List<List<TestCaseModal>>();

                var mappedTestCases = initialSteps.MapTestCaseInThreads(TestCaseModalList, noOfNodes);
                int node = 0;
                Parallel.For(node = 0, mappedTestCases.Count, node =>
                {
                    executeWeb.ExecuteAllTCsinDriver(mappedTestCases[node], node);
                });

                ExtentReport extentReport = new ExtentReport();
                extentReport.GenerateExtentReport(ReportConstants.ResultsFolder + "\\", ReportConstants.reportModel);


                //List<IWebDriver> drivers = new List<IWebDriver>();
                //SeleniumUtilities selUtil = new SeleniumUtilities();
                //for (int i = 0; i < noOfBrowers; i++)
                //{
                //    drivers.Add(selUtil.InitializeDriver(ConfigReader.IConfig.GetValue<String>("DefaultBrowser")));
                //}


                //DriverModal.LoadDriverModal(drivers, TestCaseModalList);
                //try
                //{
                //    Parallel.ForEach(DriverModal.get(), drv =>
                //    {
                //        executeWeb.ExecuteAllTCsinDriver("Chrome", drv.TestCases, drv.Driver, 0);
                //    });
                //}
                //catch (Exception) { }
                //finally
                //{
                //    DriverModal.clearDriverRepository();

                //    ExtentReport extentReport = new ExtentReport();
                //    extentReport.GenerateExtentReport(ReportConstants.ResultsFolder + "\\", ReportConstants.reportModel);
                //}



            }
        }

    }
}
