using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Security;

namespace UserMgr.Controllers
{
    public class HomeController : Controller
    {
        [IdentityAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}