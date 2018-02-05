using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyFonts.Elements
{
    public class DriverClass
    {
        private static IWebDriver Driver;
        public static WebDriverWait Wait;

        public static IWebElement FindElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return Driver.FindElement(locator);
        }

        public static void SwithToFrame(string xpath)
        {
            IWebElement frame = FindElement(By.XPath(xpath));
            Driver.SwitchTo().Frame(frame);
        }
        public static void SwithToFrame(By xpath)
        {
            IWebElement frame = FindElement(xpath);
            Driver.SwitchTo().Frame(frame);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        public static void SwitchToDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
        }

        public static void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }

        public static void SetWait(int seconds)
        {
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
        }

        public static void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public static void Dispose()
        {
            Driver.Dispose();
        }

        public static void GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public static IWebDriver GetDriver()
        {
            return Driver;
        }
    }
}
