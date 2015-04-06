using Robo.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Robo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Index()
        {
            string html = WR.DoRequest("http://www.dolarhoje.com.br/");
            string expressao = "stockup stockup_bigger";

            int posDolar = html.IndexOf(expressao);

            return html.Substring(posDolar + expressao.Length + 2, 5);


        }

        public string Login()
        {
            var Credenciais = new NameValueCollection();

            Credenciais.Add("usuario", "claudio");
            Credenciais.Add("senha", "ac@78902");

            return WR.DoPost("http://www.agilus.com.br/alvo", "http://www.agilus.com.br/alvo/Consultas", Credenciais);


        }

        public string IndexTeste()
        {
            //String capturada no Fiddler, talvez voce consiga entender melhor os parametros.
            string stringDoFiddler = @"__EVENTTARGET=&=&__VIEWSTATE=%2FwEPDwUJMTgyODIxODE0ZGTaWEPObuyvoJB7RKx1WJS9BFJ18%2B1zW9am2bUvkyCBjw%3D%3D&__EVENTVALIDATION=%2FwEWBALlgv%2B%2FDQLqxoiEDAL%2Fr4KwDAKp5dHBDx7DU7IrxbkrOoxyN%2BsTt1ADrd4fFwVXxfCGlI%2FG65Pw&ctl00%24ctl00%24cphBodyMain%24cphBody%24txtLogin=gestor%40guiton&ctl00%24ctl00%24cphBodyMain%24cphBody%24txtSenha=criston96*&ctl00%24ctl00%24cphBodyMain%24cphBody%24btnEntrar=";
            string url = "http://www.bradescopromotoranet.com.br/";
            string postDados = null;

            //TODO: CASO ERRO 500, ATUALIZAR A PAGINA DO BANCO NO BROWSER E PROCURAR PELOS PARAMETROS "VIEWSTATE" E "EVENTVALIDATION" NO CODIGO ABAIXO
            Dictionary<string, string> parametrosAspnet = new Dictionary<string, string>()
            {                
                {"__EVENTTARGET", ""},
                {"__VIEWSTATE", "/wEPDwUJMTgyODIxODE0ZGTaWEPObuyvoJB7RKx1WJS9BFJ18+1zW9am2bUvkyCBjw=="},
                {"__EVENTVALIDATION", "/wEWBALlgv+/DQLqxoiEDAL/r4KwDAKp5dHBDx7DU7IrxbkrOoxyN+sTt1ADrd4fFwVXxfCGlI/G65Pw"},
            };

            Dictionary<string, string> parametrosLogin = new Dictionary<string, string>()
            {
                {"ctl00$ctl00$cphBodyMain$cphBody$txtLogin", "gestor@guiton"},
                {"ctl00$ctl00$cphBodyMain$cphBody$txtSenha", "criston96*"},
                {"ctl00$ctl00$cphBodyMain$cphBody$btnEntrar", ""}
            };

            foreach (var key in parametrosAspnet.Keys)
            {
                postDados += key + "=" + Url.Encode(parametrosAspnet[key]) + "&";
            }

            foreach (var key in parametrosLogin.Keys)
            {
                postDados += Url.Encode(key) + "=" + Url.Encode(parametrosLogin[key]) + "&";
            }

            postDados = postDados.Remove(postDados.Length - 1);
            System.Diagnostics.Debug.WriteLine(postDados);


            //Primeiro request. Pelo que pude ler, o ideal seria extrair os codigos do parametro __VIEWSTATE e do __EVENTVALIDATION neste. Apesar que na string tem o EVENTTARGET e o EVENTARGUMENT, porem vazios.
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());

            //TODO Um parser para este responseData que extraia VIEWSTATE, EVENTVALIDATION e parametros aspnet
            string responseData = responseReader.ReadToEnd();
            responseReader.Close();

            //Tentativa de parametrizar
            //string postData = String.Format("__VIEWSTATE={0}&ctl00$ctl00$cphBodyMain$cphBody$txtLogin={1}&ctl00$ctl00$cphBodyMain$cphBody$txtSenha={2}&ctl00$ctl00$cphBodyMain$cphBody$btnEntrar=", viewState, usuario, senha);
            //postDados = stringDoFiddler;

            // Armazenando o cookie
            CookieContainer cookies = new CookieContainer();

            // Agora fazendo o POST
            webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.CookieContainer = cookies;

            // enviar a string postDados no nosso webrequest
            StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
            requestWriter.Write(postDados);
            requestWriter.Close();
            webRequest.GetResponse().Close();

            // Agora chamando a pagina Home
            webRequest = WebRequest.Create(url + "/forms/Home.aspx") as HttpWebRequest;
            webRequest.CookieContainer = cookies;
            responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());

            // Leitura do response e o retorno para o nosso index
            responseData = responseReader.ReadToEnd();
            responseReader.Close();

            return responseData;
        }

    }
}
