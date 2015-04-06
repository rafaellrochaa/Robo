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
    static public class WR2
    {
        static public string DoRequest(String url)
        {
            //cria uma requisição para a url
            WebRequest rq = WebRequest.Create(url);

            //obtem a resposta
            HttpWebResponse rp = (HttpWebResponse)rq.GetResponse();
            
            //obtém um stream contendo a resposta retornada pelo servidor
            Stream ds = rp.GetResponseStream();

            //cria um StreamReader para leitura
            StreamReader rd = new StreamReader(ds);

            //LE O CONTEÚDO
            string ct = rd.ReadToEnd();

            rd.Close();
            ds.Close();
            rp.Close();

            return ct;
        }

        static public string DoPost(string urlGetAutenticacao, string UrlPost, NameValueCollection parametros)
        {
            using (var client = new CookieAwareWebClient())
            {
                var response = client.UploadValues(urlGetAutenticacao, parametros);
                var result = client.DownloadData(UrlPost);
                return Encoding.UTF8.GetString(result);
            }
        }

        public class CookieAwareWebClient : WebClient
        {
            public CookieAwareWebClient()
            {
                CookieContainer = new CookieContainer();
            }
            public CookieContainer CookieContainer { get; private set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = (HttpWebRequest)base.GetWebRequest(address);
                
                request.CookieContainer = CookieContainer;
                return request;
            }
        }

    }
}