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
            string loginAddress = "www.agilus.com.br/Alvo";
            var loginData = new NameValueCollection
            {
                { "usuario", "claudio" },
                { "senha", "ac@78902" }
            };

            var client = new processadorDeRequisicoes();

            client.Login(loginAddress, loginData);
            return client.DownloadString(loginAddress);
        }

        public string Login()
        {
            var Credenciais = new NameValueCollection();

            Credenciais.Add("usuario", "6191CRISTO");
            Credenciais.Add("senha", "1530guiton");

            return WR.DoPost("http://www.agilus.com.br/alvo", "http://www.agilus.com.br/alvo/Consultas", Credenciais);
        }

        }

    }
}
