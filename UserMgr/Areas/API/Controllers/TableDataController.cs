using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.DB;
using System.Web.Mvc;
using Newtonsoft.Json;
using UserMgr.Areas.API.Models;
using UserMgr.Entities;

namespace UserMgr.Areas.API.Controllers
{
    public class TableDataController : Controller
    {
        // GET: API/TableData
        public ActionResult UrlMgr()
        {
            #region 获取表中的参数
            if (!int.TryParse(Request["offset"], out int offset)) 
            {
                offset = 0;
            }
            if (!int.TryParse(Request["limit"], out int limit)) 
            {
                limit = 0;
            }
            string keyword = Request["keyword"] ?? "";
            string sortName = Request["sortName"] ?? "";
            string sortOrder = Request["sortOrder"] ?? "";
            #endregion

            var pages = new DbHelper().GetPages(keyword, sortName, sortOrder, offset, limit, out int cnt);

            //重新拼接json数据，返回TB_json格式
            var result = JsonConvert.SerializeObject(pages.ToArray());
            string res = "{\"total\":" + cnt + ",\"totalNotFiltered\":" + (cnt - pages.Count) + ",\"rows\":" + result + "}";
            try
            {
                TablePaginModel<Page> paginModel = JsonConvert.DeserializeObject<TablePaginModel<Page>>(res);
                return Json(paginModel, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }
    }
}