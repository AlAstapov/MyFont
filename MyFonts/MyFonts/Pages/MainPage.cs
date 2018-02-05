

using MyFonts.Elements;
using OpenQA.Selenium;

namespace MyFonts.Pages
{
    public class MainPage : AbstractPage
    {
        private BaseElement SignInMenuPoint = new BaseElement(By.XPath("//a[@id='headerLoginLink']"), "Sign In Menu Point");

        public SignInDropMenu OpenDropMenu()
        {
              SignInMenuPoint.Click();
               DriverClass.SwithToFrame(SignInDropMenu.iFrameLocator);
              return new SignInDropMenu();
        }
    }
}
