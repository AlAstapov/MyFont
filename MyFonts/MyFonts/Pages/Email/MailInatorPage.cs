using MyFonts.Pages.Email;
using OpenQA.Selenium;

namespace MyFonts.Pages
{
   public class MailInator : AbstractPage
    {
        private static string url = "https://www.mailinator.com/";

        public By InsertEmailBoxLocator = By.XPath("//input[@id='inboxfield']");
        public By GoButtonLocator = By.XPath("//button[@class='btn btn-dark']");

        private IWebElement InsertEmailBoxElement => FindElement(InsertEmailBoxLocator);
        private IWebElement GoButtonElement => FindElement(GoButtonLocator);

        public MailInator(IWebDriver driver)
        {
            Driver = driver;
            Driver.Navigate().GoToUrl(url);
        }

        public EmailsWindow OpenEmailsWindow(string email)
        {
            InsertEmailBoxElement.SendKeys(email);
            GoButtonElement.Click();
            return new EmailsWindow(Driver);
        }
    }
}
