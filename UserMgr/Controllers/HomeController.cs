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
        //[IdentityAuth]
        public ActionResult Index()
        {
            return View();
        }

        [IdentityAuth(UrlName = "主页")]
        public ActionResult IndexView()
        {
            return View();
        }   


        [IdentityAuth(UrlName = "页面访问权限管理")]
        public ActionResult AccessMgr()
        {
            return View();
        }


        [IdentityAuth(UrlName = "用户组管理")]
        public ActionResult UserGroupMgr()
        {
            return View();
        }

        [IdentityAuth(UrlName = "审核注册用户")]
        public ActionResult CheckUser()
        {
            return View();
        }

        [IdentityAuth(UrlName = "用户列表")]
        public ActionResult UserList()
        {
            return View();
        }



        /// <summary>
        /// 403页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Deny()
        {
            return View();
        }

        /// <summary>
        /// 404页面
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return View();
        }
    }
}