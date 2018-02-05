using System.Collections.ObjectModel;
using System.Threading;
using MyFonts.Elements;
using OpenQA.Selenium;

namespace MyFonts.Pages.Email
{
    public class EmailsWindow : AbstractPage
    {
        public By EmailsLocator = By.XPath("//li[@class='all_message-item all_message-item-parent cf ng-scope']//div[@title='FROM']");
        public bool IsMessageFromEmailPresentOnPage(string fromEmail)
        {
            bool isEmailPresent = false;
            ReadOnlyCollection<IWebElement> emails = DriverClass.FindElements(EmailsLocator);
            if (emails.Count == 0) return false;
            foreach (IWebElement email in emails)
            {
                email.Click();
                BusinessObjects.Email _email = new BusinessObjects.Email();
                isEmailPresent = checkCurrentLetterThatTheNeddedLetterRecived(fromEmail, _email);
                if (isEmailPresent) break;
            }
            return isEmailPresent;
        }

        private bool checkCurrentLetterThatTheNeddedLetterRecived(string fromEmail, BusinessObjects.Email email)
        {
            email.ChoseJsonMessageFromat();
            string letterTextXpath = string.Format("//body[contains(.,'{0}')]", fromEmail);
            DriverClass.SwithToFrame("//iframe[@id='msg_body']");
            bool isPresent = DriverClass.FindElements(By.XPath(letterTextXpath)).Count > 0;
            DriverClass.SwitchToDefaultContent();
            email.DeleteLetter();
            return isPresent;
        }
    }
}
