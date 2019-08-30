using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Security;

namespace UserMgr.Controllers
{
    public class MaterialsController : Controller
    {
        [IdentityAuth(UrlName = "供应商列表")]
        public ActionResult Supplier()
        {
            return View();
        }

        [IdentityAuth(UrlName = "物资种类")]
        public ActionResult TypeList()
        {
            return View();
        }

        [IdentityAuth(UrlName = "物资列表")]
        public ActionResult MaterialList()
        {
            return View();
        }
    }
}