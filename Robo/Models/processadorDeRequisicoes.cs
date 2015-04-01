using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Specialized;
using System.Net;

namespace Robo.Models
{
    public class processadorDeRequisicoes : WebClient
    {
        public void Login(string loginPageAddress, NameValueCollection loginData)
        {
            CookieContainer container;

            var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //Bufferizando dados do Post e armazenando na requisicao
            var buffer = Encoding.ASCII.GetBytes(loginData.ToString());
            request.ContentLength = buffer.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();

            //Guardando no cookie de autenticação no container
            container = request.CookieContainer = new CookieContainer();

            var response = request.GetResponse();
            response.Close();
            CookieContainer = container;
        }

        public processadorDeRequisicoes(CookieContainer container)
        {
            CookieContainer = container;
        }

        public processadorDeRequisicoes()
            : this(new CookieContainer())
        { }

        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }

    }
}
