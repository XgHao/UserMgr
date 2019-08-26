using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserMgr.Formatter;

namespace UserMgr.Controllers.OverrideController
{
    /// <summary>
    /// Json时间格式化-控制器
    /// </summary>
    public class DateJsonController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new DateJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        public new JsonResult Json(object data,JsonRequestBehavior behavior)
        {
            return new DateJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

        public new JsonResult Json(object data)
        {
            return new DateJsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}