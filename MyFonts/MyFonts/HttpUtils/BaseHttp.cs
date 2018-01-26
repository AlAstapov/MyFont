using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;

namespace MyFonts.HttpUtils
{
   public class BaseHttp
    {
        protected  string PostUrl ;
        protected  string GetUrl ;
        protected  string RefererUrl;
        protected  string ParametrsToRequest = "";
        protected  Cookie[] Cookies;
        protected  string CookieParamsToLogin;
        protected  string Responce;

        protected  HttpWebRequest ExecutePostRequest(string paramsToRequest, Dictionary<string, string> headers, Dictionary<string, string> properties)
        {
            string formParams = paramsToRequest;
            byte[] byteArray = Encoding.ASCII.GetBytes(formParams);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(properties["url"]);
            request.Method = "POST";
            request.ContentType = properties["ContentType"];
            request.Referer = properties["Referer"];
            request.AllowAutoRedirect = Convert.ToBoolean(properties["AllowAutoRedirect"]);
            request.ContentLength = byteArray.Length;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
          using (var stream = request.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
            }
            return request;
        }

        protected HttpWebRequest ExecuteGetRequest(Dictionary<string, string> headers,
            Dictionary<string, string> properties)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(properties["Url"]);
            request.Accept = properties["Accept"];
            request.Method = "GET";
            request.ContentType = properties["ContentType"];
            request.Referer = properties["Referer"];
            foreach (var head in headers)
            {
                request.Headers.Add(head.Key, head.Value);
            }
            request.AllowAutoRedirect = Convert.ToBoolean(properties["AllowAutoRedirect"]);
            return request;
        }

        protected string GetResponce(HttpWebRequest executePostRequest)
        {
            using (HttpWebResponse responce = (HttpWebResponse)executePostRequest.GetResponse())
            {
               using (StreamReader streamReader = new StreamReader(responce.GetResponseStream(), Encoding.UTF8))
                {
                    Responce = streamReader.ReadToEnd();
                }
            }
            return Responce;
        }

        protected string CreateCookieString(IWebDriver driver)
        {
            string cookieParams = "";
            Cookies = driver.Manage().Cookies.AllCookies.ToArray();
            foreach (var cookie in Cookies)
            {
                cookieParams += string.Format("{0},{1} ", cookie.Name, cookie.Value);
            }
            return cookieParams;
        }
    }
}
