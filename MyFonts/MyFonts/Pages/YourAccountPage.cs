using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyFonts.Pages
{
    public class YourAccountPage : AbstractPage
    {
        private By LableLocator = By.XPath("//ul[@id='headcrumbs']");
        
        public By UsernameLocator = By.XPath("//span[@id='headerLoginUsername']");
        public By LogOutLocator = By.XPath("//a[@id='signOut']");

        private IWebElement UserNameElement => FindElement(UsernameLocator);
        

        public YourAccountPage(IWebDriver driver)
        {
            Driver = driver;
            FindElement(LableLocator);
        }

        public string GetUsername()
        {
            return UserNameElement.Text;
        }

        public void SignOut()
        {
         UserNameElement.Click();
         FindElement(LogOutLocator).Click();
        }

    }
}
