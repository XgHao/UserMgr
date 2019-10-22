using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Security;
using UserMgr.Entities;
using UserMgr.DB;
using UserMgr.Formatter;
using SqlSugar;

namespace UserMgr.Controllers
{
    public class BasicDataController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LayUI()
        {
            return View();
        }


        /// <summary>
        /// 入库类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-入库类型")]
        public ActionResult InboundType()
        {
            return View();
        }

        /// <summary>
        /// 出库类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-出库类型")]
        public ActionResult OutboundType()
        {
            return View();
        }

        /// <summary>
        /// 容器
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-容器")]
        public ActionResult Container()
        {
            return View();
        }

        /// <summary>
        /// 巷道
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-巷道")]
        public ActionResult Narrow()
        {
            return View();
        }

        /// <summary>
        /// 拣货类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-拣货类型")]
        public ActionResult PickingType()
        {
            return View();
        }

        /// <summary>
        /// 销售类型        
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-销售类型")]
        public ActionResult SaleType()
        {
            return View();
        }

        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-单位")]
        public ActionResult Unit()
        {
            return View();
        }

        /// <summary>
        /// 波次类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "基础资料-波次类型")]
        public ActionResult WavePickingType()
        {
            return View();
        }
    }
}