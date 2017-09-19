using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Security;
using System.Reflection;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace WinYatang
{
    public class WebHttpHeper
    {
        public static string GetRequest(string url, ref CookieContainer cookie)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            if (cookie == null)
            {
                cookie = new CookieContainer();
            }
            request.CookieContainer = cookie;

            //将请求的结果发送给客户端(界面、应用)
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            cookie.Add(response.Cookies);
            string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            };
        }

        public static string PostJSONRequest(string postData, string headerURi, string url, ref CookieContainer cookie)
        {
            return PostRequest(postData, headerURi, url, "json", ref cookie);
        }

        public static string PostFormRequest(string postData, string headerURi, string url, ref CookieContainer cookie)
        {
            return PostRequest(postData, headerURi, url, "application/x-www-form-urlencoded; charset=UTF-8", ref cookie);
        }

        private static string PostRequest(string postData, string headerURi, string url, string contentType, ref CookieContainer cookie)
        {
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(postData);

                HttpWebRequest request = null;
                //如果是发送HTTPS请求
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                    request = (HttpWebRequest)WebRequest.Create(url);

                MethodInfo priMethod = request.Headers.GetType().GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
                priMethod.Invoke(request.Headers, new object[] { HttpRequestHeader.Referer.ToString(), headerURi });

                request.Accept = "*/*";
                request.Method = "POST";
                request.ContentType = contentType;
                request.ContentLength = data.Length;
                if (cookie == null)
                {
                    cookie = new CookieContainer();
                }

                request.CookieContainer = cookie;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cookie.Add(response.Cookies);

                string strcrook = request.CookieContainer.GetCookieHeader(new Uri(frm_Login.PageURL_Base));
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException)
            {
                return "{}";
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }
    }
}
