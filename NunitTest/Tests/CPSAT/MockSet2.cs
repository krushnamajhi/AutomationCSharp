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
    [Parallelizable(ParallelScope.Self)]
    public class MockSet2 : DriverTestBase
    {
        [Test]
        [Order(1)]
        [Parallelizable]
        [Category("MockSet 2, Test Suite 1")]
        public void Question_2()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://www.imdb.com/");

            driver.FindElement(By.Id("suggestion-search")).SendKeys("Gangs of New York");
            WebKeywords.Instance.WaitElementVisible(driver, driver.FindElement(By.XPath("//ul[@role='listbox']/li[1]/a")));
            WebKeywords.Instance.Click(reportLogger, driver, driver.FindElement(By.XPath("//ul[@role='listbox']/li[1]/a")), "Gangs of New York Suggestion");
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30);
            String[] time = WebKeywords.Instance.GetAttribute(reportLogger, driver.FindElement(By.XPath("//h1/following-sibling::div[1]//li[3]")), "Run Time", "innerText").Trim().Split(' ');
            time[0] = time[0].Replace("h", "");
            time[1] = time[1].Replace("min", "");
            int min = int.Parse(time[0]) * 60 + int.Parse(time[1]);
            Assert.That(min < 180, Is.True, "Run Time is greater than 180 mins: " + min);
            Assert.That(WebKeywords.Instance.GetAttribute(reportLogger, driver.FindElement(By.XPath("//div[@data-testid='genres']")), "Genre Section", "innerText")
                .Contains("Crime"), Is.True, "Genre no Contain Crime");

            String MPARating = WebKeywords.Instance.GetAttribute(reportLogger, driver.FindElement(By.XPath("//h1/following-sibling::div[1]//li[2]")), "MPA Rating ", "innerText").Trim();
            Assert.That(MPARating.Equals("R"), Is.True, "MPAA Rating is not R: " + MPARating);
            WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.LinkText("User reviews")), "User Reviews Section");

            List<String> Reviewers = driver.FindElements(By.XPath("//div[@class='review-container']//span[@class='display-name-link']")).Select
                (s =>
                {
                    return s.GetAttribute("innerText").Trim();
                }).ToList();

            reportLogger.Info("Reviewers", String.Join(", ", Reviewers));
            Console.WriteLine("Reviewers: " + String.Join(", ", Reviewers));
        }


        [Test]
        [Parallelizable(ParallelScope.Children)]
        [TestCaseSource(nameof(GetDataFromExcelFile),
            new Object[]
            {
                DataSourcePath.Default,
                @"CPSAT\MockSet2\Question_3" ,
                "TestData"
            })]
        [Order(0)]
        public void Question_3(Dictionary<String, String> TestData)
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, " https://www.firstcry.com/");
            WebKeywords.Instance.SetText_PressEnter(reportLogger, driver, driver.FindElement(By.Id("search_box")), "Search Box", TestData["SearchText"]);
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30);
            //driver.SwitchTo().Window(driver.CurrentWindowHandle);
            //WebKeywords.Instance.Click(reportLogger, driver, driver.FindElement(By.XPath("//div[@class='sort-select']")), "Sort By");
            //Thread.Sleep(1000);
            //driver.FindElement(By.LinkText("Price")).Click();
            //try { driver.FindElement(By.LinkText("Price")).Click(); } catch (Exception) { }

            for (int i = 1; i < 3; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.
                    FindElement(By.XPath("//div[@class='sort-select']/following-sibling::ul/li/a[text()='Price']")));
                try
                {
                    WebKeywords.Instance.WaitElementVisible(driver, By.XPath("//div[@class='sort-select-content L12_42' and contains(text(),'Price:')]"), 4);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            //WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.XPath("//ul[contains(@class,'sort-menu')]/li[@data-val='Price']/a")), "Sort By - Price");
            Thread.Sleep(10000);
        }

        [Test]
        [Parallelizable]
        public void Question5()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://letterboxd.com/");
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30);
            String Header = WebKeywords.Instance.GetAttribute(reportLogger, driver.FindElement(By.XPath("//h1[@class='site-logo']")),
                "LetterBox Header", "innerText").Trim();
            Assert.That(Header.ToLower().Contains("letterbox"), Is.True);
            WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.XPath("//a[@href='/members/']")), "Members - Top Panel");
            Thread.Sleep(2000);
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30, true);

            List<String> Reviewers = driver.FindElements(By.XPath("//*[@class='featured-person']//h3/a")).Select
                (s =>
                {
                    return s.GetAttribute("innerText").Trim();
                }).ToList();

            reportLogger.Info("Reviewers", String.Join(", ", Reviewers));
            Console.WriteLine("Reviewers: " + String.Join(",\n ", Reviewers));

            String NoOfreviews = WebKeywords.Instance.GetAttribute(reportLogger, driver.FindElement(By.XPath("(//*[@class='featured-person']//p)[1]/a[2]")),
                "No Of Reviews of First Reviewer", "innerText");


            reportLogger.Info("No Of Reviews of First Reviewer", NoOfreviews);
            Console.WriteLine("No Of Reviews of First Reviewer: " + NoOfreviews);


        }

        [Test]
        [Parallelizable]
        public void Question6()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "http://www.nseindia.com/");
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 30);
            WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.LinkText("Currency Derivatives")), "Currency Derivatives");
            Thread.Sleep(2000);
            WebKeywords.Instance.WaitForPageToLoad(reportLogger, driver, 45);
            WebKeywords.Instance.SetText_PressEnter(reportLogger, driver, driver.FindElement(By.Id("header-search-input")), "Search Text Box", "USDINR");

            String Result = WebKeywords.Instance.GetAttribute(reportLogger, driver.FindElement(By.XPath("//div[@id='searchListing']/div[1]")),
                 "First Search Result", "innerText").Trim();
            Console.WriteLine(Result);

        }

    }
}
