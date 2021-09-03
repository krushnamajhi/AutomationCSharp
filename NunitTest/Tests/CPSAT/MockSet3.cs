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
using Helpers;
using SeleniumLibrary;

namespace NunitTest.Tests.CPSAT
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class MockSet3 : DriverTestBase
    {

        [Test]
        [Parallelizable]
        [Order(0)]
        [Category("MockSet3 - > Question 6")]
        public void Question_2()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://maps.google.com");

            WebKeywords.Instance.SetText(reportLogger, driver, driver.FindElement(By.Id("searchboxinput")), "GoogleMaps: Search box", "Wankhede Stadium");
            driver.FindElement(By.Id("searchboxinput")).SendKeys(Keys.Enter);
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30);
            Thread.Sleep(5000);
            WebKeywords.Instance.WaitElementVisible(driver, By.XPath("(//div[contains(@class,'header-title')])[1]"), 30);
            reportLogger.Info("Taking Screenshot", "Screenshot Captured", WebKeywords.Instance.getScreenshotPath(driver, reportLogger));
            String Header = WebKeywords.Instance.GetAttribute(reportLogger,
                driver.FindElement(By.XPath("(//div[contains(@class,'header-title')])[1]")), "Left Frame Header", "innerText");

            Assert.That(Header.Contains("Stadium"), Is.True);
            String Title = driver.Title;
            Assert.AreEqual("Wankhede Stadium Mumbai - Google Maps", Title);

            Console.WriteLine(WebKeywords.Instance.GetAttribute(
                reportLogger, driver.FindElement(By.XPath("((//div[contains(@class,'header-title')])[1]//h2/following-sibling::div/div/div)[1]")), "Ratings and Reviews", "innerText"));
            var element = "(//div[contains(@class,'header-title')])[1]/following-sibling::div[5]";
            var AddressXpath = $"{element}/div[1]";
            var WebUrlXpath = $"{element}/div[2]";

            Assert.That(driver.FindElement(By.XPath(WebUrlXpath)).GetAttribute("innerText").Contains("mumbaicricket.com"));
            Console.WriteLine(driver.FindElement(By.XPath(AddressXpath)).GetAttribute("innerText"));


            String ScreenshotPath = util.CreateDirectory(ReportConstants.ScreenShotFolder, reportLogger.GetTestCase().ModuleName.Replace(".", "_")) +
"\\" + reportLogger.GetTestCase().TestCaseName + "_" + Util.getCurrentTime() + ".png";
            ScreenshotCommon sc = new ScreenshotCommon();
            sc.CaptureDesktopScreenshot(ScreenshotPath);
            reportLogger.Info("Capturing Screenshot of Desktop", "Captured Screenshot of Desktop", ScreenshotPath);

        }

    }
}
