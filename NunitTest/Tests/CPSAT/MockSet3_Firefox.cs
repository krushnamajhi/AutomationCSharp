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
    public class MockSet3_Firefox : DriverTestBase
    {

        public MockSet3_Firefox(string browserName)
        {
            this.browserName = browserName;
        }


        [Test]
        [Order(2)]
        [Parallelizable(ParallelScope.Children)]
        [Category("MockSet3 - > Question 6")]
        [TestCaseSource(nameof(GetDataFromExcelFile), new Object[]
        {
                DataSourcePath.Default,
                @"CPSAT\MockSet3\Question_1",
                "TestData"
        })]
        public void Question_1(Dictionary<String, String> TestData)
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://www.meripustak.com/");
            WebKeywords.Instance.SetText_PressEnter(reportLogger, driver, driver.FindElement(By.Id("txtsearch")), "PepperFry: Search box", TestData["SearchText"]);

            var BookElements = driver.FindElements(By.XPath("//div[@class='book_list']//li"));

            foreach (var b in BookElements)
            {
                String BookName = WebKeywords.Instance.GetAttribute(reportLogger,
                b.FindElement(By.ClassName("book_list_name")), "Book Name", "innerText");

                String DiscountedPrice = WebKeywords.Instance.GetAttribute(reportLogger,
                    b.FindElement(By.ClassName("our_tag_price")), "Book Name", "innerText");
                Console.WriteLine("Book Name: " + BookName);
                Console.WriteLine("Discounted Price: " + DiscountedPrice);
            }
        }




        [Test]
        [Parallelizable]
        public void Question_3()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "https://qaagility.com/");
            Assert.That(driver.Title.Contains("QAAgility"), Is.True);
            Assert.That(driver.FindElement(By.ClassName("copyright-bar")).GetAttribute("innerText").Trim()
                .Equals("QAAgility Technologies © 2020. All Rights Reserved"), Is.True);
        }

        [Test]
        [Parallelizable]
        public void Question_4()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, " http://www.allmovie.com/");
            WebKeywords.Instance.SetText_PressEnter(reportLogger,
                driver, driver.FindElement(By.ClassName("site-search-input")), "Search Box", "The GodFather");
            Thread.Sleep(5000);
            Console.WriteLine(driver.FindElement(By.XPath("//h1[contains(text(),'search results')]")).Text);
            var movies = driver.FindElements(By.XPath("//ul[@class='search-results']//li//div[@class='title']"));
            foreach(var m in movies)
            {
                if (m.GetAttribute("innerText").Contains("1972"))
                {
                    m.FindElement(By.TagName("a")).Click();
                    break;
                }
                throw new Exception("Not found");
            }
            Thread.Sleep(2000);
            Assert.That(driver.FindElement(By.XPath("//span[@class='header-movie-genres']/a")).Text.Equals("Crime"), Is.True);
            Assert.That(driver.FindElement(By.XPath("//span[contains(text(),'MPAA Rating')]/span")).Text.Equals("R"));
            driver.FindElement(By.PartialLinkText("Cast & Crew")).Click();
            Assert.That(driver.FindElement(By.ClassName("director_container")).GetAttribute("innerText").Trim()
                .Equals("Francis Ford Coppola"), Is.True);

            Assert.That(driver.FindElement(By.XPath("//div[@class='cast_container']/div[@class='cast_name artist-name']/a[text()='Al Pacino']/../following-sibling::div[1]")).GetAttribute("innerText").Trim()
    .Equals("Michael Corleone"), Is.True);

        }


        [Test]
        [Parallelizable]
        public void Question_5()
        {
            WebKeywords.Instance.Navigate(reportLogger, driver, "http://ata123456789123456789.appspot.com/");

            int a = 7, b = 4;
            WebKeywords.Instance.JSClick(reportLogger, driver, driver.FindElement(By.XPath("//*[text()='Euclid(-)']/../input")), "Euclid(-) button");
            WebKeywords.Instance.SetText(reportLogger,driver, driver.FindElement(By.Id("ID_nameField1")), "Name Field 1", a.ToString());
            WebKeywords.Instance.SetText(reportLogger,driver, driver.FindElement(By.Id("ID_nameField2")), "Name Field 2", b.ToString());
            WebKeywords.Instance.JSClick(reportLogger,driver, driver.FindElement(By.Id("ID_calculator")), "Calculate Button");
            int ans = (a * a) - 2 * (a * b) + (b * b);
            WebKeywords.Instance.VerifyTextInTextBox(reportLogger, driver, driver.FindElement(By.Id("ID_nameField3")), "Results Field", ans.ToString());
        }

    }
}
