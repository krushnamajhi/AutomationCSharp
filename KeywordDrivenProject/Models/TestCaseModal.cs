using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenProject.Models
{
    public class TestCaseModal
    {
        String testScriptName = "";
        String testSuiteName = "";
        String testSuitePath = "";
        String module = "";
        
        String objectRepositoryPath = "";
        String testData = "";
        public String Browser = "Chrome";

        public int Repeat { get; set; }
        public int Retry { get; set; }
        public String TestScriptPath { get; set; }

        String testSuiteDescription = "";
        String testScriptDescription = "";

        public string TestScriptName { get => testScriptName; set => testScriptName = value; }
        public string TestSuiteName { get => testSuiteName; set => testSuiteName = value; }
        public string Module { get => module; set => module = value; }
        public string ObjectRepository { get => objectRepositoryPath; set => objectRepositoryPath = value; }
        public string TestDataPath { get => testData; set => testData = value; }
        public string TestSuiteDescription { get => testSuiteDescription; set => testSuiteDescription = value; }
        public string TestScriptDescription { get => testScriptDescription; set => testScriptDescription = value; }
        public string TestSuitePath { get => testSuitePath; set => testSuitePath = value; }
    }
}
