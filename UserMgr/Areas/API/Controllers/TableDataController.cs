using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.DB;
using System.Web.Mvc;
using Newtonsoft.Json;
using UserMgr.Areas.API.Models;
using UserMgr.Entities;
using Newtonsoft.Json.Converters;
using UserMgr.Controllers.OverrideController;

namespace UserMgr.Areas.API.Controllers
{
    public class TableDataController : DateJsonController
    {
        /// <summary>
        /// URL管理数据
        /// </summary>
        /// <returns></returns>
        public ActionResult UrlMgr()
        {
            return Json(GetTablePaginModel<Page>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户组管理数据
        /// </summary>
        /// <returns></returns>
        public ActionResult uGroupMgr()
        {
            return Json(GetTablePaginModel<UserGroup>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            return Json(GetTablePaginModel<User>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult Supplier()
        {
            return Json(GetTablePaginModel<Supplier>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 物资种类
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialsType()
        {
            return Json(GetTablePaginModel<MaterialType>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审查用户
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUserMgr()
        {
            var userlist = new DbEntities<User>().SimpleClient.GetList().Where(u => !u.IsUse);

            string res = JsonSerialize(userlist.ToList(), userlist.Count());

            try
            {
                TablePaginModel<User> paginModel = JsonConvert.DeserializeObject<TablePaginModel<User>>(res);
                return Json(paginModel, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// 所有物资列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialList()
        {
            return Json(GetTablePaginModel<Material>(), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取对象所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private TablePaginModel<T> GetTablePaginModel<T>() where T : class, new()
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

            List<T> datas = new DbHelper().GetDatas<T>(keyword, sortName, sortOrder, offset, limit, out int cnt);

            //重新拼接json数据，返回TB_json格式
            string res = JsonSerialize(datas, cnt);
            try
            {
                TablePaginModel<T> paginModel = JsonConvert.DeserializeObject<TablePaginModel<T>>(res);
                return paginModel;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 格式化数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        private string JsonSerialize<T>(List<T> obj, int cnt) where T : class, new()
        {
            var result = JsonConvert.SerializeObject(obj.ToArray());
            return "{\"total\":" + cnt + ",\"totalNotFiltered\":" + (cnt - obj.Count) + ",\"rows\":" + result + "}";
        }
    }
}