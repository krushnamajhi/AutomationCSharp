using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Helpers;
using ReportLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary
{
    public class ExtentReport
    {
        Util util = new Util();
        ImageUtil ImageUtil = new ImageUtil();
        public void GenerateExtentReport(String path, ReportModel model)
        {
            var extent = new ExtentReports();
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            extent.AttachReporter(htmlReporter);

            foreach(var InfoName in model.SystemInformation.Keys)
            {
                extent.AddSystemInfo(InfoName, model.SystemInformation[InfoName]);
            }
            try
            {

                foreach (var test in model.testCases)
                {
                    var extentTest = extent.CreateTest(test.TestCaseName + " ==> " + test.Iteration,test.Description);
                    
                    foreach (var steps in test.testSteps)
                    {
                        if (steps.ScreenShotPath == "")
                            extentTest.Log(steps.getExtentStatus(), steps.StepName + " =====> " + steps.StepDescription);
                        else
                        {
                            MediaEntityModelProvider screenshot_provider;
                            try
                            {
                                screenshot_provider = MediaEntityBuilder.
                                    CreateScreenCaptureFromBase64String(
                                    ImageUtil.ReduceImageSize_ConvertToBase64(steps.ScreenShotPath, 0.7),
                                    steps.StepName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss")).Build();
                            }
                            catch (Exception)
                            {
                                screenshot_provider = null;
                            }
                            if (screenshot_provider != null)
                            {
                                extentTest.Log(steps.getExtentStatus(), steps.StepName + " ====> " + steps.StepDescription, screenshot_provider);
                            }
                            else
                            {
                                extentTest.Log(steps.getExtentStatus(), steps.StepName + " =====> " + steps.StepDescription);
                            }

                        }
                    }

                    if (test.NoOfStepsPassed != 0 && test.NoOfStepsFailed != 0)
                    {
                        extentTest.Info($"Number of Steps Passed = { test.NoOfStepsPassed }, Number of Steps Failed = { test.NoOfStepsFailed }");
                    }

                    extentTest.AssignCategory(test.ModuleName);

                    if (test.Browser != "")
                    {
                        extentTest.AssignCategory(test.Browser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                extent.Flush();
            }

        }
    }
}
