using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;

namespace Robo.Models
{
    static public class WR
    {
        static public string DoRequest(string url, NameValueCollection parametros)
        {
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(url);

            CookieContainer cookies = new CookieContainer();
            // Adiciona cookies
            rq.CookieContainer = cookies;

            //Dados do post
            var postData = new StringBuilder();
            foreach (string key in parametros.Keys)
            {
                postData.AppendFormat("{0}={1}&",
                HttpUtility.UrlEncode(key),
                HttpUtility.UrlEncode(parametros[key]));
            }
            postData.Length -= 1;

            //bufferizando os dados do post
            byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
            
            //Dados interceptados e adicionados ao cabeçaho 
            rq.Method = System.Net.WebRequestMethods.Http.Post;
            rq.Accept = "Accept: */*";
            rq.Headers.Add("Accept-Encoding: gzip, deflate");
            rq.Headers.Add("Accept-Language: pt-BR,pt;q=0.8,en-US;q=0.6,en;q=0.4");
            rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.101 Safari/537.36";
            rq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            rq.Headers.Add("Origin: http://www.bradescopromotoranet.com.br");
            rq.Referer = "http://www.bradescopromotoranet.com.br/forms/Home.aspx";
            rq.Headers.Add("X-MicrosoftAjax: Delta=true");
            rq.Host = "www.bradescopromotoranet.com.br";
            rq.Headers.Add("Cookie: ASP.NET_SessionId=3pik0f54vthv04x2wn4wz3so");
            rq.KeepAlive = true;
            rq.ServicePoint.Expect100Continue = false;
            rq.AllowAutoRedirect = true;
            
            //

            //informando para cabeçalho tamanho dos dados do post
            rq.ContentLength = byteArray.Length;

            //Gravando dados bufferizados na requisição
            Stream sr =  rq.GetRequestStream();
            sr.Write(byteArray, 0, byteArray.Length);
            sr.Close();
            //Obtendo a resposta do server
            HttpWebResponse rresponse = (HttpWebResponse)rq.GetResponse();

            //Pegando a stream de resposta para leitura
            StreamReader s = new StreamReader(rresponse.GetResponseStream());
            return s.ReadToEnd();

        }
    }
}