using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Security;
using System.Web.Mvc;

namespace UserMgr.Controllers
{
    public class WarehouseController : Controller
    {
        [IdentityAuth(UrlName = "仓库列表")]
        public ActionResult List()
        {
            return View();
        }

        [IdentityAuth(UrlName = "库区列表")]
        public ActionResult InventoryArea()
        {
            return View();
        }
    }
}