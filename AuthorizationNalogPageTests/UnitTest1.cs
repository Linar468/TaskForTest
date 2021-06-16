using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace AuthorizationNalogPageTests
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private readonly By _personalOfficeButton = By.XPath("//span[.='Физические']/ancestor::a/following-sibling::a[1]/descendant::span[.='Личный кабинет']");
        private readonly By _demoVersionButton = By.XPath("//a[.='Демо-версия']/ancestor::div[1]");
        private readonly By _profilePhoto = By.XPath("//a[@title='Профиль']");
        private readonly By _fullNameLabel = By.XPath("//a[@class[contains(.,'UserInfo-module__text')]]");
        private readonly By _sumLabel = By.XPath("//span[@class='nowrap main-page_title_sum']");
        private string mainPageTitle = "Федеральная налоговая служба";
        private string personalOfficePageTitle = "Личный кабинет налогоплательщика — физического лица";
        private string username = "Иванов Иван Иванович";
        private string defaultHeight = "31px";
        private string defaultWidth = "31px";

        [TestMethod]
        public void NalogTest()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.nalog.ru/");
            Assert.AreEqual(mainPageTitle, driver.Title);
            var goToPersonalOffice = driver.FindElement(_personalOfficeButton);
            goToPersonalOffice.Click();
            Assert.AreEqual(personalOfficePageTitle, driver.Title);
            var goToDemoVersion = driver.FindElement(_demoVersionButton);
            goToDemoVersion.Click();
            string fullName = driver.FindElement(_fullNameLabel).Text;
            Assert.AreEqual(username, fullName);
            var photo = driver.FindElement(_profilePhoto);
            var photoWidth = photo.GetCssValue("width");
            var photoHeight = photo.GetCssValue("height");
            Assert.AreEqual(defaultHeight, photoHeight);
            Assert.AreEqual(defaultWidth, photoWidth);
            var sum = driver.FindElement(_sumLabel).Text;
            sum = sum.Replace(" ", "");
            sum = sum.Remove(sum.Length - 3);
            Assert.IsTrue(int.Parse(sum) < 200000);
            driver.Close();
        }
    }
}
