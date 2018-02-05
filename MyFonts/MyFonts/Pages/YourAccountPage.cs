using MyFonts.BusinessObjects;
using MyFonts.Elements;
using OpenQA.Selenium;

namespace MyFonts.Pages
{
    public class YourAccountPage : AbstractPage
    {
        private By LableLocator = By.XPath("//ul[@id='headcrumbs']");
        public By LogOutLocator = By.XPath("//a[@id='signOut']");
        private BaseElement UserNameElement = new BaseElement(By.XPath("//span[@id='headerLoginUsername']"), "UserNameElement");

        public YourAccountPage()
        {

        }

        public string GetUsername()
        {
            return UserNameElement.GetText();
        }

        public void SignOut()
        {
            UserNameElement.Click();
            DriverClass.FindElement(LogOutLocator).Click();
        }
        public bool IsUserLoggined(User user)
        {
            return user.Name == GetUsername();
        }
    }
}
