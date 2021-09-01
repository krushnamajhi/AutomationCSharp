using Helpers;
using Helpers.SeriLog;
using NUnit.Framework;
using NunitTest.Initialize;
using ReportLibrary;
using ReportLibrary.Models;
using RunTimeSettings;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest.Base
{
    public class BaseController
    {
        protected object EndLock = new object();

        protected static ILogger logger = LoggerConfig.Logger;

        protected Util util = new Util();

        protected Dictionary<String, ReportLogger> Reportlogs = new Dictionary<string, ReportLogger>();

        protected String TestKey => TestContext.CurrentContext.Test.MethodName + "==>" + TestContext.CurrentContext.Test.ID;

        protected ReportLogger reportLogger => Reportlogs[TestKey];


        private static void setTestDataFolder()
        {
            TestRunConstants.ResourceFolder = Directory.GetParent(Util.getRootPath()).FullName + "\\Resources";
            TestRunConstants.TestDataFolder = TestRunConstants.ResourceFolder + "\\TestData";
        }


        protected static IEnumerable<TestCaseData> GetDataFromExcelFile(DataSourcePath sourcePath, String FilePath, String SheetName)
        {
            
            String ExcelPath = getDataSourcePath(sourcePath, FilePath);

            if (!ExcelPath.Split('\\').Last().Contains("."))
            {
                ExcelPath = ExcelPath + ".xlsx";
            }

            List<Dictionary<String, String>> TestData = Util.getTestData(ExcelPath, SheetName);

            foreach (Dictionary<String, String> DictData in TestData)
            {
                yield return new TestCaseData(DictData);
            }
        }

        public enum DataSourcePath
        {
            Default, Custom
        }

        private static string getDataSourcePath(DataSourcePath pathType, String path)
        {
            if (TestRunConstants.ResourceFolder != "")
            {
                InitializeSettings IS = new InitializeSettings();
                IS.InitializeResourcesObjects();
            }

            switch (pathType)
            {
                case DataSourcePath.Default:
                    string testdataPath = !TestRunConstants.TestDataFolder.EndsWith("\\") ? TestRunConstants.TestDataFolder + "\\" : path;
                    return testdataPath + path;
                case DataSourcePath.Custom:
                    return path;
                default:
                    return "";
            }
        }

    }
}
