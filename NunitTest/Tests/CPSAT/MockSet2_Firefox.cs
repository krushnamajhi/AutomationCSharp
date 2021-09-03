using Helpers.SeriLog;
using Newtonsoft.Json;
using NUnit.Framework;
using NunitTest.Base;
using OpenQA.Selenium;
using RunTimeSettings;
using SeleniumLibrary.Pages.PHPTravels;
using SeleniumLibrary.Web;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NunitTest.Tests.CPSAT
{
    [TestFixture("Firefox")]
    [Parallelizable(ParallelScope.Self)]
    public class MockSet2_Firefox : DriverTestBase
    {
        public MockSet2_Firefox(string browserName)
        {
           this.browserName = browserName;
        }

        [Test]
        [Order(2)]
        [Category("MockSet 2, Test Suite 1")]
        [Parallelizable]
        public void Question_1()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://www.shoppersstop.com");
            WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.LinkText("BRANDS")), "BRANDS TAB");
            string url = driver.Url + "/haute-curry";
            WebKeywords.Instance.Navigate(reportLogger, driver, url, true);
            Assert.That(driver.Title.Trim().Equals("Haute Curry Bags | Haute Curry Ladies Footwear | Shoppers Stop | Shoppers Stop",
                StringComparison.CurrentCultureIgnoreCase), Is.True, "Title is not as Expected");
        }

    }
}
