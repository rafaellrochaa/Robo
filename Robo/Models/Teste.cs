using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Robo.Models
{
    public class Teste
    {

        public static string DoPost()
        {
            CookieCollection cookies = new CookieCollection();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.facebook.com/login.php?login_attempt=1");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);

            //Get the response from the server and save the cookies from the first request..
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cookies = response.Cookies;

            //Request Post
            string getUrl = "https://www.facebook.com/login.php?login_attempt=1";
            string postData = String.Format("email={0}&pass={1}", "rafasp_2006@hotmail.com", "leafar270489");
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
            getRequest.CookieContainer = new CookieContainer();
            getRequest.CookieContainer.Add(cookies); //recover cookies First request
            getRequest.Method = WebRequestMethods.Http.Post;
            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            getRequest.AllowWriteStreamBuffering = true;
            getRequest.ProtocolVersion = HttpVersion.Version11;
            getRequest.AllowAutoRedirect = true;
            getRequest.ContentType = "application/x-www-form-urlencoded";

            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            getRequest.ContentLength = byteArray.Length;
            Stream newStream = getRequest.GetRequestStream(); //open connection
            newStream.Write(byteArray, 0, byteArray.Length); // Send the data.

            newStream.Close();

            HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
            cookies = getResponse.Cookies;
            string sourceCode;
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                sourceCode = sr.ReadToEnd();
            }

            //Get the token
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://www.facebook.com/dialog/oauth?client_id=282892925078054&redirect_uri=https://www.facebook.com/&response_type=token");
            getRequest.CookieContainer = new CookieContainer();
            getRequest.CookieContainer.Add(cookies);
            webRequest.AllowAutoRedirect = false;
            HttpWebResponse rresponse = (HttpWebResponse)webRequest.GetResponse();

            if (rresponse.StatusCode == HttpStatusCode.Redirect)
            {
                Console.WriteLine("redirected to: " + rresponse.GetResponseHeader("Location"));
            }
            return sourceCode;
        }
    }
}
