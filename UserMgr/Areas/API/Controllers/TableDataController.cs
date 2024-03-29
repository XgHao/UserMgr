﻿using System;
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
        #region 基本功能-Table

        /// <summary>
        /// URL管理数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UrlMgr()
        {
            return Json(GetTablePaginModel<Page>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户组管理数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UGroupMgr()
        {
            return Json(GetTablePaginModel<UserGroup>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserList()
        {
            return Json(GetTablePaginModel<View_User>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 供应商
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Supplier()
        {
            return Json(GetTablePaginModel<Supplier>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 物资种类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MaterialsType()
        {
            return Json(GetTablePaginModel<View_MaterialType>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审查用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckUserMgr()
        {
            //额外执行的sql语句--筛选出没有审核的用户
            string sql = @"IsChecked = 0";
            return Json(GetTablePaginModel<View_User>(sql), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 所有物资列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MaterialList()
        {
            return Json(GetTablePaginModel<View_Material>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WarehouseList()
        {
            return Json(GetTablePaginModel<Warehouse>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 库区列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InventoryAreaList()
        {
            return Json(GetTablePaginModel<View_InventoryArea>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 库位列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InventoryLocationList()
        {
            return Json(GetTablePaginModel<View_InventoryLocation>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 托盘列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Tray()
        {
            return Json(GetTablePaginModel<View_Tray>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 托盘细节
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TrayDetail()
        {
            return Json(GetTablePaginModel<View_TrayDetail>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 库存清单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InventoryList()
        {
            return Json(GetTablePaginModel<View_InventoryList>(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 波次清单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WavePicking()
        {
            return Json(GetTablePaginModel<View_WavePicking>(), JsonRequestBehavior.AllowGet);
        }

        #endregion





        #region 基础资料-Table

        /// <summary>
        /// 基础资料-入库类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDInboundType() => Json(GetTablePaginModel<View_InboundType>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-出库类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDOutboundType() => Json(GetTablePaginModel<View_OutboundType>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-容器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDContainer() => Json(GetTablePaginModel<View_Container>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-巷道
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDNarrow() => Json(GetTablePaginModel<View_Narrow>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-拣货类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDPickingType() => Json(GetTablePaginModel<View_PickingType>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-销售类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDSaleType() => Json(GetTablePaginModel<View_SaleType>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-单元
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDUnit() => Json(GetTablePaginModel<View_Unit>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-波次类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDWavePickingType() => Json(GetTablePaginModel<View_WavePickingType>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-托盘类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDTrayType() => Json(GetTablePaginModel<View_TrayType>(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// 基础资料-托盘类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BDInventoryAreaType() => Json(GetTablePaginModel<View_InventoryAreaType>(), JsonRequestBehavior.AllowGet);

        #endregion





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

            List<T> datas = new DbHelper().GetDatas<T>(keyword, sortName, sortOrder, offset, limit, out int cnt, ExterSql);

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