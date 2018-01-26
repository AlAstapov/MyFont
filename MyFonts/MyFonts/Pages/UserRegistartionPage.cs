using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkCore;
using MyFonts.BusinessObjects;
using MyFonts.WorkWithFile;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace MyFonts.Pages
{
    public class UserRegistartionPage : AbstractPage
    {
        public By YourNameBoxLocator = By.XPath("//input[@name='newinfo[name]']");
        public By EmailAdressBoxLocator = By.XPath("//input[@name='newinfo[emailAddress]']");
        public By PasswordBoxLocator = By.XPath("//input[@name='newinfo[password]']");
        public By PasswordBoxAgaingLocator = By.XPath("//input[@name='newinfo[confirmPassword]']");
        public By CreateAccountButtonLocator = By.XPath("//input[@value='Create Account']");

        private IWebElement YourNameBoxElement => FindElement(YourNameBoxLocator);
        private IWebElement EmailAdressBoxElement => FindElement(EmailAdressBoxLocator);
        private IWebElement PasswordBoxElement => FindElement(PasswordBoxLocator);
        private IWebElement PasswordBoxAgaingElement => FindElement(PasswordBoxAgaingLocator);
        private IWebElement CreateAccountButtonElement => FindElement(CreateAccountButtonLocator);

        public UserRegistartionPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public YourAccountPage RegisterNewUser(User user,out bool isRegistred)
        {
            YourNameBoxElement.SendKeys(user.Name);
            EmailAdressBoxElement.SendKeys(user.Email);
            PasswordBoxElement.SendKeys(user.Password);
            PasswordBoxAgaingElement.SendKeys(user.Password);
            CreateAccountButtonElement.Click();

           YourAccountPage yourAccountPage = null;
           try
            {
                yourAccountPage = new YourAccountPage(Driver);
                isRegistred = isUserRegistred(user, yourAccountPage);
            }
            catch (WebDriverTimeoutException e)
            {
                isRegistred = false;
            }
            if (isRegistred)
            {
                FileWriter.WriteUser(user);
            }
            return yourAccountPage;
        }

        private bool isUserRegistred(User user,YourAccountPage yourAccountPage)
        {
            return user.Name == yourAccountPage.GetUsername();
        }
    }
}
