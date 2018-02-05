using MyFonts.Elements;
using MyFonts.Pages.Email;
using OpenQA.Selenium;

namespace MyFonts.Pages
{
    public class MailInator : AbstractPage
    {
        public static string url = "https://www.mailinator.com/";
        private TextBox InsertEmailBox = new TextBox(By.XPath("//input[@id='inboxfield']"), "InsertEmailBox");
        private Button GoButton = new Button(By.XPath("//button[@class='btn btn-dark']"), "Fo Button");

        public MailInator()
        {
        }

        public EmailsWindow OpenEmailsWindow(string email)
        {
            InsertEmailBox.SendKeys(email);
            GoButton.Click();
            return new EmailsWindow();

        }
    }
}
