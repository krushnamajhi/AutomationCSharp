using Helpers;
using KeywordDrivenProject.Models;
using Microsoft.Extensions.Configuration;
using RunTimeSettings;
using RunTimeSettings.ProjectSettingsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenProject
{
    public class InitialSteps
    {
        public static void InitialiseAppSettings()
        {
            Util util = new Util();
            ConfigReader._Build("appsettingsKeywordDriven");

            var driverSettings = ConfigReader.IConfig.GetSection("DriverSettings").Get<Driversettings>();
            var testsettings = ConfigReader.IConfig.GetSection("TestSettings").Get<TestSettings>();
            var reportsettings = ConfigReader.IConfig.GetSection("ReportSettings").Get<Reportsettings>();
            TestRunConstants.Environment = ConfigReader.IConfig.GetValue<String>("Environment");


            DriverConstants.DriverFolder = driverSettings.DriverFolder.getPath();
            DriverConstants.DriverConfigFolder = driverSettings.DriverConfigs.Folder.getPath();
            DriverConstants.Configfiles = driverSettings.DriverConfigs.ConfigFiles;

            TestRunConstants.ResourceFolder = testsettings.ResourceFolder.getPath();
            TestRunConstants.TestDataFolder = testsettings.TestDataFolder.getPath();
            TestRunConstants.TestCaseFolder = testsettings.TestScriptFolder.getPath();
            TestRunConstants.ObjectRepositoryFolder = testsettings.ObjectRepositoryFolder.getPath();
            TestRunConstants.TestSuiteFolder = testsettings.TestSuiteFolder.getPath();
            TestRunConstants.SingleSheetFile = testsettings.SingleSheet.getPath();

            ReportConstants.ResultsFolder = util.CreateResultsFolder(reportsettings.ResultsFolder.getPath(), "");
            ReportConstants.ScreenShotFolder = util.CreateDirectory(ReportConstants.ResultsFolder, reportsettings.ScreenshotFolderName);
        }

        public List<TestCaseModal> getTestCaseModalList()
        {
            ExcelUtilities exelUtil = new ExcelUtilities();

            DefaultBrowser defaultBrowser = ConfigReader.IConfig.GetSection("DefaultBrowser").Get<DefaultBrowser>();

            Dictionary<string, List<string>> singlesheet = exelUtil.fetchWithCondition(TestRunConstants.SingleSheetFile, "SingleSheet", new List<string>() { "Execute::Yes" });

            List<TestCaseModal> testCaseModalList = new List<TestCaseModal>();

            for (int suiteNumber = 0; suiteNumber < singlesheet["Execute"].Count; suiteNumber++)
            {
                string TestSuitePath = singlesheet["TestSuitePath"][suiteNumber];
                string TestSuiteName = singlesheet["TestSuite"][suiteNumber];
                TestSuitePath = TestRunConstants.TestSuiteFolder + TestSuitePath + "\\" + TestSuiteName + ".xlsx";

                Dictionary<string, List<string>> TestSuiteSheet =
                    exelUtil.fetchWithCondition(TestSuitePath, "TestSuite", new List<string>() { "Execute::Yes" });

                for (int scriptNumber = 0; scriptNumber < TestSuiteSheet["Execute"].Count; scriptNumber++)
                {
                    TestCaseModal testCaseModal = new TestCaseModal();
                    testCaseModal.TestScriptName = TestSuiteSheet["TestScript"][scriptNumber];
                    testCaseModal.TestScriptPath = TestSuiteSheet["TestScriptPath"][scriptNumber];
                    testCaseModal.TestSuiteName = TestSuiteName;
                    testCaseModal.TestSuitePath = TestSuitePath;
                    testCaseModal.TestSuiteDescription = singlesheet["Description"][suiteNumber];
                    testCaseModal.TestScriptDescription = TestSuiteSheet["Description"][scriptNumber];
                    if (!defaultBrowser.UseDefaultBrowser)
                    {
                        testCaseModal.Browser = TestSuiteSheet["Browser"][scriptNumber];
                    }
                    else
                    {
                        testCaseModal.Browser = defaultBrowser.BrowserName;
                    }
                    testCaseModal.ObjectRepository = TestSuiteSheet["ObjectRepository"][scriptNumber];
                    testCaseModal.TestDataPath = TestSuiteSheet["TestData"][scriptNumber];
                    testCaseModal.Retry = int.Parse(TestSuiteSheet["Retry"][scriptNumber]);
                    testCaseModal.Repeat = int.Parse(TestSuiteSheet["Repeat"][scriptNumber]);
                    testCaseModalList.Add(testCaseModal);
                }
            }
            return testCaseModalList;
        }

        public List<List<TestCaseModal>> MapTestCaseInThreads(List<TestCaseModal> testCaseModalList, int noOfNodes)
        {
            List<List<TestCaseModal>> mapped = new List<List<TestCaseModal>>();
            int Node = 0;
            int startpoint = 0;
            int endPoint = 0;
            if (testCaseModalList.Count >= noOfNodes)
            {
                for(int n = 0; n < noOfNodes; n++)
                {
                    List<TestCaseModal> temp = new List<TestCaseModal>();
                    Node = n;
                    endPoint = (testCaseModalList.Count / noOfNodes) + startpoint;
                    for (int i = startpoint; i < endPoint; i++)
                    {
                        temp.Add(testCaseModalList[i]);
                    }
                    startpoint = endPoint;
                    Node++;
                    mapped.Add(temp);
                }
                if (testCaseModalList.Count % noOfNodes != 0)
                {
                    int Startpoint = testCaseModalList.Count - (testCaseModalList.Count % noOfNodes);
                    int endpoint = testCaseModalList.Count;
                    int driverIndex = 0;
                    for (int i = Startpoint; i < endpoint; i++)
                    {
                        mapped[driverIndex].Add(testCaseModalList[i]);
                        driverIndex++;
                    }
                }
            }
            else
            {
                int endpoint = testCaseModalList.Count;
                for (int i = 0; i < endpoint; i++)
                {
                    DriverTestCaseAssociationModal dtcaObj = new DriverTestCaseAssociationModal();
                    mapped.Add(new List<TestCaseModal>() { testCaseModalList[i] });
                }
            }
            return mapped;
        }

    }
}
