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
using UserMgr.Formatter;

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
        public ActionResult InboundTaskDetail(string ibtid = "-1")
        {
            if (int.TryParse(ibtid, out int id)) 
            {
                //新建视图模型
                List<InboundTaskDetailViewModel> model = new List<InboundTaskDetailViewModel>();

                //获取入库任务细节列表
                var lists = new DbEntities<InboundTaskDetail>().SimpleClient.GetList();

                //遍历所有
                foreach (InboundTaskDetail item in lists)
                {
                    if (item.InboundTaskDetailID != id) 
                    {
                        model.Add(Formatterr.GetInboundTaskDetailViewModel(item));
                    }
                }

                InboundTaskDetail cur = lists.Where(it => it.InboundTaskDetailID == id).FirstOrDefault();
                //不为空
                if (cur != null) 
                {
                    //存在当前对象
                    List<InboundTaskDetailViewModel> temp = model.OrderBy(it => it.ChangeTime).ToList();
                    temp.Add(Formatterr.GetInboundTaskDetailViewModel(cur));
                    
                    //反转
                    temp.Reverse();
                    return View(temp);
                }

                model.Reverse();
                return View(model);
            }

            TempData["Msg"] = "没有找到对象";
            return RedirectToAction("InboundTask", "Warehouse");
        }


        [IdentityAuth(UrlName = "出库任务单")]
        public ActionResult OutboundTask()
        {
            List<View_OutboundTask> lists = new DbEntities<View_OutboundTask>().SimpleClient.GetList().OrderByDescending(ob => ob.ChangeTime).ToList();

            return View(lists);
        }
    }
}