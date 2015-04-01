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

            var Credenciais = new NameValueCollection();

            Credenciais.Add("cphBodyMain_cphBody_txtLogin", "gestor@guiton");
            Credenciais.Add("cphBodyMain_cphBody_txtSenha", "criston96*");

            string html = WR.DoRequest("http://www.bradescopromotoranet.com.br/Login.aspx", Credenciais);

            return html;
        }

    }
}
