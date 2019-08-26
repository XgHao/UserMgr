using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserMgr.Controllers
{
    public class AddEntityController : Controller
    {
        // GET: AddEntity
        public ActionResult UserGroup()
        {
            return View();
        }
    }
}