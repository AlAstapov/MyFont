﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyFonts.Elements
{
    class BaseElement
    {
        protected By Locator;
        protected string Title;
        protected IWebElement webElement;


        public BaseElement(By locator, string title)
        {
            Locator = locator;
            Title = title;
            DriverClass.Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            webElement = DriverClass.FindElement(locator);
        }

        public void Click()
        {
            webElement.Click();
        }

        public string GetText()
        {
            return webElement.Text;
        }

    }
}