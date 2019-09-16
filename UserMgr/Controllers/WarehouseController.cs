using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Security;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Models;
using UserMgr.Entities;
using UserMgr.Entities.View;

namespace UserMgr.Controllers
{
    public class WarehouseController : Controller
    {
        [IdentityAuth(UrlName = "仓库管理")]
        public ActionResult List()
        {
            return View();
        }


        [IdentityAuth(UrlName = "库区管理")]
        public ActionResult InventoryArea()
        {
            return View();
        }


        [IdentityAuth(UrlName = "库位管理")]
        public ActionResult InventoryLocation()
        {
            return View();
        }


        [IdentityAuth(UrlName = "库位分配详情")]
        public ActionResult InventoryAllocation()
        {
            //视图模型
            InventoryAllocationViewModel model = new InventoryAllocationViewModel();

            //获取库存分配信息
            List<InventoryAllocation> IAlist = new DbEntities<InventoryAllocation>().SimpleClient.GetList().OrderByDescending(ia => ia.ChangeTime).ThenByDescending(ia => ia.CreateTime).ToList();

            //遍历所有的库位分配信息
            foreach (InventoryAllocation item in IAlist)
            {
                //库位信息-物资种类
                View_InventoryLocation View_InventoryLocationInfo = new DbEntities<View_InventoryLocation>().SimpleClient.GetSingle(vil => vil.InventoryLocationID == item.InventoryLocationID);
                View_MaterialType MaterialTypeInfo = new DbEntities<View_MaterialType>().SimpleClient.GetSingle(vmt => vmt.MaterialTypeID == item.MaterialTypeID);

                //添加库位分配的具体信息
                V_InventoryAllocation v_Inventory = new V_InventoryAllocation
                {
                    InventoryAllocation = item,
                    InventoryLocation = View_InventoryLocationInfo,
                    MaterialType = MaterialTypeInfo
                };

                model.InventoryAllocations.Add(v_Inventory);
            }

            return View(model);
        }


        [IdentityAuth(UrlName = "托盘信息")]
        public ActionResult Tray()
        {
            return View();
        }


        [IdentityAuth(UrlName = "入库任务单")]
        public ActionResult InboundTask()
        {
            List<View_InboundTask> lists = new DbEntities<View_InboundTask>().SimpleClient.GetList().OrderByDescending(ib => ib.ChangeTime).ToList();

            return View(lists);
        }


        [IdentityAuth(UrlName = "入库任务细节单")]
        public ActionResult InboundTaskDetail()
        {
            return View();
        }
    }
}