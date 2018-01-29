using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace MyFonts
{
    public abstract class AbstractPage
    {
        protected  IWebDriver Driver;
        public static WebDriverWait Wait;

        public IWebElement FindElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return Driver.FindElement(locator);
        }

        public void SwithToFrame(string xpath)
        {
            IWebElement frame = FindElement(By.XPath(xpath));
            Driver.SwitchTo().Frame(frame);
        }
       
    }
}
