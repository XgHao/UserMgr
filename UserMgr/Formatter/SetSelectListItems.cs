using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;
using SqlSugar;

namespace UserMgr.Formatter
{
    /// <summary>
    /// 设置下拉框数据
    /// </summary>
    public static class SetSelectListItems
    {
        /// <summary>
        /// 物资种类
        /// </summary>
        /// <param name="controller">当前控制器</param>
        /// <param name="curMaterialTypeID">当前正在修改的种类ID，空值不传</param>
        /// <param name="rootMaterialTypeID">当前物资种类父类，空值不传</param>
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
            else
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "无",
                    Value = string.Empty
                });
            }

            SimpleClient<MaterialType> db = new DbEntities<MaterialType>().SimpleClient;
            
            //遍历所有种类
            foreach (MaterialType item in db.GetList())
            {
                if (MaterialTypeBool(item, db, curMaterialTypeID)) 
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Selected = item.MaterialTypeID == curMaterialTypeID ? true : false,
                        Text = item.MaterialTypeName + "(" + item.MaterialTypeNo + ")",
                        Value = item.MaterialTypeID.ToString()
                    });
                }
            }

            controller.ViewData["MaterialType"] = selectListItems;
        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curSupplierID"></param>
        public static void Supplier(Controller controller, int? curSupplierID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curSupplierID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择供应商",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<Supplier>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.SupplierID == curSupplierID ? true : false,
                    Text = item.SupplierName,
                    Value = item.SupplierID.ToString()
                });
            }

            controller.ViewData["Supplier"] = selectListItems;
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

        /// <summary>
        /// 容器列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curContainerID"></param>
        public static void Container(Controller controller, int? curContainerID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curContainerID == null) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择容器",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<Container>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.ContainerID == curContainerID ? true : false,
                    Text = item.ContainerName,
                    Value = item.ContainerID.ToString()
                });
            }

            controller.ViewData["Container"] = selectListItems;  
        }


        /// <summary>
        /// 判断是否显示该项
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private static bool MaterialTypeBool(MaterialType temp, SimpleClient<MaterialType> db, int? curMaterialTypeID = null)
        {
            //跳过本身
            if (temp.MaterialTypeID != curMaterialTypeID) 
            {
                //父类不为空
                while (temp.MaterialTypeRoot != null)
                {
                    //父类等于当前类-返回false
                    if (temp.MaterialTypeRoot == curMaterialTypeID)
                    {
                        return false;
                    }
                    else
                    {
                        //不等于父类，继续查找父类
                        temp = db.GetById(temp.MaterialTypeRoot);
                    }
                }
                return true;
            }
            return false;
        }
    }
}