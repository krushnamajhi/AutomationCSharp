using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenProject.Models
{
    public class DriverTestCaseAssociationModal
    {
        IWebDriver driver;
        List<TestCaseModal> testCases = new List<TestCaseModal>();
        int node;

        public IWebDriver Driver { get => driver; set => driver = value; }
        public int Node { get => node; set => node = value; }
        public List<TestCaseModal> TestCases { get => testCases; set => testCases = value; }
    }
}
