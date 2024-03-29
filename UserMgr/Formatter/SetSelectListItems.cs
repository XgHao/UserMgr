﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;
using UserMgr.Entities.View;
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

            if (curMaterialTypeID.IsNullOrUnchecked())
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
            foreach (MaterialType item in db.GetList().Where(it => it.IsAbandon == false)) 
            {
                if (MaterialTypeBool(item, db, curMaterialTypeID))
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Selected = item.MaterialTypeID == curMaterialTypeID ? true : false,
                        Text = $"{item.MaterialTypeName}({item.MaterialTypeNo})",
                        Value = item.MaterialTypeID.ToString()
                    });
                }
            }

            controller.ViewData["MaterialTypeDDL"] = selectListItems;
        }

        /// <summary>
        /// 物资
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curMaterialSizeID"></param>
        public static void Material(Controller controller, int? curMaterialSizeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curMaterialSizeID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择物资规格",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_Material>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.MaterialSizeID == curMaterialSizeID ? true : false,
                    Text = $"{item.MaterialType}[{item.SizeCode}] - {item.Detail}",
                    Value = item.MaterialSizeID.ToString()
                });
            }

            controller.ViewData["MaterialDDL"] = selectListItems;
        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curSupplierID"></param>
        public static void Supplier(Controller controller, int? curSupplierID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curSupplierID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择供应商",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<Supplier>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.SupplierID == curSupplierID ? true : false,
                    Text = item.SupplierName,
                    Value = item.SupplierID.ToString()
                });
            }

            controller.ViewData["SupplierDDL"] = selectListItems;
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

            if (curUserID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择用户",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<User>().SimpleClient.GetList().Where(u => u.UserGroupID != 0 && u.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.UserID == curUserID ? true : false,
                    Text = item.UserName,
                    Value = item.UserID.ToString()
                });
            }

            controller.ViewData["UserDDL"] = selectListItems;
        }

        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <param name="controller"></param>
        public static void UserGroup(Controller controller, int? curUserGroupID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curUserGroupID.IsNullOrUnchecked())
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

            controller.ViewData["UserGroupDDL"] = selectListItems;
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

            var lists = new DbEntities<Unit>().SimpleClient.GetList().Where(it => it.IsAbandon == false);
            foreach (var item in lists)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.UnitName,
                    Value = item.UnitName
                });
            }

            controller.ViewData["UnitDDL"] = selectListItems;
        }

        /// <summary>
        /// 仓库列表
        /// </summary>
        /// <param name="controller"></param>
        public static void Warehouse(Controller controller, int? curWarehousID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curWarehousID.IsNullOrUnchecked())
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
                    Text = $"{item.WarehouseName}({item.WarehouseNo})",
                    Value = item.WarehouseID.ToString()
                });
            }

            controller.ViewData["WarehouseDDL"] = selectListItems;
        }

        /// <summary>
        /// 库区列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInventoryAreaID"></param>
        public static void InventoryArea(Controller controller, int? curInventoryAreaID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInventoryAreaID.IsNullOrUnchecked())
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
                    Text = $"{item.InventoryAreaName}({item.InventoryAreaNo})",
                    Value = item.InventoryAreaID.ToString()
                });
            }

            controller.ViewData["InventoryAreaDDL"] = selectListItems;
        }

        /// <summary>
        /// 库位列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInventoryLocationID"></param>
        public static void InventoryLocation(Controller controller, int? curInventoryLocationID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInventoryLocationID.IsNullOrUnchecked())
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
                    Text = $"{item.InventoryLocationName}({item.InventoryLocationNo})",
                    Value = item.InventoryLocationID.ToString()
                });
            }

            controller.ViewData["InventoryLocationDDL"] = selectListItems;
        }

        /// <summary>
        /// 库存列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInventoryListID"></param>
        public static void InventoryList(Controller controller, int? curInventoryListID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInventoryListID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择库存",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_InventoryList>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.InventoryListID == curInventoryListID ? true : false,
                    Text = $"{item.InventoryType} - {item.InboundTaskNo} - {item.OutboundTaskNo} - {item.TrayNoAndCode} - {item.InventoryLocationNo}",
                    Value = item.InventoryListID.ToString()
                });
            }

            controller.ViewData["InventoryListDDL"] = selectListItems;
        }

        /// <summary>
        /// 容器列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curContainerID"></param>
        public static void Container(Controller controller, int? curContainerID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curContainerID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择容器",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<Container>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.ContainerID == curContainerID ? true : false,
                    Text = item.ContainerName,
                    Value = item.ContainerID.ToString()
                });
            }

            controller.ViewData["ContainerDDL"] = selectListItems;
        }


        /// <summary>
        /// 状态列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curStatusID"></param>
        public static void Status(Controller controller, int? curStatusID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curStatusID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择状态",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<Status>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.StatusID == curStatusID ? true : false,
                    Text = item.StatusName,
                    Value = item.StatusID.ToString()
                });
            }

            controller.ViewData["StatusDDL"] = selectListItems;
        }

        /// <summary>
        /// 入库明细单
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInboundTaskDetailID"></param>
        public static void InboundTaskDetail(Controller controller, int? curInboundTaskDetailID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInboundTaskDetailID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择入库任务明细单",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_InboundTaskDetail>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.InboundTaskDetailID == curInboundTaskDetailID ? true : false,
                    Text = $"{item.InboundTaskNo} - {item.MaterialSizeInfo} - {item.BatchNumber} - {item.CarNum} - {item.Glaze} - {item.Unit}",
                    Value = item.InboundTaskDetailID.ToString()
                });
            }

            controller.ViewData["InboundTaskDetailDDL"] = selectListItems;
        }

        /// <summary>
        /// 入库类型
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curInboundType"></param>
        public static void InboundType(Controller controller, int? curInboundTypeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curInboundTypeID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择入库类型",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<InboundType>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.InboundTypeID == curInboundTypeID ? true : false,
                    Text = item.InboundTypeName,
                    Value = item.InboundTypeID.ToString()
                });
            }

            controller.ViewData["InboundTypeDDL"] = selectListItems;
        }

        /// <summary>
        /// 出库明细单
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curOutboundTaskDetailID"></param>
        public static void OutboundTaskDetail(Controller controller, int? curOutboundTaskDetailID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curOutboundTaskDetailID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "请选择出库任务明细单",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_OutboundTaskDetail>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.OutboundTaskDetailID == curOutboundTaskDetailID ? true : false,
                    Text = $"{item.OutboundTaskNo} - {item.MaterialSizeInfo} - {item.BatchNumber} - {item.CarNum} - {item.Glaze} - {item.Unit}",
                    Value = item.OutboundTaskDetailID.ToString()
                });
            }

            controller.ViewData["OutboundTaskDetailDDL"] = selectListItems;
        }

        /// <summary>
        /// 出库类型
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curOutboundTypeID"></param>
        public static void OutboundType(Controller controller, int? curOutboundTypeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curOutboundTypeID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择出库类型",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<OutboundType>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.OutboundTypeID == curOutboundTypeID ? true : false,
                    Text = item.OutboundTypeName,
                    Value = item.OutboundTypeID.ToString()
                });
            }

            controller.ViewData["OutboundTypeDDL"] = selectListItems;
        }

        /// <summary>
        /// 销售类型
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curSaleTypeID"></param>
        public static void SaleType(Controller controller, int? curSaleTypeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curSaleTypeID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择销售类型",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<SaleType>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.SaleTypeID == curSaleTypeID ? true : false,
                    Text = item.SaleTypeName,
                    Value = item.SaleTypeID.ToString()
                });
            }

            controller.ViewData["SaleTypeDDL"] = selectListItems;
        }

        /// <summary>
        /// 托盘列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curTrayID"></param>
        public static void Tray(Controller controller, int? curTrayID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curTrayID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择托盘",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_Tray>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.TrayID == curTrayID ? true : false,
                    Text = $"{item.TrayNo} - {item.TrayTypeName} - {item.TrayCode} - {item.Remark}",
                    Value = item.TrayID.ToString()
                });
            }

            controller.ViewData["TrayDDL"] = selectListItems;
        }

        /// <summary>
        /// 托盘类型列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curTrayTypeID"></param>
        public static void TrayType(Controller controller,int? curTrayTypeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curTrayTypeID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择托盘类型",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_TrayType>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.TrayTypeID == curTrayTypeID ? true : false,
                    Text = item.TrayTypeName,
                    Value = item.TrayTypeID.ToString()
                });
            }

            controller.ViewData["TrayTypeDDL"] = selectListItems;
        }

        /// <summary>
        /// 托盘明细列表
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curTrayDetailID"></param>
        public static void TrayDetail(Controller controller, int? curTrayDetailID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curTrayDetailID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择托盘明细",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<View_TrayDetail>().SimpleClient.GetList())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.TrayDetailID == curTrayDetailID ? true : false,
                    Text = $"{item.InboundTaskNo} - {item.MaterialSizeInfo} - {item.TrayNo} - {item.TrayCode}",
                    Value = item.TrayDetailID.ToString()
                });
            }

            controller.ViewData["TrayDetailDDL"] = selectListItems;
        }

        /// <summary>
        /// 波次类型
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curWavePickingID"></param>
        public static void WavePickingType(Controller controller, int? curWavePickingID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curWavePickingID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择波次类型",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<WavePickingType>().SimpleClient.GetList().Where(it => it.IsAbandon == false)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.WavePickingTypeID == curWavePickingID ? true : false,
                    Text = item.WavePickingTypeName,
                    Value = item.WavePickingTypeID.ToString()
                });
            }

            controller.ViewData["WavePickingTypeDDL"] = selectListItems;
        }

        /// <summary>
        /// 拣货类型
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="curPickingTypeID"></param>
        public static void PickingType(Controller controller, int? curPickingTypeID = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (curPickingTypeID.IsNullOrUnchecked())
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "选择拣货类型",
                    Value = "-1"
                });
            }

            foreach (var item in new DbEntities<PickingType>().SimpleClient.GetList().Where(it => it.IsAbandon)) 
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = item.PickingTypeID == curPickingTypeID ? true : false,
                    Text = item.PickingTypeName,
                    Value = item.PickingTypeID.ToString()
                });
            }

            controller.ViewData["PickingTypeDDL"] = selectListItems;
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