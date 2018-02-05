using MyFonts.Elements;
using OpenQA.Selenium;

namespace MyFonts.BusinessObjects
{
    public class Email : AbstractPage
    {
        private BaseElement contentTypeSelectElement = new BaseElement(By.XPath("//select[@id='contenttypeselect']"), "contentTypeSelectElement");
        private BaseElement jsonContentTypeElement = new BaseElement(By.XPath("//option[@value='json']"), "jsonContentTypeElement");
        private BaseElement EmailTextElement = new BaseElement(By.XPath("//iframe[@id='msg_body']"), "EmailTextElement");
        private Button DeleteEmaeilButton = new Button(By.XPath("//i[@class='fa fa-trash fa-stack-1x fa-inverse']"), "DeleteEmaeilButton");


        public void ChoseJsonMessageFromat()
        {
            bool staleElement = true;
            while (staleElement)
            {
                try
                {
                    contentTypeSelectElement.Click();
                    jsonContentTypeElement.Click();

                    staleElement = false;
                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
            }

        }

        public void DeleteLetter()
        {
            DeleteEmaeilButton.Click();
        }

        public string GetLetterText()
        {
            return EmailTextElement.GetText();
        }

    }
}
