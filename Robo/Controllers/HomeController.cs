using Robo.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

    }
}
