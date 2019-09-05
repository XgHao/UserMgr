using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;

namespace UserMgr.Formatter
{
    public static class SetSelectListItems
    {
        /// <summary>
        /// 物资种类
        /// </summary>
        /// <param name="controller">当前控制器</param>
        /// <param name="curMaterialTypeID">当前正在修改的种类ID，空值不传</param>
        public static void MaterialType(Controller controller, int? curMaterialTypeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curMaterialTypeID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择物资种类",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<MaterialType>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.MaterialTypeID == curMaterialTypeID ? true : false,
                    Text = item.MaterialTypeName + "(" + item.MaterialTypeNo + ")",
                    Value = item.MaterialTypeID.ToString()
                });
            }

            controller.ViewData["MaterialType"] = selectListItems;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name = "controller" >当前控制器</ param >
        /// <param name="id">ULR管理ID</param>
        /// <param name="isSkipNotUse">是否跳过未审核用户</param>
        /// <returns></returns>
        public static void User(Controller controller, int? curUserID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curUserID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择用户",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<User>().SimpleClient.GetList().Where(u => u.UserGroupID != 0)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.UserID == curUserID ? true : false,
                    Text = item.UserName,
                    Value = item.UserID.ToString()
                });
            }

            controller.ViewData["User"] = selectListItems;
        }

        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <param name="controller"></param>
        public static void UserGroup(Controller controller, int? curUserGroupID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curUserGroupID==null)
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择用户组",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<UserGroup>().SimpleClient.GetList().Where(ug => ug.UserGroupID != 0))
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.UserGroupID == curUserGroupID ? true : false,
                    Text = item.UserGroupName,
                    Value = item.UserGroupID.ToString()
                });
            }

            controller.ViewData["UserGroup"] = selectListItems;
        }

        /// <summary>
        /// 常用单位
        /// </summary>
        /// <param name="controller"></param>
        public static void UnitList(Controller controller)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>
            {
                new SelectListItem{ Selected=true,Text="选择常用单位",Value="-1" }
            };

            var lists = new DbEntities<Unit>().SimpleClient.GetList();
            foreach (var item in lists)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.UnitName,
                    Value = item.UnitName
                });
            }

            controller.ViewData["Unit"] = selectListItems;
        }

        /// <summary>
        /// 仓库列表
        /// </summary>
        /// <param name="controller"></param>
        public static void Warehouse(Controller controller, int? curWarehousID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            
            if (curWarehousID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择仓库",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<Warehouse>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.WarehouseID == curWarehousID ? true : false,
                    Text = item.WarehouseName + "(" + item.WarehouseNo + ")",
                    Value = item.WarehouseID.ToString()
                });
            }

            controller.ViewData["Warehouse"] = selectListItems;
        }

        /// <summary>
        /// 库区列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInventoryAreaID"></param>
        public static void InventoryArea(Controller controller, int? curInventoryAreaID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInventoryAreaID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择库区",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<InventoryArea>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.InventoryAreaID == curInventoryAreaID ? true : false,
                    Text = item.InventoryAreaName + "(" + item.InventoryAreaNo + ")",
                    Value = item.InventoryAreaID.ToString()
                });
            }

            controller.ViewData["InventoryArea"] = selectListItems;
        }

        /// <summary>
        /// 库位列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInventoryLocationID"></param>
        public static void InventoryLocation(Controller controller, int? curInventoryLocationID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInventoryLocationID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择要分配库位",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<InventoryLocation>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.InventoryLocationID == curInventoryLocationID ? true : false,
                    Text = item.InventoryLocationName + "(" + item.InventoryLocationNo + ")",
                    Value = item.InventoryLocationID.ToString()
                });
            }

            controller.ViewData["InventoryLocation"] = selectListItems;
        }
    }
}