using System;
using System.Collections.ObjectModel;
using System.Threading;
using MyFonts.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace MyFonts.Pages
{
   public class SignInDropMenu : AbstractPage
    {
        public static By iFrameLocator = By.XPath("//iframe[@class='dropdownLoginFrame']");
        private BaseElement signUp = new BaseElement(By.XPath("//a[@data-qe-id='signup']"),"Sing up");
        
        public SignInDropMenu()
        {
           
        }

        public UserRegistartionPage SingUp()
        {
           signUp.JsClick();
           return new UserRegistartionPage();
        }
      
    }
}
