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
    public class UITests : IDisposable
    {
        private readonly IWebDriver driver;


        public UITests()
        {

            driver = new ChromeDriver();
            Login();
        }
        private void Login()
        {
            driver.Navigate().GoToUrl("https://localhost:7199/Identity/Account/Login");

            driver.FindElement(By.Id("Input_Email")).SendKeys("");
            driver.FindElement(By.Id("Input_Password")).SendKeys("");
            driver.FindElement(By.Id("login-submit")).Click();
        }

        [Fact]
        public void Login_Test()
        {
            driver.Navigate().GoToUrl("https://localhost:7199/Identity/Account/Login");

            driver.Navigate().GoToUrl("https://localhost:7199/Identity/Account/Login");

            driver.FindElement(By.Id("Input_Email")).SendKeys("unittest@example.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Qwerty123!");
            driver.FindElement(By.Id("login-submit")).Click();

            Thread.Sleep(1000);
            Assert.Contains("Logout", driver.PageSource);
        }

        [Fact]
        public void LanguageSelector_ChangesLanguage()
        {
            driver.Navigate().GoToUrl("https://localhost:7199/");

            var languageSelect = driver.FindElement(By.Name("culture"));

            var selectElement = new SelectElement(languageSelect);
            selectElement.SelectByValue("pl-PL");

            Thread.Sleep(1500); 

            Assert.Contains("Prywatność", driver.PageSource);
        }

        [Fact]
        public void Car_CRUD_Test()
        {
            string brand = "TestBrand";
            string model = "TestModel";
            

            // ==== CREATE ====
            driver.Navigate().GoToUrl("https://localhost:7199/Cars/AddCar");
            Thread.Sleep(1000); 

            driver.FindElement(By.Id("Brand")).SendKeys(brand);
            driver.FindElement(By.Id("CarModel")).SendKeys(model);

            var input = driver.FindElement(By.Id("YearOfProduction"));
            input.Clear();
            input.SendKeys("2020");

            driver.FindElement(By.Id("RegistrationNumber")).SendKeys("TEST1234");

            var statusDropdown = new SelectElement(driver.FindElement(By.Id("Status")));
            statusDropdown.SelectByIndex(2);

            input = driver.FindElement(By.Id("PricePerDay"));
            input.Clear();
            input.SendKeys("100");
            input = driver.FindElement(By.Id("EngineCapacity"));
            input.Clear();
            input.SendKeys("1.6");
            input = driver.FindElement(By.Id("EnginePower"));
            input.Clear();
            input.SendKeys("120");

            IWebElement submitBtn = driver.FindElement(By.CssSelector("input[type='submit']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitBtn);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submitBtn);



            var createdCarRow = driver.FindElements(By.TagName("tr"))
                .FirstOrDefault(row => row.Text.Contains(brand));
            Assert.NotNull(createdCarRow);

            // ==== UPDATE ====
            var editLink = createdCarRow.FindElements(By.TagName("a"))
                .FirstOrDefault(a => a.Text.Contains("Edit"));
            Assert.NotNull(editLink);
            editLink.Click();

            var modelInput = driver.FindElement(By.Id("RegistrationNumber"));
            modelInput.Clear();
            modelInput.SendKeys("NewRegNumber123");

            submitBtn = driver.FindElement(By.CssSelector("input[type='submit']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitBtn);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submitBtn);

            Assert.Contains("NewRegNumber123", driver.PageSource);

            // ==== DELETE ====
  

            var updatedCarRow = driver.FindElements(By.TagName("tr"))
                .FirstOrDefault(row => row.Text.Contains("NewRegNumber123"));
            Assert.NotNull(updatedCarRow);

            var deleteLink = updatedCarRow.FindElements(By.TagName("a"))
                .FirstOrDefault(a => a.Text.Contains("Delete"));
            Assert.NotNull(deleteLink);
            deleteLink.Click();


            IWebElement deleteBtn = driver.FindElement(By.CssSelector("button[type='submit']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteBtn);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", deleteBtn);

            Assert.DoesNotContain("NewRegNumber123", driver.PageSource);
        }


       
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}