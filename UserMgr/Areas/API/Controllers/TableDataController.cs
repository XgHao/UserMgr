using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.DB;
using System.Web.Mvc;
using Newtonsoft.Json;
using UserMgr.Areas.API.Models;
using UserMgr.Entities;
using UserMgr.Entities.View;
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
        public ActionResult UGroupMgr()
        {
            return Json(GetTablePaginModel<UserGroup>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            return Json(GetTablePaginModel<View_User>(), JsonRequestBehavior.AllowGet);
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
            return Json(GetTablePaginModel<View_MaterialType>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审查用户
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUserMgr()
        {
            //额外执行的sql语句--筛选出没有审核的用户
            string sql = @"select *from [View_User] where IsChecked = '0'";
            return Json(GetTablePaginModel<View_User>(sql), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 所有物资列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialList()
        {
            return Json(GetTablePaginModel<View_Material>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 仓库列表
        /// </summary>
        /// <returns></returns>
        public ActionResult WarehouseList()
        {
            return Json(GetTablePaginModel<Warehouse>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 库区列表
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryAreaList()
        {
            return Json(GetTablePaginModel<View_InventoryArea>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 库位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryLocationList()
        {
            return Json(GetTablePaginModel<View_InventoryLocation>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 托盘列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Tray()
        {
            return Json(GetTablePaginModel<View_Tray>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 托盘细节
        /// </summary>
        /// <returns></returns>
        public ActionResult TrayDetail()
        {
            return Json(GetTablePaginModel<View_TrayDetail>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 库存清单
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryList()
        {
            return Json(GetTablePaginModel<View_InventoryList>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 波次清单
        /// </summary>
        /// <returns></returns>
        public ActionResult WavePicking()
        {
            return Json(GetTablePaginModel<View_WavePicking>(), JsonRequestBehavior.AllowGet);
        }






        /// <summary>
        /// 获取对象所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ExterSql">需额外执行的Sql语句</param>
        /// <returns></returns>
        private TablePaginModel<T> GetTablePaginModel<T>(string ExterSql = null) where T : class, new()
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

            List<T> datas = new DbHelper().GetDatas<T>(keyword, sortName, sortOrder, offset, limit, out int cnt, ExterSql ?? $"select * from [{typeof(T).Name}]");

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

            return $"{{\"total\":{cnt},\"totalNotFiltered\":{cnt - obj.Count},\"rows\":{result}}}";
        }
    }
}