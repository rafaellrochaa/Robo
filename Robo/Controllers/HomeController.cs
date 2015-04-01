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

        }

        public string Login()
        {
            var Credenciais = new NameValueCollection();

            Credenciais.Add("usuario", "6191CRISTO");
            Credenciais.Add("senha", "1530guiton");

            return WR.DoPost("https://consignado.bemvindobanrisul.com.br/Login.aspx?ReturnUrl=%2fDefault.aspx", "https://consignado.bemvindobanrisul.com.br/Default.aspx", Credenciais);


        }

    }
}
