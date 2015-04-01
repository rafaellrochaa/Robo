using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Robo.Models
{
    static public class WR
    {
        static public string DoRequest(string url, NameValueCollection parametros)
        {
            //cria uma requisição para a url
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(url);
            rq.CookieContainer = new CookieContainer(); // Adiciona cookie de autenticação a requisição
            rq.Method = "POST";

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
            rq.ContentType = "application/x-www-form-urlencoded";
            //informando para cabeçalho tamanho dos dados do post
            rq.ContentLength = byteArray.Length;

            //Gravando dados bufferizados na requisição
            Stream sr = rq.GetRequestStream();
            sr.Write(byteArray, 0, byteArray.Length);
            sr.Close();

            //Obtendo a resposta do server
            WebResponse response = rq.GetResponse();

            //Pegando a stream de resposta para leitura
            StreamReader s = new StreamReader(response.GetResponseStream());
            return s.ReadToEnd();

        }
    }
}