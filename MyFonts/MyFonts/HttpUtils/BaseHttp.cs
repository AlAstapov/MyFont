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
        protected string PostUrl;
        protected string URL;
        protected string RefererUrl;
        protected string ParametrsToRequest = "";
        protected Cookie[] Cookies;
        protected string CookieParamsToLogin;
        protected string Token;
        protected string Responce;
        protected string Accept;

        protected HttpWebRequest ExecutePostRequest(string paramsToRequest, Dictionary<string, string> headers,
            Dictionary<string, string> properties)
        {
            string formParams = paramsToRequest;
            byte[] byteArray = Encoding.ASCII.GetBytes(formParams);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(properties["url"]);
            request.Method = "POST";
            if (properties.ContainsKey("Host")) request.Host = properties["Host"];
            request.ContentLength = byteArray.Length;
            if (properties.ContainsKey("Cache-Control")) request.Headers.Add("Cache-Control", properties["Cache-Control"]);
            if (properties.ContainsKey("Origin")) request.Headers.Add("Origin", properties["Origin"]);
            if (properties.ContainsKey("Upgrade-Insecure-Requests")) request.Headers.Add("Upgrade-Insecure-Requests", properties["Upgrade-Insecure-Requests"]);
            request.ContentType = properties["ContentType"];
            request.Accept = properties["Accept"];
            request.Referer = properties["Referer"];
            if (properties.ContainsKey("Accept-Encoding")) request.Headers.Add("Accept-Encoding", properties["Accept-Encoding"]);
            if (properties.ContainsKey("Accept-Language")) request.Headers.Add("Accept-Language", properties["Accept-Language"]);
            
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
          request.AllowAutoRedirect = Convert.ToBoolean(properties["AllowAutoRedirect"]);
         
          request.GetRequestStream().Write(byteArray, 0, byteArray.Length);
            return request;
        }

        protected HttpWebRequest ExecuteGetRequest(Dictionary<string, string> headers,
            Dictionary<string, string> properties)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(properties["url"]);
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

     

        protected string CreateCookieString(IWebDriver driver)
        {
            string cookieParams = "";
            Cookies = driver.Manage().Cookies.AllCookies.ToArray();
            foreach (var cookie in Cookies)
            {
                cookieParams += string.Format("{0}={1}; ", cookie.Name, cookie.Value);
            }
            cookieParams = cookieParams.Remove(cookieParams.LastIndexOf(";"));
            return cookieParams;
        }
    }
}
