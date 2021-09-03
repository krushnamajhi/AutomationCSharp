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
using SeleniumLibrary.Pages.Google;

namespace NunitTest.Tests.CPSAT
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class MockSet1 : DriverTestBase
    {
        [Test]
        [Order(2)]
        [Parallelizable(ParallelScope.Children)]
        [TestCaseSource(nameof(GetDataFromExcelFile),new Object[]
            {
                DataSourcePath.Default,
                "TD_pepperFry",
                "TestData"
            })]
        public void pepperFry(Dictionary<String, String> TestData)
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "http://www.pepperfry.com/");
            WebKeywords.Instance.SetText(reportLogger, driver, driver.FindElement(By.Id("search")), "PepperFry: Search box", TestData["Keyword"]);
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            WebKeywords.Instance.VerifyTextDisplayed(reportLogger, driver,
                driver.FindElement(By.XPath("//span[text()='Search Results for']/following-sibling::h1")),
                "Search Result for", TestData["Keyword"]);

            WebKeywords.Instance.Click(reportLogger, driver, driver.FindElement(By.ClassName("drpdwn-price-htol")), "Sort By - Box");
            System.Threading.Thread.Sleep(3000);
            WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.XPath("//a[text()='Price Low to High']")), "Sort by option: Price Low to High");
            Thread.Sleep(5000);
        }


        [Test]
        [Order(1)]
        [Parallelizable]
        [Category("suite")]
        public void Wikipedia()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://www.wikipedia.org/");

            String NoOfEnglishArticles = WebKeywords.Instance.GetAttribute(reportLogger,
                driver.FindElement(By.XPath("//a[contains(@title,'English — Wikipedia')]/..//small")), "No of Articles in English", "innerText");

            Console.WriteLine("No of English Articles: " + NoOfEnglishArticles);

            WebKeywords.Instance.JSClick(reportLogger, driver,
                driver.FindElement(By.XPath("//a[contains(@title,'English — Wikipedia')]")), "English Link");

            WebKeywords.Instance.SetText_PressEnter(reportLogger, driver, driver.FindElement(By.Id("searchInput")), "Wiki Search box", "Anna University", 2000);

            WebKeywords.Instance.SetText(reportLogger, driver, driver.FindElement(By.Id("searchInput")), "Wiki: Search box", "Anna University");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("searchInput")).SendKeys(Keys.Enter);
            
            String MotoInEnglishText = WebKeywords.Instance.GetAttribute(reportLogger,
                driver.FindElement(By.XPath("(//table[@class='infobox vcard'])[1]//tr[2]/td")), "Moto In English", "innerText");
            Console.WriteLine("Moto in English: " + MotoInEnglishText);

            Assert.That(MotoInEnglishText.Contains("Knowledge"), Is.True);

            List<String> Notable_Alumini = driver.FindElements(By.XPath("//span[@id='Notable_alumni']/../following-sibling::ul/li")).
                Select(x => { return x.GetAttribute("innerText"); }).ToList();

            Console.WriteLine("Notable Alumini: " + JsonConvert.SerializeObject(Notable_Alumini));

            Assert.That(Notable_Alumini.Any(x => x.ToLower().Contains("Shiv Nadar")), Is.True);    

        }


        [Test]
        [Parallelizable]
        [Order(0)]
        public void GoogleMaps()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://maps.google.com");

            WebKeywords.Instance.SetText(reportLogger, driver, driver.FindElement(By.Id("searchboxinput")), "GoogleMaps: Search box", "Angul, Odisha, 759122");
            driver.FindElement(By.Id("searchboxinput")).SendKeys(Keys.Enter);

            WebKeywords.Instance.WaitElementVisible(driver, By.XPath("(//div[contains(@aria-label,\"Results for\")]//a)[1]"), 60);

            String FirstResult = WebKeywords.Instance.GetAttribute(reportLogger,
                driver.FindElement(By.XPath("(//div[contains(@aria-label,\"Results for\")]//a)[1]")), "First Search Result ", "aria-label");
            reportLogger.Info("Taking Screenshot", "Captured Successfully", WebKeywords.Instance.getScreenshotPath(driver, reportLogger));

            Console.WriteLine(FirstResult);

            WebKeywords.Instance.JSClick(reportLogger, driver,
                driver.FindElement(By.XPath("(//div[contains(@aria-label,\"Results for\")]//a)[1]/..//button[@data-value = 'Directions']")),
                "Directions");

            WebKeywords.Instance.WaitElementVisible(driver, By.XPath("//input[contains(@aria-label,'Destination') or contains(@aria-label,'destination')]"));

            WebKeywords.Instance.SetText(reportLogger, driver,
                driver.FindElement(By.XPath("//input[contains(@aria-label,'Destination') or contains(@aria-label,'destination')]")),
                "GoogleMaps: Destination Text box", "Deloitte USI, Mumbai");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[contains(@aria-label,'Destination') or contains(@aria-label,'destination')]")).SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            WebKeywords.Instance.WaitElementVisible(driver, By.XPath("//button[img[@aria-label='Driving']]"));
            WebKeywords.Instance.JSClick(reportLogger, driver,
                driver.FindElement(By.XPath("//button[img[@aria-label='Driving']]")), "Driving Option");
            WebKeywords.Instance.WaitElementVisible(driver, By.XPath("//div[contains(@class,'section-directions-trip-duration ')]"));
            Console.WriteLine(WebKeywords.Instance.GetAttribute(reportLogger,
                driver.FindElement(By.XPath("(//div[contains(@class,'section-directions-trip-duration')])[1]")), "Trip Duration", "innerText"));

            Console.WriteLine(WebKeywords.Instance.GetAttribute(reportLogger,
                driver.FindElement(By.XPath("(//div[contains(@class,'section-directions-trip-distance')])[1]")), "Trip Distance", "innerText"));

        }

        [Test]
        public void Question5()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://mu.ac.in/portal");
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30);
        }

    }
}
