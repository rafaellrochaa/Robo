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