using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using MyFonts.BusinessObjects;
using MyFonts.HttpUtils;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;

using System;

namespace MyFonts
{
    public class HttpLogin : BaseHttp
    {
        public string LogInWithUser(string Locale, string environment,IWebDriver driver, User user)
        {
            URL = string.Format("https://firmcentral{0}.{1}.westlaw.com/", Locale, environment);

            string requestVerificationToken = "__RequestVerificationToken";
            string requestVerificationTokenValueFromPageSource = "";
            string requestVerificationTokenValueFromResponceWhileOPenPage = "";

            string COSIOsession = "COSISOSession";
            string COSIOsessionValue = "";
                       
            driver.Navigate().GoToUrl(URL);
            CookieParamsToLogin = CreateCookieString(driver);
            var driverCookies = driver.Manage().Cookies;
            requestVerificationTokenValueFromResponceWhileOPenPage = driverCookies.GetCookieNamed(requestVerificationToken).Value;
            string pageSource = driver.PageSource;
            string patternForRequestVerificationToken = requestVerificationToken + "\"\\s[\\S]*\\svalue=\"([^\"]*)";
            var matchForRequestVerificationToken = new Regex(patternForRequestVerificationToken).Matches(pageSource);
            if (matchForRequestVerificationToken.Count > 0) requestVerificationTokenValueFromPageSource = matchForRequestVerificationToken[0].Groups[1].Value;
            COSIOsessionValue = driverCookies.GetCookieNamed(COSIOsession).Value;
            ParametrsToRequest = string.Format("username={0}&overrideCaptchaFlags=False&captchaIsAlreadyDisplayed=false&{1}={2}",
                user.Name, requestVerificationToken, requestVerificationTokenValueFromPageSource);

            // Captcha request
            PostUrl = "https://signon.qa.thomsonreuters.com/v2/captchasrm/check/username/";
            RefererUrl = driver.Url;

            HttpWebRequest postRequest = ExecutePostRequest(ParametrsToRequest, new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                new Dictionary<string, string>
                {
                     {"url", PostUrl},
                     {"ContentType", "application/x-www-form-urlencoded; charset=UTF-8"},
                     {"Referer", RefererUrl},
                     {"AllowAutoRedirect", "true"},
                     {"Accept","*/*"}
                       });
             var postResponce = (HttpWebResponse)postRequest.GetResponse();

            //COSIOSession is changed by captcha
            var setCookie = postResponce.Headers["Set-Cookie"];
            COSIOsessionValue = getCookieValueFromResponceHeader(setCookie, COSIOsession);
            deleteCookieByName(COSIOsession, driver);
            addCookie(COSIOsession, COSIOsessionValue, driver);


            //request to get login link
            CookieParamsToLogin = string.Format("{0}={1}; {2}={3}", requestVerificationToken, requestVerificationTokenValueFromResponceWhileOPenPage, COSIOsession, COSIOsessionValue);
            var patternForSiteKey = "SiteKey\"\\s[\\S]*\\svalue=\"([^\"]*)";
            string SiteKey = "";
            var matchForSiteKey = new Regex(patternForSiteKey).Matches(pageSource);
            if (matchForSiteKey.Count > 0) SiteKey = matchForSiteKey[0].Groups[1].Value;
            int minutesToMidnight = 24 * 60 - DateTime.Now.Hour * 60 - DateTime.Now.Minute;
            ParametrsToRequest = string.Format("{0}={1}&IsCDNAvailable=False&MinutesToMidnight={2}&Username={3}&Password={4}&Password-clone={4}&SaveUsername=false&SaveUsernamePassword=false&RememberMeToday=false&SiteKey={5}&CultureCode=en&OverrideCaptchaFlags=False&recaptcha_response_field=&SignIn=submit", requestVerificationToken, requestVerificationTokenValueFromPageSource, minutesToMidnight, user.Name, user.Password, SiteKey);
            PostUrl = RefererUrl = driver.Url;
            postRequest = ExecutePostRequest(ParametrsToRequest, new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                new Dictionary<string, string>
                {
                     {"url", PostUrl},
                     {"ContentType", "application/x-www-form-urlencoded"},
                     {"Referer", RefererUrl},
                     {"AllowAutoRedirect", "false"},
                     {"Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"},
                     {"Host","signon.qa.thomsonreuters.com" },
                     {"Origin","https://signon.qa.thomsonreuters.com" },
                     {"Cache-Control","max-age=0" },
                     {"Upgrade-Insecure-Requests","1" },
                     {"Accept-Encoding","gzip, deflate, br" },
                     {"Accept-Language","en-US,en;q=0.9" }
                       });
             postResponce = (HttpWebResponse)postRequest.GetResponse();
                                 
            return postResponce.GetResponseHeader("Location");
           
                }
        
          public string getCookieValueFromResponceHeader(string AllCookies, string cookieName)
        {
            string valueToReturn = "";
            string pattern = "=([^;]*)";
            var matchForBigIpServer = new Regex(cookieName + pattern).Matches(AllCookies);
            if (matchForBigIpServer.Count > 0) valueToReturn = matchForBigIpServer[0].Groups[1].Value;
            return valueToReturn;
        }
        public Cookie getCookieByName(string name, IWebDriver driver)
        {
            return driver.Manage().Cookies.GetCookieNamed(name);
        }
        public void deleteCookieByName( string name, IWebDriver driver)
        {
            driver.Manage().Cookies.DeleteCookie(getCookieByName(name, driver));
        }
        public void addCookie(string name, string value, IWebDriver driver)
        {
            driver.Manage().Cookies.AddCookie(new Cookie(name, value));
        }
    }
}

