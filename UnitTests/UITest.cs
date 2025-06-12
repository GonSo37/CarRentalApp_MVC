using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class UITest: IDisposable
    {
        private readonly IWebDriver driver;
        public UITest()
        {
            driver = new ChromeDriver();
            Login();
        }
        private void Login()
        {
            driver.Navigate().GoToUrl("https://localhost:7199/Identity/Account/Login");

            driver.FindElement(By.Id("Input_Email")).SendKeys("unittest@example.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Qwerty123!");
            driver.FindElement(By.Id("login-submit")).Click();
        }

        [Fact]
        public void Create_GET_ReturnsCreateView()
        {

            driver.Navigate().GoToUrl("https://localhost:7199/Cars/AddCar");

            Assert.Contains("Brand", driver.PageSource);
            Assert.Contains("Model", driver.PageSource);
            Assert.Contains("YearOfProduction", driver.PageSource);

            IWebElement submitBtn = driver.FindElement(By.CssSelector("input[type='submit']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitBtn);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submitBtn);

            Assert.Contains("The Brand field is required", driver.PageSource);
            Assert.Contains("The CarModel field is required", driver.PageSource);
        }

        [Fact]
        public void Create_POST()
        {
            driver.Navigate().GoToUrl("https://localhost:7199/Cars/AddCar");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Brand")).SendKeys("TestBrand");
            driver.FindElement(By.Id("CarModel")).SendKeys("TestModel");
            var input = driver.FindElement(By.Id("YearOfProduction"));
            input.Clear();
            input.SendKeys("2020");
            driver.FindElement(By.Id("RegistrationNumber")).SendKeys("TEST1234");

            var statusDropdown = new SelectElement(driver.FindElement(By.Id("Status")));
            statusDropdown.SelectByIndex(2);

            driver.FindElement(By.Id("PricePerDay")).SendKeys("100");
            driver.FindElement(By.Id("EngineCapacity")).SendKeys("1.6");
            driver.FindElement(By.Id("EnginePower")).SendKeys("120");

            IWebElement submitBtn = driver.FindElement(By.CssSelector("input[type='submit']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitBtn);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submitBtn);

            Assert.DoesNotContain("The Brand field is required", driver.PageSource);
            Assert.Contains("TestBrand", driver.PageSource);
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
