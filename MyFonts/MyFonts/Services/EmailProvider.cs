
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MyFonts.Services
{
   public class EmailProvider

    {
        private By emailLocator = By.XPath("//input[@id='mailAddress']");
        private string url = "https://10minutemail.com/10MinuteMail";
        private static IWebDriver driver;

        private void openEmailProvider()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
        }

        private void closeEmailProvider()
        {
            driver.Dispose();
        }

        public string GetEmail()
        {
            string email;
            openEmailProvider();
            email = driver.FindElement(emailLocator).GetAttribute("value");
            closeEmailProvider();
            return email;
        }

    }
}
