using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;    // I didn't put it first, no issue

namespace FreeToGameExplorer.E2E
{
    [TestFixture]
    public class GameExplorerTests
    {
        // 7060 is actual address for this app, 5168 - if failing. Add two address if no other options.
        private const string BaseUrl = "https://localhost:7060/";
        private IWebDriver _driver = null!;
        private WebDriverWait _wait = null!;



        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                _driver?.Quit();
            }
            catch
            {
                // ignore (maybe I will fix later)
            }

            if (_driver is IDisposable d)
            {
                d.Dispose();
            }
            _driver = null!;
        }

        [Test]
        public void HomePage_Loads_And_Shows_AtLeastOneGame()                       //one game needed at least
        {
            _driver.Navigate().GoToUrl(BaseUrl);
            // page header here
            _wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//*[contains(text(),'Free*To*Play Games Explorer')]")));
            // card of the game here
            _wait.Until(ExpectedConditions.ElementExists(
                By.CssSelector("[data-testid='game-card']")));

            var cards = _driver.FindElements(By.CssSelector("[data-testid='game-card']"));
            Assert.That(cards.Count, Is.GreaterThan(0), "Should show at least one game card.");

            // genres here
            var chart = _driver.FindElement(By.CssSelector("[data-testid='genre-chart-card']"));
            Assert.That(chart.Displayed, Is.True);
        }

        [Test]
        public void TitleValidation_Shows_Error_When_TooShort()
        {
            _driver.Navigate().GoToUrl(BaseUrl);

            var titleInput = _wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("[data-testid='title-input']")));

            var searchButton = _driver.FindElement(
                By.CssSelector("[data-testid='search-button']"));

            titleInput.Clear();
            titleInput.SendKeys("ab");   //more that 2 letters
            searchButton.Click();

            var validation = _wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("[data-testid='validation-message']")));

            Assert.That(validation.Text.Trim(),
                Is.EqualTo("Title must be more than 3 characters long!!!"));
        }

        [Test]
        public void Can_Search_And_Navigate_To_Details()
        {
            _driver.Navigate().GoToUrl(BaseUrl);

            var titleInput = _wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("[data-testid='title-input']")));
            var searchButton = _driver.FindElement(
                By.CssSelector("[data-testid='search-button']"));

            titleInput.Clear();
            titleInput.SendKeys("war");
            searchButton.Click();

            _wait.Until(ExpectedConditions.ElementExists(
                By.CssSelector("[data-testid='game-card']")));

            var firstDetails = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector("[data-testid='details-link']")));

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", firstDetails);

            // handle exeptions here
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].click();", firstDetails);

            //test game if here
            _wait.Until(d =>
            {
                var url = d.Url;
                return Regex.IsMatch(url, "/game/\\d+");
            });

            var detailsTitle = _wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("[data-testid='details-title']")));

            Assert.That(detailsTitle.Displayed, Is.True);
        }
    }
}
// Need to explain in  my papers that playwright wasn't working, so ended up with selenium and unit