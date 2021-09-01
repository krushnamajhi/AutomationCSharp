using Helpers;
using NUnit.Framework;
using NunitTest.Base;
using NunitTest.Initialize;
using ReportLibrary;
using RunTimeSettings;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest
{
    [SetUpFixture]
    class OneTimeActions
    {
        Util util = new Util();

        [OneTimeSetUp]
        public void _oneTimeSetup()
        {
            InitializeSettings IS = new InitializeSettings();
            IS.InitialiseVariables();
            IS.ConfigureReportModel();
        }

        [OneTimeTearDown]
        public void _oneTimeTearDown()
        {
            try
            {
                ExtentReport exR = new ExtentReport();
                exR.GenerateExtentReport(ReportConstants.ResultsFolder + "\\", ReportConstants.reportModel);
            }
            catch (Exception) { }
            //try
            //{
            //    ExcelReport excelReport = new ExcelReport();
            //    excelReport.GenerateExcelReport(ReportConstants.ResultsFolder + "\\", ReportConstants.reportModel);
            //}
            //catch(Exception) { }
        }
    }
}
