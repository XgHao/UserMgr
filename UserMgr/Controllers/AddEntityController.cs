using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Models;
using UserMgr.Security;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Controllers
{
    public class AddEntityController : Controller
    {
        #region 基础功能-新增

        /// <summary>
        /// 增加用户组
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增用户组")]
        public ActionResult UserGroup()
        {
            return View();
        }

        /// <summary>
        /// 增加用户组[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserGroup(UserGroupViewModel model)
        {
            //验证模型
            if (ModelState.IsValid)
            {
                //获取当前用户信息
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<UserGroup>().SimpleClient;
                    //用户组名与编码不能重复
                    if (db.IsAny(ug => ug.UserGroupName == model.UserGroupName || ug.UserGroupNo == model.UserGroupNo))
                    {
                        //名称或者编码重复
                        ModelState.AddModelError("UserGroupCode", "用户组名或编码已存在");
                    }
                    else
                    {
                        //转换为实体模型
                        var usergroup = model.InitAddUserGroup(CurUserID);

                        //插入数据
                        if (db.Insert(usergroup))
                        {
                            //新增成功
                            TempData["Msg"] = $"用户组 {model.UserGroupName} 添加成功";
                            return RedirectToAction("UserGroupMgr", "Home");
                        }
                        else
                        {
                            TempData["Msg"] = "添加失败";
                        }
                    }
                }
                else
                {
                    TempData["Msg"] = "登录身份已过期，请重新登录";
                }
            }
            return View(model);
        }



        /// <summary>
        /// 增加供应商
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增供应商")]
        public ActionResult Supplier()
        {
            return View();
        }

        /// <summary>
        /// 增加供应商[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Supplier(SupplierViewModel model)
        {
            //验证模型
            if (ModelState.IsValid)
            {
                //判断名称与编码重复
                var db = new DbEntities<Supplier>().SimpleClient;

                if (db.IsAny(su => su.SupplierName == model.SupplierName || su.SupplierNo == model.SupplierNo))
                {
                    ModelState.AddModelError("SupplierCode", "供应商名称或者编码已存在");
                }
                else
                {
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        //转换为实体
                        Supplier entity = model.InitAddSupplier(curUserID);

                        if (new DbEntities<Supplier>().SimpleClient.Insert(entity))
                        {
                            TempData["Msg"] = $"供应商 {model.SupplierName} 添加成功";
                            return RedirectToAction("Supplier", "Materials");
                        }
                        else
                        {
                            TempData["Msg"] = "添加失败";
                        }
                    }
                }
            }
            return View();
        }



        /// <summary>
        /// 物资种类
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "物资种类")]
        public ActionResult MaterialType()
        {
            //设置下拉框
            SetSelectListItems.Supplier(this);
            SetSelectListItems.MaterialType(this);
            return View();
        }

        /// <summary>
        /// 物资种类[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaterialType(MaterialTypeViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {

                //名称编码是否有重复
                var db = new DbEntities<MaterialType>().SimpleClient;

                if (db.IsAny(mt => mt.MaterialTypeName == model.MaterialTypeName || mt.MaterialTypeNo == model.MaterialTypeNo))
                {
                    ModelState.AddModelError("MaterialTypeCode", "物资种类名称或编码已存在");
                }
                else
                {
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        //转换为对应实体
                        MaterialType entity = model.InitAddMaterialType(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"物资种类 {entity.MaterialTypeName} 添加成功";
                            return RedirectToAction("TypeList", "Materials");
                        }
                        else
                        {
                            TempData["Msg"] = "添加失败";
                        }
                    }
                }
            }

            SetSelectListItems.Supplier(this);
            SetSelectListItems.MaterialType(this);
            return View();
        }



        /// <summary>
        /// 物资
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增物资")]
        public ActionResult Material()
        {
            //设置下拉框
            SetSelectListItems.MaterialType(this);
            SetSelectListItems.UnitList(this);
            SetSelectListItems.Container(this);

            return View();
        }

        /// <summary>
        /// 物资[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Material(MaterialViewModel model)
        {
            if (model.UnitInput == null && model.Unit == "-1")
            {
                ModelState.AddModelError("UnitInput", "请输入或选择一项单位");
            }
            if (ModelState.IsValid)
            {
                //物资型号模型不能相同
                var db = new DbEntities<Material>().SimpleClient;

                if (db.IsAny(m => m.SizeCode == model.SizeCode || m.MaterialModel == model.MaterialModel))
                {
                    ModelState.AddModelError("SizeCode", "编号或者型号不能相同");
                }
                else
                {
                    //获得对应实体
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int creater))
                    {
                        Material entity = model.InitAddMaterial(creater);

                        //插入数据
                        if (new DbEntities<Material>().SimpleClient.Insert(entity))
                        {
                            TempData["Msg"] = "添加成功";
                            return RedirectToAction("MaterialList", "Materials");
                        }
                    }
                }
            }

            //设置下拉框
            SetSelectListItems.MaterialType(this, model.MaterialTypeID);
            SetSelectListItems.UnitList(this);
            SetSelectListItems.Container(this, model.MaterialContainer);
            TempData["Msg"] = "添加失败，请检查信息";
            return View();
        }



        /// <summary>
        /// 仓库
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增仓库")]
        public ActionResult Warehouse()
        {
            return View();
        }

        /// <summary>
        /// 仓库[HttpPost]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Warehouse(WarehouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                //名称和编号不能重复
                var db = new DbEntities<Warehouse>().SimpleClient;

                if (db.IsAny(w => w.WarehouseName == model.WarehouseName || w.WarehouseNo == model.WarehouseNo))
                {
                    ModelState.AddModelError("WarehouseNo", "仓库名称或编号已存在");
                }
                else
                {
                    //检验当前登录身份是否过期，并获取当前登录人ID
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        Warehouse entity = model.InitAddWarehouse(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"仓库 {entity.WarehouseName} 添加成功";
                            return RedirectToAction("List", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }
            return View(model);
        }



        /// <summary>
        /// 库区
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增库区")]
        public ActionResult InventoryArea()
        {
            SetSelectListItems.Warehouse(this);
            return View();
        }

        /// <summary>
        /// 库区[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InventoryArea(InventoryAreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                //名称编号不可重复
                var db = new DbEntities<InventoryArea>().SimpleClient;

                if (db.IsAny(ia => ia.InventoryAreaNo == model.InventoryAreaNo || ia.InventoryAreaName == model.InventoryAreaName))
                {
                    ModelState.AddModelError("InventoryAreaNo", "库区名称或编号已存在");
                }
                else
                {
                    //检验登录人身份，并获取对应ID
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        InventoryArea entity = model.InitAddInventoryArea(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"库区 {entity.InventoryAreaName} 添加成功";
                            return RedirectToAction("InventoryArea", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }
            SetSelectListItems.Warehouse(this);
            return View(model);
        }



        /// <summary>
        /// 库位
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增库位")]
        public ActionResult InventoryLocation()
        {
            SetSelectListItems.InventoryArea(this);
            return View();
        }

        /// <summary>
        /// 库位[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InventoryLocation(InventoryLocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                //名称编号不重复
                var db = new DbEntities<InventoryLocation>().SimpleClient;

                if (db.IsAny(il => il.InventoryLocationName == model.InventoryLocationName || il.InventoryLocationNo == model.InventoryLocationNo))
                {
                    ModelState.AddModelError("InventoryLocationNo", "库位名称或编号已存在");
                }
                else
                {
                    //检验登录人身份，并获取对应ID
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        InventoryLocation entity = model.InitAddInventoryLocation(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"库区 {entity.InventoryLocationName} 添加成功";
                            return RedirectToAction("InventoryLocation", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            SetSelectListItems.InventoryArea(this);
            return View(model);
        }


        /// <summary>
        /// 库位分配
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "库位分配")]
        public ActionResult InventoryAllocation(string Id = null)
        {
            SetSelectListItems.MaterialType(this);

            if (int.TryParse(Id, out int curInventoryLocationID))
            {
                SetSelectListItems.InventoryLocation(this, curInventoryLocationID);
            }
            else
            {
                SetSelectListItems.InventoryLocation(this);
            }
            return View();
        }

        /// <summary>
        /// 库位分配[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InventoryAllocation(InventoryAllocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                {
                    InventoryAllocation entity = model.InitAddInventoryAllocation(curUserID);

                    if (new DbEntities<InventoryAllocation>().SimpleClient.Insert(entity))
                    {
                        TempData["Msg"] = "库位分配成功";
                        return RedirectToAction("InventoryAllocation", "Warehouse");
                    }
                    TempData["Msg"] = "失败";
                }
                else
                {
                    TempData["Msg"] = "登录身份过期，请重新登录";
                }
            }

            SetSelectListItems.MaterialType(this, model.MaterialTypeID);
            SetSelectListItems.InventoryLocation(this, model.InventoryLocationID);
            return View(model);
        }



        /// <summary>
        /// 托盘
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增托盘")]
        public ActionResult Tray()
        {
            SetSelectListItems.Container(this);
            SetSelectListItems.TrayType(this);

            return View();
        }

        /// <summary>
        /// 托盘[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tray(TrayViewModel model)
        {
            if (ModelState.IsValid)
            {
                //编号条码不重复
                var db = new DbEntities<Tray>().SimpleClient;

                if (db.IsAny(tr => tr.TrayNo == model.TrayNo || tr.TrayCode == model.TrayCode))
                {
                    ModelState.AddModelError("TrayCode", "托盘编号或条码");
                }
                else
                {
                    //检验登录人身份,并获取对应ID
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        Tray entity = model.InitAddTray(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"托盘 [{entity.TrayNo}] 添加成功";
                            return RedirectToAction("Tray", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            SetSelectListItems.Container(this, model.Container);
            SetSelectListItems.TrayType(this, model.TrayType);

            return View(model);
        }



        /// <summary>
        /// 托盘明细
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增托盘细节")]
        public ActionResult TrayDetail(string tdid = null)
        {
            //关联托盘单
            if (int.TryParse(tdid, out int id))
            {
                var db = new DbEntities<TrayDetail>().SimpleClient;

                if (db.GetById(id) == null)
                {
                    TrayDetailViewModel model = new TrayDetailViewModel
                    {
                        TrayID = id,
                    };

                    SetSelectListItems.InboundTaskDetail(this);
                    SetSelectListItems.Material(this);
                    return View(model);
                }
            }

            TempData["Msg"] = "请先选择对应托盘";
            return RedirectToAction("Tray", "Warehouse");
        }

        /// <summary>
        /// 托盘明细[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrayDetail(TrayDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<TrayDetail>().SimpleClient;

                ////入库明细、物资规格、托盘不能都相同
                //if (db.IsAny(t => t.InboundTaskDetailID == model.InboundTaskDetailID && t.MaterialSizeID == model.MaterialSizeID && t.TrayDetailID == model.TrayDetailID))

                //当前的托盘盘是否已经添加
                if (db.IsAny(t => t.TrayID == model.TrayID))
                {
                    TempData["Msg"] = "该托盘单细节已存在";
                }
                else
                {
                    //登录人信息
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        TrayDetail entity = model.InitAddTrayDetail(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = "添加成功";
                            return RedirectToAction("TrayDetail", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            SetSelectListItems.InboundTaskDetail(this);
            SetSelectListItems.Material(this);
            return View(model);
        }



        /// <summary>
        /// 入库任务单
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增入库任务单")]
        public ActionResult InboundTask()
        {
            SetSelectListItems.Supplier(this);
            SetSelectListItems.InboundType(this);
            return View();
        }

        /// <summary>
        /// 入库任务单[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InboundTask(InboundTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<InboundTask>().SimpleClient;

                //编号不重复
                if (db.IsAny(ib => ib.InboundTaskNo == model.InboundTaskNo))
                {
                    ModelState.AddModelError("InboundTaskNo", "该编号已存在");
                }
                else
                {
                    //登录人信息
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        InboundTask entity = model.InitAddInboundTask(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"入库任务单 [{entity.InboundTaskNo}] 添加成功";
                            return RedirectToAction("InboundTask", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            SetSelectListItems.Supplier(this, model.SupplierID);
            SetSelectListItems.InboundType(this, model.InboundType);
            return View(model);
        }



        /// <summary>
        /// 入库任务细节单
        /// </summary>
        /// <param name="ibtid">关联的入库任务单ID</param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增入库任务细节单")]
        public ActionResult InboundTaskDetail(string ibtid = null)
        {
            //关联的入库单ID
            if (int.TryParse(ibtid, out int curibtID))
            {
                var db = new DbEntities<InboundTask>().SimpleClient;

                var InboundTaskInfo = db.GetById(curibtID);

                if (InboundTaskInfo != null)
                {
                    InboundTaskDetailViewModel model = new InboundTaskDetailViewModel
                    {
                        InboundTaskID = curibtID,
                        InboundTaskInfo_InboundTaskNo = InboundTaskInfo.InboundTaskNo
                    };

                    //设置物资规格
                    SetSelectListItems.Material(this);
                    return View(model);
                }
            }

            TempData["Msg"] = "没有找到对象";
            return RedirectToAction("InboundTask", "Warehouse");
        }

        /// <summary>
        /// 入库任务细节单[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InboundTaskDetail(InboundTaskDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<InboundTaskDetail>().SimpleClient;

                if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                {
                    InboundTaskDetail entity = model.InitAddInboundTaskDetail(curUserID);

                    if (db.Insert(entity))
                    {
                        TempData["Msg"] = $"入库任务细节单 [{model.InboundTaskInfo_InboundTaskNo}] 添加成功";
                        return RedirectToAction("InboundTask", "Warehouse");
                    }
                    TempData["Msg"] = "添加失败";
                }
                else
                {
                    TempData["Msg"] = "登录身份过期，请重新登录";
                }
            }

            SetSelectListItems.Material(this, model.MaterialSizeID);
            return View(model);
        }



        /// <summary>
        /// 出库任务单
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增出库任务单")]
        public ActionResult OutboundTask()
        {
            //下拉框
            SetSelectListItems.OutboundType(this);
            SetSelectListItems.SaleType(this);

            return View();
        }

        /// <summary>
        /// 出库任务单[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OutboundTask(OutboundTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<OutboundTask>().SimpleClient;

                //编号不重复
                if (db.IsAny(ob => ob.OutboundTaskNo == model.OutboundTaskNo))
                {
                    ModelState.AddModelError("OutboundTaskNo", "该编号已存在");
                }
                else
                {
                    //登录人信息
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        OutboundTask entity = model.InitAddOutboundTask(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"出库任务单 [{entity.OutboundTaskNo}] 添加成功";
                            return RedirectToAction("OutboundTask", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            //下拉框
            SetSelectListItems.OutboundType(this);
            SetSelectListItems.SaleType(this);

            return View(model);
        }



        /// <summary>
        /// 出库任务细节单
        /// </summary>
        /// <param name="obtid"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增出库任务细节单")]
        public ActionResult OutboundTaskDetail(string obtid = null)
        {
            //关联的出库单ID
            if (int.TryParse(obtid, out int curobtID))
            {
                var db = new DbEntities<OutboundTask>().SimpleClient;

                //当前出库任务单信息
                var OutboundTaskInfo = db.GetById(curobtID);

                if (OutboundTaskInfo != null)
                {
                    OutboundTaskDetailViewModel model = new OutboundTaskDetailViewModel
                    {
                        OutboundTaskID = curobtID,
                        OutboundTaskInfo_OutboundTaskNo = OutboundTaskInfo.OutboundTaskNo
                    };

                    //设置物资规格
                    SetSelectListItems.Material(this);
                    return View(model);
                }
            }

            TempData["Msg"] = "没有找到对象";
            return RedirectToAction("OutboundTask", "Warehouse");
        }

        /// <summary>
        /// 出库任务细节单[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OutboundTaskDetail(OutboundTaskDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<OutboundTaskDetail>().SimpleClient;

                if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                {
                    OutboundTaskDetail entity = model.InitAddOutboundTaskDetail(curUserID);

                    if (db.Insert(entity))
                    {
                        TempData["Msg"] = $"出库任务细节单 [{model.OutboundTaskInfo_OutboundTaskNo}] 添加成功";
                        return RedirectToAction("OutboundTask", "Warehouse");
                    }
                    TempData["Msg"] = "添加失败";
                }
                else
                {
                    TempData["Msg"] = "登录身份过期，请重新登录";
                }
            }

            SetSelectListItems.Material(this, model.MaterialSizeID);
            return View(model);
        }



        /// <summary>
        /// 库存清单
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增库存清单")]
        public ActionResult InventoryList()
        {
            //下拉框设置
            SetSelectListItems.InboundTaskDetail(this);
            SetSelectListItems.OutboundTaskDetail(this);
            SetSelectListItems.Tray(this);
            SetSelectListItems.InventoryLocation(this);

            return View();
        }

        /// <summary>
        /// 库存清单[HttpPost]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InventoryList(InventoryListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<InventoryList>().SimpleClient;

                //信息记录不重复
                if (db.IsAny(il => il.InboundTaskDetailID == model.InboundTaskDetailID && il.OutboundTaskDetailID == model.OutboundTaskDetailID && il.TrayID == model.TrayID && il.InventoryLocationID == model.InventoryLocationID && il.InventoryType == model.InventoryType))
                {
                    TempData["Msg"] = "该条记录已存在";
                }
                else
                {
                    //登录人信息
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        InventoryList entity = model.InitAddInventoryList(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"添加成功";
                            return RedirectToAction("InventoryList", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            //下拉框设置
            SetSelectListItems.InboundTaskDetail(this);
            SetSelectListItems.OutboundTaskDetail(this);
            SetSelectListItems.Tray(this);
            SetSelectListItems.InventoryLocation(this);
            return View();
        }



        /// <summary>
        /// 波次清单
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "波次清单")]
        public ActionResult WavePicking()
        {
            //设置下拉框
            SetSelectListItems.PickingType(this);
            SetSelectListItems.WavePickingType(this);

            return View();
        }

        /// <summary>
        /// 波次清单[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WavePicking(WavePickingViewModel model)
        {
            if (ModelState.IsValid)
            {
                //编号不重复
                var db = new DbEntities<WavePicking>().SimpleClient;

                if (db.IsAny(wp => wp.WavePickingNo == model.WavePickingNo))
                {
                    ModelState.AddModelError("WavePickingNo", "波次编号已存在");
                }
                else
                {
                    //检验登录人身份
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        WavePicking entity = model.InitAddWavePicking(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = $"波次单 [{entity.WavePickingNo}] 添加成功";
                            return RedirectToAction("WavePicking", "Warehouse");
                        }
                        TempData["Msg"] = "添加失败";
                    }
                    else
                    {
                        TempData["Msg"] = "登录身份过期，请重新登录";
                    }
                }
            }

            //设置下拉框
            SetSelectListItems.PickingType(this);
            SetSelectListItems.WavePickingType(this);

            return View(model);
        }



        /// <summary>
        /// 波次明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "波次明细清单")]
        public ActionResult WavePickingDetail(string id)
        {
            if (int.TryParse(id, out int wpdid))
            {
                //当前的波次单
                var curWP = new DbEntities<WavePicking>().SimpleClient.GetById(wpdid);
                if (curWP != null)
                {
                    WavePickingDetailViewModel model = new WavePickingDetailViewModel
                    {
                        WavePickingID = curWP.WavePickingID,
                        WavePickingNo = curWP.WavePickingNo
                    };

                    //下拉框
                    SetSelectListItems.OutboundTaskDetail(this);
                    SetSelectListItems.Material(this);
                    SetSelectListItems.TrayDetail(this);
                    SetSelectListItems.InventoryList(this);

                    return View(model);
                }
            }

            TempData["Msg"] = "没有找到对象";
            return RedirectToAction("WavePicking", "Warehouse");
        }

        /// <summary>
        /// 波次明细[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WavePickingDetail(WavePickingDetailViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }

        #endregion




        #region 基础资料-新增

        /// <summary>
        /// 新增入库类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增入库类型")]
        public ActionResult InboundType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增入库类型
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string InboundType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<InboundType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.InboundTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该入库名称已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new InboundTypeViewModel { InboundTypeName = Name }.InitAddInboundType(CurUserID);

                        //更新
                        res = new DbEntities<InboundType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增出库类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增出库类型")]
        public ActionResult OutboundType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增出库类型
        /// </summary>
        /// <param name="Content">新增内容</param>
        /// <returns></returns>
        [HttpPost]
        public string OutboundType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<OutboundType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.OutboundTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该出库名称已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new OutboundTypeViewModel { OutboundTypeName = Name }.InitAddOutboundType(CurUserID);

                        //更新
                        res = new DbEntities<OutboundType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增容器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增容器")]
        public ActionResult Container()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增容器
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string Container(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<Container>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.ContainerName == Name && ibt.IsAbandon == false))
                    {
                        res = "该容器名称已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new ContainerViewModel { ContainerName = Name }.InitAddContainer(CurUserID);

                        //更新
                        res = new DbEntities<Container>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增巷道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增巷道")]
        public ActionResult Narrow()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增巷道
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string Narrow(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<Narrow>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.NarrowName == Name && ibt.IsAbandon == false))
                    {
                        res = "该巷道名称已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new NarrowViewModel { NarrowName = Name }.InitAddNarrow(CurUserID);

                        //更新
                        res = new DbEntities<Narrow>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增拣货类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增拣货类型")]
        public ActionResult PickingType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增拣货类型
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string PickingType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<PickingType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.PickingTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该拣货类型已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new PickingTypeViewModel { PickingTypeName = Name }.InitAddPickingType(CurUserID);

                        //更新
                        res = new DbEntities<PickingType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增销售类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增销售类型")]
        public ActionResult SaleType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增销售类型
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaleType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<SaleType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.SaleTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该销售类型已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new SaleTypeViewModel { SaleTypeName = Name }.InitAddSaleType(CurUserID);

                        //更新
                        res = new DbEntities<SaleType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增单位")]
        public ActionResult Unit()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增单位
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string Unit(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<Unit>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.UnitName == Name && ibt.IsAbandon == false))
                    {
                        res = "该单位已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new UnitViewModel { UnitName = Name }.InitAddUnit(CurUserID);

                        //更新
                        res = new DbEntities<Unit>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增波次类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增波次类型")]
        public ActionResult WavePickingType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增波次类型
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string WavePickingType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<WavePickingType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.WavePickingTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该波次类型已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new WavePickingTypeViewModel { WavePickingTypeName = Name }.InitAddWavePickingType(CurUserID);

                        //更新
                        res = new DbEntities<WavePickingType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增托盘类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增托盘类型")]
        public ActionResult TrayType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增托盘类型
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string TrayType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<TrayType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.TrayTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该托盘类型已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new TrayTypeViewModel { TrayTypeName = Name }.InitAddTrayType(CurUserID);

                        //更新
                        res = new DbEntities<TrayType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }



        /// <summary>
        /// 新增库区类型
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "新增库区类型")]
        public ActionResult InventoryAreaType()
        {
            return View();
        }

        /// <summary>
        /// [AJAX] - 新增库区类型
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryAreaType(string Content = "")
        {
            string res = "名称不合法";
            string Name = Content.Trim();

            if (!string.IsNullOrEmpty(Name))
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID))
                {
                    var db = new DbEntities<InventoryAreaType>().SimpleClient;

                    //检查是否已存在
                    if (db.IsAny(ibt => ibt.InventoryAreaTypeName == Name && ibt.IsAbandon == false))
                    {
                        res = "该库区类型已存在";
                    }
                    else
                    {
                        //新建实体
                        var entity = new InventoryAreaTypeViewModel { InventoryAreaTypeName = Name }.InitAddInventoryAreaType(CurUserID);

                        //更新
                        res = new DbEntities<InventoryAreaType>().SimpleClient.Insert(entity) ? "添加成功" : "添加失败";
                    }
                }
                else
                {
                    res = "登陆身份过期";
                }
            }
            return res;
        }
        #endregion

    }
}