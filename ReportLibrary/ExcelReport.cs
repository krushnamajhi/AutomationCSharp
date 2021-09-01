using Helpers;
using ReportLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary
{
    public class ExcelReport
    {
        public void GenerateExcelReport(String path, ReportModel model)
        {
            ExcelUtilities excelUtil = new ExcelUtilities();

            Dictionary<String, List<String>> data;

            data = new Dictionary<string, List<string>>();
            var sysInfo = model.SystemInformation;

            excelUtil.AddRowsInSheet(path + "ExcelReport.xlsx", "OverallSummary", new List<List<string>>() { new List<string>(){ "Overall Report Information" } });

            excelUtil.AddRowsInSheet(path + "ExcelReport.xlsx", "OverallSummary", new List<List<string>>() { new List<string>() { " " } });

            List<List<String>> data_1 = new List<List<string>>();
            data_1.Add(new List<string>() { "Name", "Value" });

            foreach(String k in sysInfo.Keys)
            {
                data_1.Add(new List<string>() { k, sysInfo[k] });
            }
            data_1.Add(new List<string>() { "Overall Status", model.Status });
            data_1.Add(new List<string>() { "Execution Start Time", model.StartTime.ToString("MM/dd/yyyy HH:mm:ss:fff") });
            data_1.Add(new List<string>() { "Execution End Time", model.EndTime.ToString("MM/dd/yyyy HH:mm:ss:fff") });
            data_1.Add(new List<string>() { "Time Elapsed", model.ElapsedTime.ToString("c") });
     

            excelUtil.AddRowsInSheet(path + "ExcelReport.xlsx", "OverallSummary", data_1);

            data = new Dictionary<string, List<string>>();

            foreach (var testcase in model.testCases)
            {
                AddTodata(data, testcase, "Module Name", testcase.ModuleName);
                AddTodata(data, testcase, "Test Name", testcase.TestCaseName);
                AddTodata(data, testcase, "Test Description", testcase.Description);
                AddTodata(data, testcase, "Iteration", testcase.Iteration.ToString());
                AddTodata(data, testcase, "Test Status", testcase.Status);
                AddTodata(data, testcase, "Start Time", testcase.StartTime.ToString("MM/dd/yyyy HH:mm:ss:fff"));
                AddTodata(data, testcase, "End Time", testcase.EndTime.ToString("MM/dd/yyyy HH:mm:ss.fff"));
                AddTodata(data, testcase, "Time Elapsed",testcase.ElapsedTime.ToString("c"));
            }

            excelUtil.AddRowsInSheet(path + "ExcelReport.xlsx", "TestExecutionSumary", data);


            void AddTodata(Dictionary<String, List<String>> data, TestCase testcase, String header, String Value)
            {
                if (!data.ContainsKey(header))
                {
                    data.Add(header, new List<string>() { Value });
                }
                else
                {
                    data[header].Add(Value);
                }
            }
        }

    }
}
