using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using MyFonts.BusinessObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyFonts.Pages.Email
{
   public class EmailsWindow : AbstractPage
   {
   public By EmailsLocator = By.XPath("//li[@class='all_message-item all_message-item-parent cf ng-scope']//div[@title='FROM']");
      
     public EmailsWindow(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool IsMessageFromEmailPresentOnPage(string fromEmail)
        {
            bool isEmailPresent = false;
           ReadOnlyCollection<IWebElement> emails = Driver.FindElements(EmailsLocator);
           foreach (IWebElement email in emails)
            {
                email.Click();
                Letter letter = new Letter(Driver);
                isEmailPresent = checkCurrentLetterThatTheNeddedLetterRecived(fromEmail, letter);
                if(isEmailPresent) break;
            }
            return isEmailPresent;
        }

       private bool checkCurrentLetterThatTheNeddedLetterRecived(string fromEmail,Letter letter)
       {
           letter.ChoseJsonMessageFromat();
           string letterTextXpath = string.Format("//body[contains(.,'{0}')]", fromEmail);
           SwithToFrame("//iframe[@id='msg_body']");
           Thread.Sleep(5000);
           bool isPresent = Driver.FindElements(By.XPath(letterTextXpath)).Count > 0;
           Driver.SwitchTo().ParentFrame();
           letter.DeleteLetter();
           return isPresent;
       }
    }
}
