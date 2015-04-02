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
            Credenciais.Add("__EVENTTARGET","" );
            Credenciais.Add("__EVENTARGUMENT","" );
            Credenciais.Add("__VIEWSTATE", "%2FwEPDwUJMTgyODIxODE0ZGQInJ4UBWG1gt88XYEFMmPDELQuweZbcjaCv3AJ3jYS3Q%3D%3D");
            Credenciais.Add("_EVENTVALIDATION", "%2FwEWBAK2v7PFDQLqxoiEDAL%2Fr4KwDAKp5dHBD%2FbDTqedsY4Fe6Vn%2FoL3rnHM2ZFLrOE%2BbBA%2Ft24LIUvD");
            Credenciais.Add("cphBodyMain_cphBody_txtLogin", "gestor@guiton");
            Credenciais.Add("cphBodyMain_cphBody_txtSenha", "criston96*");
            Credenciais.Add("ctl00%24ctl00%24cphBodyMain%24cphBody%24btnEntrar", "");

            string html = WR.DoRequest("http://www.bradescopromotoranet.com.br/Login.aspx", Credenciais);

            return html;
        }
    }
}
