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
            string BIGipServershared_apache = string.Format("BIGipServer{0}shared_apache_b", environment);
            string BIGipServershared_apache_bValue;

          //  switch (environment)
          //  {
             //   case "ci":
               //     BIGipServershared_apache = string.Format("BIGipServer{0}shared_apache_b", environment);
               //     break;
              //  case "demo":
                 //   BIGipServershared_apache = string.Format("BIGipServer{0}shared_apache_pc1", environment);
                 //   break;
               // case "qed":
                  //  BIGipServershared_apache = string.Format("BIGipServer{0}shared_apache_a", environment);
                 //   break;
               // default:
                   // throw new Exception();
           //  }


            string web_pm = "web_pm";
            string web_pmValue;

            string site = "site";
            string siteValue;

            string ig = "ig";
            string igValue;

            string requestVerificationToken = "__RequestVerificationToken";
            string requestVerificationTokenValueFromPageSource = "";
            string requestVerificationTokenValueFromResponceWhileOPenPage = "";

            string COSIOsession = "COSISOSession";
            string COSIOsessionValue = "";

            string Co_SessionToken = "Co_SessionToken";
            string Co_SessionTokenValue;

            string Web_SessionId = "Web_SessionId";
            string Web_SessionIdValue;

            //reuqest  to get web_pmValue, BIGipServershared_apache_bValue, siteValue, igValue
             GetUrl = URL;
             CurrentGetRequest = ExecuteGetRequest(new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                 new Dictionary<string, string>
                 {
                     {"url", GetUrl},
                     {"Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
                     {"AllowAutoRedirect", "false"},
                        });
            CurrnetGetResponce = (HttpWebResponse)CurrentGetRequest.GetResponse();
            var getResponceSetCookies = CurrnetGetResponce.Headers["Set-Cookie"];
            BIGipServershared_apache_bValue = getCookieValueFromResponceHeader(getResponceSetCookies, BIGipServershared_apache);
            web_pmValue = getCookieValueFromResponceHeader(getResponceSetCookies, web_pm);
            siteValue = getCookieValueFromResponceHeader(getResponceSetCookies, site);
            igValue = getCookieValueFromResponceHeader(getResponceSetCookies, ig);
                 
            driver.Navigate().GoToUrl(URL);
            CookieParamsToLogin = CreateCookieString(driver);
            var driverCookies = driver.Manage().Cookies;
            requestVerificationTokenValueFromResponceWhileOPenPage = driverCookies.GetCookieNamed(requestVerificationToken).Value;
            string pageSource = driver.PageSource;
            string patternForRequestVerificationToken = requestVerificationToken + "\"\\s[\\S]*\\svalue=\"([^\"]*)";
            var matchForRequestVerificationToken = new Regex(patternForRequestVerificationToken).Matches(pageSource);
            if (matchForRequestVerificationToken.Count > 0) requestVerificationTokenValueFromPageSource = matchForRequestVerificationToken[0].Groups[1].Value;
            COSIOsessionValue = driverCookies.GetCookieNamed(COSIOsession).Value;
           
            // Captcha request
            PostUrl = "https://signon.qa.thomsonreuters.com/v2/captchasrm/check/username/";
            RefererUrl = driver.Url;
            ParametrsToRequest = string.Format("username={0}&overrideCaptchaFlags=False&captchaIsAlreadyDisplayed=false&{1}={2}",
               user.Name, requestVerificationToken, requestVerificationTokenValueFromPageSource);
            CurrentPostRequest = ExecutePostRequest(ParametrsToRequest, new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                new Dictionary<string, string>
                {
                     {"url", PostUrl},
                     {"ContentType", "application/x-www-form-urlencoded; charset=UTF-8"},
                     {"Referer", RefererUrl},
                     {"AllowAutoRedirect", "false"},
                     {"Accept","*/*"}
                       });
            CurrnetPostResponce = (HttpWebResponse)CurrentPostRequest.GetResponse();

            //COSIOSession is changed by captcha
            var setCookie = CurrnetPostResponce.Headers["Set-Cookie"];
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
            CurrentPostRequest = ExecutePostRequest(ParametrsToRequest, new Dictionary<string, string>
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
             CurrnetPostResponce = (HttpWebResponse)CurrentPostRequest.GetResponse();              
             string loginWithUserLink = CurrnetPostResponce.GetResponseHeader("Location");

            //request to get Co_SessionToken, Web_SessionId
            CookieParamsToLogin = string.Format("{0}={1}; {2}={3}; {4}={5}; {6}={7}; UserSettingsLocale=locale=en-CA; tr_privacy_policy_banner=3"
                , BIGipServershared_apache, BIGipServershared_apache_bValue, site, siteValue,ig, igValue, web_pm, web_pmValue);
            GetUrl = loginWithUserLink;

             CurrentGetRequest = ExecuteGetRequest(new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                 new Dictionary<string, string>
                 {
                    // {"Host",URL },
                     {"url", GetUrl},
                     {"Cache-Control","max-age=0" },
                     {"Upgrade-Insecure-Requests","1" },
                     {"Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
                     {"AllowAutoRedirect", "false"},
                     {"Accept-Encoding","gzip, deflate, br" },
                     {"Accept-Language","en-US,en;q=0.9" }
                        });
            CurrnetGetResponce = (HttpWebResponse)CurrentGetRequest.GetResponse();
            getResponceSetCookies = CurrnetGetResponce.Headers["Set-Cookie"];
            Co_SessionTokenValue = getCookieValueFromResponceHeader(getResponceSetCookies, Co_SessionToken);
            Web_SessionIdValue = getCookieValueFromResponceHeader(getResponceSetCookies, Web_SessionId);

            //reuqest to get link for login with admin
            CookieParamsToLogin = string.Format("{0}={1}; {2}={3}; {4}={5}; {6}={7}", BIGipServershared_apache, BIGipServershared_apache_bValue, site, siteValue,
                ig, igValue, web_pm, web_pmValue);
            GetUrl = URL+"/session/admin?returnTo=wb/admin";
            RefererUrl = URL;
            CookieParamsToLogin += string.Format("; {0}={1}; {2}={3}; UserSettingsLocale=locale=en-CA; tr_privacy_policy_banner=2", Co_SessionToken, Co_SessionTokenValue,
                Web_SessionId, Web_SessionIdValue);

            CurrentGetRequest = ExecuteGetRequest(new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                new Dictionary<string, string>
                {
                     {"url", GetUrl},
                     {"Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
                     {"Referer",URL},
                     {"AllowAutoRedirect", "false"},
                     {"Accept-Encoding","gzip, deflate, br" },
                     {"Accept-Language","en-US,en;q=0.9" }
                       });
            CurrnetGetResponce = (HttpWebResponse)CurrentGetRequest.GetResponse();
            string loginWithAdminLink = CurrnetPostResponce.GetResponseHeader("Location");
            return loginWithAdminLink;
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

