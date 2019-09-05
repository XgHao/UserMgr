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
        [IdentityAuth(UrlName = "仓库管理")]
        public ActionResult List()
        {
            return View();
        }

        [IdentityAuth(UrlName = "库区管理")]
        public ActionResult InventoryArea()
        {
            return View();
        }

        [IdentityAuth(UrlName = "库位管理")]
        public ActionResult InventoryLocation()
        {
            return View();
        }

        [IdentityAuth(UrlName = "库位分配详情")]
        public ActionResult InventoryAllocation()
        {
            return View();
        }
    }
}