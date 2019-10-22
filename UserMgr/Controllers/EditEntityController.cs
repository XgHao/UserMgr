using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Models;
using UserMgr.DB;
using UserMgr.Security;
using UserMgr.Entities;
using UserMgr.Formatter;
using System.Reflection;

namespace UserMgr.Controllers
{
    public class EditEntityController : Controller
    {
        #region 基础功能-编辑

        /// <summary>
        /// 编辑页面访问权限
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改页面访问权限")]
        public ActionResult AccessMgr(string Id)
        {
            if (int.TryParse(Id, out int amid))
            {
                //获取对象
                var curPage = new DbEntities<Page>().SimpleClient.GetById(amid);
                if (curPage != null)
                {
                    //设置用户列表
                    SetSelectListItems.User(this);
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<PageViewModel, Page>(curPage));
                }
            }

            //操作失败，返回列表页
            return RedirectToAction("AccessMgr", "Home");
        }

        /// <summary>
        /// 编辑页面访问权限[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AccessMgr(PageViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //名称不重复
                var db = new DbEntities<Page>().SimpleClient;

                //页面名称不重复
                if (db.IsAny(p => p.PageName == model.PageName && p.PageID != model.PageID))
                {
                    ModelState.AddModelError("PageName", "该页面名称已存在");
                }
                else
                {
                    //更新指定列数据
                    int res = new DbContext().Db
                                .Updateable<Page>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new Page
                                {
                                    PageName = model.PageName,
                                    PageClass = model.PageClass,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                })
                                .Where(it => it.PageID == model.PageID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = $"页面 {model.PageUrl} 更新成功";
                        return RedirectToAction("AccessMgr", "Home");
                    }
                }
            }

            //更新失败
            TempData["Msg"] = "更新失败，请重试";
            SetSelectListItems.User(this);
            return View(model);
        }



        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改用户信息")]
        public ActionResult UserList(string Id)
        {
            if (int.TryParse(Id, out int ulid))
            {
                //获取对象
                var curuser = new DbEntities<User>().SimpleClient.GetById(ulid);
                if (curuser != null)
                {
                    //设置用户组列表
                    SetSelectListItems.UserGroup(this, curuser.UserGroupID);
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<UserListViewModel, User>(curuser));
                }
            }

            //操作失败，返回列表页
            return RedirectToAction("UserList", "Home");
        }

        /// <summary>
        /// 编辑用户[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserList(UserListViewModel model)
        {
            //验证模型中的部分属性
            if (ModelState.IsPartValid(new List<string> { "UserDesc", "UserGroupID", "UserID", "UserName" }))
            {
                //更新-指定列
                int res = new DbContext().Db
                            .Updateable<User>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                            it => new User
                            {
                                UserDesc = model.UserDesc,
                                UserGroupID = model.UserGroupID,
                                Changer = curUserID,
                                ChangeTime = DateTime.Now
                            })
                            .Where(it => it.UserID == model.UserID).ExecuteCommand();

                //更新成功
                if (res > 0)
                {
                    TempData["Msg"] = $"用户 {model.UserName} 更新成功";
                    return RedirectToAction("UserList", "Home");
                }
            }

            //更新失败
            TempData["Msg"] = "更新失败，请检查信息";
            SetSelectListItems.UserGroup(this, model.UserGroupID);
            return View(model);
        }



        /// <summary>
        /// 编辑用户组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改用户组信息")]
        public ActionResult UserGroup(string Id)
        {
            if (int.TryParse(Id, out int ugid))
            {
                //获取对象
                var curusergroup = new DbEntities<UserGroup>().SimpleClient.GetById(ugid);
                if (curusergroup != null)
                {
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<UserGroupViewModel, UserGroup>(curusergroup));
                }
            }

            //操作失败，返回列表页
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("UserGroupMgr", "Home");
        }

        /// <summary>
        /// 编辑用户组[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserGroup(UserGroupViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //名称编号不重复
                var db = new DbEntities<UserGroup>().SimpleClient;

                if (db.IsAny(ug => (ug.UserGroupName == model.UserGroupName || ug.UserGroupNo == model.UserGroupNo) && ug.UserGroupID != model.UserGroupID))
                {
                    ModelState.AddModelError("UserGroupNo", "用户组名称或编号已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                              .Updateable<UserGroup>()
                              .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curuserid),
                              it => new UserGroup
                              {
                                  UserGroupNo = model.UserGroupNo,
                                  UserGroupClass = model.UserGroupClass,
                                  UserGroupDesc = model.UserGroupDesc,
                                  UserGroupName = model.UserGroupName,
                                  Changer = curuserid,
                                  ChangeTime = DateTime.Now
                              })
                              .Where(it => it.UserGroupID == model.UserGroupID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("UserGroupMgr", "Home");
                    }
                }
            }

            //更新失败
            TempData["Msg"] = "更新失败";
            return View(model);
        }



        /// <summary>
        /// 编辑供应商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改供应商信息")]
        public ActionResult Supplier(string Id)
        {
            if (int.TryParse(Id, out int sid))
            {
                //获取对象
                var curSupplier = new DbEntities<Supplier>().SimpleClient.GetById(sid);
                if (curSupplier != null)
                {
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<SupplierViewModel, Supplier>(curSupplier));
                }
            }

            //操作失败，返回列表页
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("Supplier", "Materials");
        }

        /// <summary>
        /// 编辑供应商[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Supplier(SupplierViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //名称编码不重复
                var db = new DbEntities<Supplier>().SimpleClient;

                if (db.IsAny(s => (s.SupplierName == model.SupplierName || s.SupplierNo == model.SupplierNo) && s.SupplierID != model.SupplierID))
                {
                    ModelState.AddModelError("SupplierNo", "供应商名称或编号已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                               .Updateable<Supplier>()
                               .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                               it => new Supplier
                               {
                                   SupplierName = model.SupplierName,
                                   SupplierNo = model.SupplierNo,
                                   SupplierPhoNum = model.SupplierPhoNum,
                                   SupplierEmail = model.SupplierEmail,
                                   SupplierRemark = model.SupplierRemark,
                                   DataVersion = it.DataVersion + 1,
                                   Changer = curUserID,
                                   ChangeTime = DateTime.Now
                               })
                               .Where(it => it.SupplierID == model.SupplierID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("Supplier", "Materials");
                    }
                }
            }

            //更新失败
            TempData["Msg"] = "更新失败";
            return View(model);
        }



        /// <summary>
        /// 编辑物资种类
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改物资种类信息")]
        public ActionResult MaterialType(string Id)
        {
            if (int.TryParse(Id, out int mtid))
            {
                //获取对象
                var curMaterialType = new DbEntities<MaterialType>().SimpleClient.GetById(Id);

                if (curMaterialType != null)
                {
                    //设置物资种类列表-父类
                    SetSelectListItems.MaterialType(this, mtid);
                    //设置供应商列表
                    SetSelectListItems.Supplier(this, curMaterialType.SupplierID);
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<MaterialTypeViewModel, MaterialType>(curMaterialType));
                }
            }

            //操作失败返回列表页
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("TypeList", "Materials");
        }

        /// <summary>
        /// 编辑物资种类[HttpPost]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MaterialType(MaterialTypeViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                var db = new DbEntities<MaterialType>().SimpleClient;

                if (db.IsAny(mt => (mt.MaterialTypeName == model.MaterialTypeName || mt.MaterialTypeNo == model.MaterialTypeNo) && mt.MaterialTypeID != model.MaterialTypeID))
                {
                    ModelState.AddModelError("MaterialTypeNo", "供应商名称或编号已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                                .Updateable<MaterialType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new MaterialType
                                {
                                    MaterialTypeName = model.MaterialTypeName,
                                    MaterialTypeNo = model.MaterialTypeNo,
                                    MaterialTypeRoot = model.MaterialTypeRoot,
                                    MaterialTypePrice = model.MaterialTypePrice,
                                    SupplierID = model.SupplierID,
                                    DataVersion = it.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                }).Where(it => it.MaterialTypeID == model.MaterialTypeID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("TypeList", "Materials");
                    }
                }
            }

            //更新失败
            SetSelectListItems.Supplier(this, model.SupplierID);
            SetSelectListItems.MaterialType(this, model.MaterialTypeID);
            return View(model);
        }



        /// <summary>
        /// 编辑仓库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改仓库信息")]
        public ActionResult Warehouse(string Id)
        {
            if (int.TryParse(Id, out int wid))
            {
                //获取对象
                var curWarehouse = new DbEntities<Warehouse>().SimpleClient.GetById(Id);

                if (curWarehouse != null)
                {
                    //转换为视图类
                    return View(Formatterr.ConvertToViewModel<WarehouseViewModel, Warehouse>(curWarehouse));
                }
            }

            //操作失败，列表页
            TempData["Msg"] = "没有找到该仓库对象";
            return RedirectToAction("List", "Warehouse");
        }

        /// <summary>
        /// 编辑仓库[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Warehouse(WarehouseViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //名称编号不能重复
                var db = new DbEntities<Warehouse>().SimpleClient;

                if (db.IsAny(w => (w.WarehouseName == model.WarehouseName || w.WarehouseNo == model.WarehouseNo) && w.WarehouseID != model.WarehouseID))
                {
                    ModelState.AddModelError("WarehouseNo", "仓库名称或编号已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                                .Updateable<Warehouse>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new Warehouse
                                {
                                    WarehouseNo = model.WarehouseNo,
                                    WarehouseName = model.WarehouseName,
                                    WarehouseType = model.WarehouseType,
                                    Remark = model.Remark,
                                    OtherInfo = model.OtherInfo,
                                    Enable = model.Enable,
                                    DataVersion = it.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                })
                                .Where(it => it.WarehouseID == model.WarehouseID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("List", "Warehouse");
                    }
                }
            }

            //更新失败
            TempData["Msg"] = "更新失败";
            return View(model);
        }



        /// <summary>
        /// 编辑库区
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改库区信息")]
        public ActionResult InventoryArea(string Id)
        {
            if (int.TryParse(Id, out int iaid))
            {
                //获取对象
                var curInventoryArea = new DbEntities<InventoryArea>().SimpleClient.GetById(iaid);

                if (curInventoryArea != null)
                {
                    //设置仓库列表
                    SetSelectListItems.Warehouse(this, curInventoryArea.WarehouseID);
                    //转换为视图类
                    return View(Formatterr.ConvertToViewModel<InventoryAreaViewModel, InventoryArea>(curInventoryArea));
                }
            }



            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("InventoryArea", "Warehouse");
        }

        /// <summary>
        /// 编辑库区[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InventoryArea(InventoryAreaViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //名称编号不重复
                var db = new DbEntities<InventoryArea>().SimpleClient;

                if (db.IsAny(ia => (ia.InventoryAreaName == model.InventoryAreaName || ia.InventoryAreaNo == model.InventoryAreaNo) && ia.InventoryAreaID != model.InventoryAreaID))
                {
                    ModelState.AddModelError("InventoryAreaNo", "库区名称或编号已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                                .Updateable<InventoryArea>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new InventoryArea
                                {
                                    InventoryAreaName = model.InventoryAreaName,
                                    InventoryAreaNo = model.InventoryAreaNo,
                                    InventoryAreaType = model.InventoryAreaType,
                                    WarehouseID = model.WarehouseID,
                                    Remark = model.Remark,
                                    OtherInfo = model.OtherInfo,
                                    Enable = model.Enable,
                                    DataVersion = it.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                })
                                .Where(it => it.InventoryAreaID == model.InventoryAreaID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("InventoryArea", "Warehouse");
                    }
                }
            }

            //更新失败
            SetSelectListItems.Warehouse(this);
            TempData["Msg"] = "更新失败";
            return View(model);
        }



        /// <summary>
        /// 编辑库位
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改库位信息")]
        public ActionResult InventoryLocation(string Id)
        {
            if (int.TryParse(Id, out int ilid))
            {
                //获取对象
                var curInventoryLocation = new DbEntities<InventoryLocation>().SimpleClient.GetById(Id);

                if (curInventoryLocation != null)
                {
                    //设置库区列表
                    SetSelectListItems.InventoryArea(this, curInventoryLocation.InventoryAreaID);
                    //转为视图类
                    return View(Formatterr.ConvertToViewModel<InventoryLocationViewModel, InventoryLocation>(curInventoryLocation));
                }
            }

            //操作失败，返回列表页
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("InventoryLocation", "Warehouse");
        }

        /// <summary>
        /// 编辑库位[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InventoryLocation(InventoryLocationViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //名称与编号不重复
                var db = new DbEntities<InventoryLocation>().SimpleClient;

                if (db.IsAny(il => (il.InventoryLocationName == model.InventoryLocationName || il.InventoryLocationNo == model.InventoryLocationNo) && il.InventoryLocationID != model.InventoryLocationID))
                {
                    ModelState.AddModelError("InventoryLocationNo", "库位名称或编号已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                                .Updateable<InventoryLocation>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new InventoryLocation
                                {
                                    InventoryLocationName = model.InventoryLocationName,
                                    InventoryLocationNo = model.InventoryLocationNo,
                                    Row = model.Row,
                                    Line = model.Line,
                                    Layer = model.Layer,
                                    EnterDistance = model.EnterDistance,
                                    ExitDistance = model.ExitDistance,
                                    Enable = model.Enable,
                                    InventoryLocationGroup = model.InventoryLocationGroup,
                                    InventoryLocationHeight = model.InventoryLocationHeight,
                                    InventoryLocationLength = model.InventoryLocationLength,
                                    InventoryLocationWidth = model.InventoryLocationWidth,
                                    InventoryLocationNarrow = model.InventoryLocationNarrow,
                                    InventoryLocationType = model.InventoryLocationType,
                                    InventoryAreaID = model.InventoryAreaID,
                                    FrontAndBack = model.FrontAndBack,
                                    Container = model.Container,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1
                                })
                                .Where(it => it.InventoryLocationID == model.InventoryLocationID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("InventoryLocation", "Warehouse");
                    }
                }
            }

            //更新失败
            SetSelectListItems.InventoryArea(this);
            TempData["Msg"] = "更新失败";
            return View(model);
        }



        /// <summary>
        /// 编辑托盘
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改托盘信息")]
        public ActionResult Tray(string Id)
        {
            if (int.TryParse(Id, out int trid))
            {
                //获取对象
                var curTray = new DbEntities<Tray>().SimpleClient.GetById(trid);

                if (curTray != null)
                {
                    //设置容器
                    SetSelectListItems.Container(this, curTray.Container);
                    SetSelectListItems.TrayType(this, curTray.TrayType);
                    SetSelectListItems.Status(this, curTray.Status);

                    TrayViewModel model = Formatterr.ConvertToViewModel<TrayViewModel, Tray>(curTray);
                    return View(model);
                }
            }

            //操作失败，返回列表页
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("Tray", "Warehouse");
        }

        /// <summary>
        /// 编辑托盘[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Tray(TrayViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                var db = new DbEntities<Tray>().SimpleClient;

                //编号和条码不重复
                if (db.IsAny(tr => (tr.TrayNo == model.TrayNo || tr.TrayCode == model.TrayCode) && tr.TrayID != model.TrayID))
                {
                    ModelState.AddModelError("TrayCode", "托盘编号或条码已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbContext().Db
                                .Updateable<Tray>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new Tray
                                {
                                    TrayNo = model.TrayNo,
                                    TrayCode = model.TrayCode,
                                    TrayType = model.TrayType,
                                    Weight = model.Weight,
                                    Height = model.Height,
                                    Container = model.Container,
                                    Remark = model.Remark,
                                    Status = model.Status,
                                    DataVersion = it.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                }).Where(it => it.TrayID == model.TrayID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("Tray", "Warehouse");
                    }
                }
            }

            //更新失败
            SetSelectListItems.Container(this, model.Container);
            SetSelectListItems.TrayType(this, model.TrayType);
            SetSelectListItems.Status(this, model.Status);

            TempData["Msg"] = "更新失败";
            return View(model);
        }




        /// <summary>
        /// 编辑托盘细节
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改托盘细节信息")]
        public ActionResult TrayDetail(string Id)
        {
            if (int.TryParse(Id, out int tdid)) 
            {
                //获取对象
                var curTrayDetail = new DbEntities<TrayDetail>().SimpleClient.GetById(tdid);

                if (curTrayDetail != null) 
                {
                    //当前关联的托盘信息
                    var curTray = new DbEntities<Tray>().SimpleClient.GetById(curTrayDetail.TrayID);
                    if (curTray != null)
                    { 
                        var model = Formatterr.ConvertToViewModel<TrayDetailViewModel, TrayDetail>(curTrayDetail);

                        model.CurTrayInfo = $"{curTray.TrayNo}   {curTray.TrayType}   {curTray.TrayCode}   {curTray.Remark}";

                        //设置下拉框数据
                        SetSelectListItems.InboundTaskDetail(this, curTrayDetail.InboundTaskDetailID);
                        SetSelectListItems.Material(this, curTrayDetail.MaterialSizeID);
                        SetSelectListItems.Status(this, curTrayDetail.Status);

                        //转为视图类
                        return View(model);
                    }
                }
            }

            //操作失败，返回列表页
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("TrayDetail", "Warehouse");
        }

        /// <summary>
        /// 编辑托盘细节[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TrayDetail(TrayDetailViewModel model)
        {
            //验证
            if (ModelState.IsValid) 
            {
                //更新-指定列
                int res = new DbContext().Db
                            .Updateable<TrayDetail>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                            it => new TrayDetail
                            {
                                Status = model.Status,
                                ProductionDate = model.ProductionDate,
                                ParcelMeasure = model.ParcelMeasure,
                                OutboundPostMark = model.OutboundPostMark,
                                MaterialSN = model.MaterialSN,
                                MaterialSizeID = model.MaterialSizeID,
                                InboundTaskDetailID = model.InboundTaskDetailID,
                                InboundPostMark = model.InboundPostMark,
                                GroupTrayOrder = model.GroupTrayOrder,
                                DataVersion = it.DataVersion + 1,
                                ChangeTime = DateTime.Now,
                                Changer = curUserID
                            }).Where(it => it.TrayDetailID == model.TrayDetailID).ExecuteCommand();

                //更新成功
                if (res > 0)
                {
                    TempData["Msg"] = "更新成功";
                    return RedirectToAction("TrayDetail", "Warehouse");
                }
            }

            SetSelectListItems.InboundTaskDetail(this, model.InboundTaskDetailID);
            SetSelectListItems.Material(this, model.MaterialSizeID);
            SetSelectListItems.Status(this, model.Status);
            return View(model);
        }



        /// <summary>
        /// 编辑入库任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改入库任务信息")]
        public ActionResult InboundTask(string Id)
        {
            if (int.TryParse(Id, out int id))
            {
                //获取对象
                var curIbt = new DbEntities<InboundTask>().SimpleClient.GetById(id);

                if (curIbt != null)
                {
                    //设置状态-供应商
                    SetSelectListItems.Status(this, curIbt.Status);
                    SetSelectListItems.Supplier(this, curIbt.SupplierID);
                    SetSelectListItems.InboundType(this, curIbt.InboundType);

                    //转为视图类
                    return View(Formatterr.ConvertToViewModel<InboundTaskViewModel, InboundTask>(curIbt));
                }
            }

            //操作失败，返回列表
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("InboundTask", "Warehouse");
        }

        /// <summary>
        /// 编辑入库任务[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InboundTask(InboundTaskViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {
                //更新
                int res = new DbContext().Db
                            .Updateable<InboundTask>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                            it => new InboundTask
                            {
                                SupplierID = model.SupplierID,
                                Status = model.Status,
                                OrderNo = model.OrderNo,
                                InboundRemark = model.InboundRemark,
                                InboundType = model.InboundType,
                                ExterNo = model.ExterNo,
                                DataVersion = it.DataVersion + 1,
                                Changer = curUserID,
                                ChangeTime = DateTime.Now
                            }).Where(it => it.InboundTaskID == model.InboundTaskID).ExecuteCommand();

                //更新成功
                if (res > 0)
                {
                    TempData["Msg"] = "更新成功";
                    return RedirectToAction("InboundTask", "Warehouse");
                }
            }

            //更新失败
            SetSelectListItems.Status(this, model.Status);
            SetSelectListItems.Supplier(this, model.SupplierID);
            SetSelectListItems.InboundType(this, model.InboundType);
            return View(model);
        }



        /// <summary>
        /// 编辑出库任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改出库任务信息")]
        public ActionResult OutboundTask(string Id)
        {
            if (int.TryParse(Id, out int obtid)) 
            {
                //获取对象
                var curObt = new DbEntities<OutboundTask>().SimpleClient.GetById(obtid);

                if (curObt != null) 
                {
                    //设置下拉框
                    SetSelectListItems.Status(this, curObt.Status);
                    SetSelectListItems.OutboundType(this, curObt.OutboundType);
                    SetSelectListItems.SaleType(this, curObt.SaleTypeID);

                    //转为视图类
                    return View(Formatterr.ConvertToViewModel<OutboundTaskViewModel, OutboundTask>(curObt));
                }
            }

            //操作失败，返回列表
            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("OutboundTask", "Warehouse");
        }

        /// <summary>
        /// 编辑出库任务[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OutboundTask(OutboundTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                //更新
                int res = new DbContext().Db
                            .Updateable<OutboundTask>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                            it => new OutboundTask
                            {
                                OutboundType = model.OutboundType,
                                OutboundRemark = model.OutboundRemark,
                                Client = model.Client,
                                ExterNo = model.ExterNo,
                                SaleNo = model.SaleNo,
                                SaleTypeID = model.SaleTypeID,
                                Department = model.Department,
                                Status = model.Status,
                                DataVersion = it.DataVersion + 1,
                                Changer = curUserID,
                                ChangeTime = DateTime.Now
                            }).Where(it => it.OutboundTaskID == model.OutboundTaskID).ExecuteCommand();

                //更新成功
                if (res > 0)
                {
                    TempData["Msg"] = "更新成功";
                    return RedirectToAction("OutboundTask", "Warehouse");
                }
            }

            //更新失败
            SetSelectListItems.Status(this, model.Status);
            SetSelectListItems.OutboundType(this, model.OutboundType);
            SetSelectListItems.SaleType(this, model.SaleTypeID);

            return View(model);
        }



        /// <summary>
        /// 编辑库存清单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改库存清单信息")]
        public ActionResult InventoryList(string Id)
        {
            if (int.TryParse(Id, out int ilid)) 
            {
                //获取对象
                var curIL = new DbEntities<InventoryList>().SimpleClient.GetById(ilid);

                if (curIL != null) 
                {
                    //下拉框
                    SetSelectListItems.InboundTaskDetail(this, curIL.InboundTaskDetailID);
                    SetSelectListItems.OutboundTaskDetail(this, curIL.OutboundTaskDetailID);
                    SetSelectListItems.Tray(this, Convert.ToInt32(curIL.TrayID));
                    SetSelectListItems.InventoryLocation(this, curIL.InventoryLocationID);
                    SetSelectListItems.Status(this, curIL.Status);

                    return View(Formatterr.ConvertToViewModel<InventoryListViewModel, InventoryList>(curIL));
                }
            }

            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("InventoryList", "Warehouse");
        }

        /// <summary>
        /// 编辑库存清单[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InventoryList(InventoryListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new DbEntities<InventoryList>().SimpleClient;

                //信息记录不重复
                if (db.IsAny(il => il.InboundTaskDetailID == model.InboundTaskDetailID && il.OutboundTaskDetailID == model.OutboundTaskDetailID && il.TrayID == model.TrayID && il.InventoryLocationID == model.InventoryLocationID && il.InventoryType == model.InventoryType && il.InventoryListID != model.InventoryListID)) 
                {
                    TempData["Msg"] = "该条记录已存在";
                }
                else
                {
                    int res = new DbContext().Db
                                .Updateable<InventoryList>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new InventoryList
                                {
                                    TrayID = model.TrayID,
                                    Status = model.Status,
                                    OutboundTaskDetailID = model.OutboundTaskDetailID,
                                    InventoryType = model.InventoryType,
                                    InventoryLocationID = model.InventoryLocationID,
                                    InboundTaskDetailID = model.InboundTaskDetailID,
                                    DataVersion = it.DataVersion + 1,
                                    ChangeTime = DateTime.Now,
                                    Changer = curUserID
                                }).Where(it => it.InventoryListID == model.InventoryListID).ExecuteCommand();

                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("InventoryList", "Warehouse");
                    }
                    TempData["Msg"] = "更新失败";
                }
            }

            //下拉框
            SetSelectListItems.InboundTaskDetail(this, model.InboundTaskDetailID);
            SetSelectListItems.OutboundTaskDetail(this, model.OutboundTaskDetailID);
            SetSelectListItems.Tray(this, Convert.ToInt32(model.TrayID));
            SetSelectListItems.InventoryLocation(this, model.InventoryLocationID);
            SetSelectListItems.Status(this, model.Status);
            return View(model);
        }



        /// <summary>
        /// 编辑波次信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改波次信息")]
        public ActionResult WavePicking(string Id)
        {
            if (int.TryParse(Id, out int wpid)) 
            {
                //获取对象
                var curWP = new DbEntities<WavePicking>().SimpleClient.GetById(wpid);

                if (curWP != null) 
                {
                    //下拉框
                    SetSelectListItems.PickingType(this, curWP.PickingType);
                    SetSelectListItems.WavePickingType(this, curWP.WavePickingTypeID);
                    SetSelectListItems.Status(this, curWP.Status);

                    return View(Formatterr.ConvertToViewModel<WavePickingViewModel, WavePicking>(curWP));
                }
            }

            TempData["Msg"] = "没有找到该对象";
            return RedirectToAction("WavePicking", "Warehouse");
        }

        /// <summary>
        /// 编辑波次信息[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WavePicking(WavePickingViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var db = new DbEntities<WavePicking>().SimpleClient;

                //检验信息
                if (false)
                {

                }
                else
                {
                    int res = new DbContext().Db
                                .Updateable<WavePicking>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID),
                                it => new WavePicking
                                {
                                    WavePickingTypeID = model.WavePickingTypeID,
                                    Status = model.Status,
                                    Remark = model.Remark,
                                    PickingType = model.PickingType,
                                    DataVersion = it.DataVersion + 1,
                                    ChangeTime = DateTime.Now,
                                    Changer = curUserID
                                }).Where(it => it.WavePickingID == model.WavePickingID).ExecuteCommand();

                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("WavePicking", "warehouse");
                    }
                    TempData["Msg"] = "更新失败";
                }
            }

            //下拉框
            SetSelectListItems.PickingType(this, model.PickingType);
            SetSelectListItems.WavePickingType(this, model.WavePickingTypeID);
            SetSelectListItems.Status(this, model.Status);
            return View(model);
        }

        #endregion




        #region 基础资料-编辑

        /// <summary>
        /// 编辑入库类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改入库类型")]
        public ActionResult InboundType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new InboundTypeViewModel { InboundTypeID = id, InboundTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的入库类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InboundType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<InboundType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.InboundTypeID;
                    res.Content = model.InboundTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前入库类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateInboundType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<InboundType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.InboundTypeName == Content && it.InboundTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<InboundType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new InboundType
                                {
                                    InboundTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.InboundTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑出库类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改出库类型")]
        public ActionResult OutboundType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new OutboundTypeViewModel { OutboundTypeID = id, OutboundTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的出库类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OutboundType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<OutboundType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.OutboundTypeID;
                    res.Content = model.OutboundTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前出库类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateOutboundType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<OutboundType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.OutboundTypeName == Content && it.OutboundTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<OutboundType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new OutboundType
                                {
                                    OutboundTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.OutboundTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑容器-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改容器")]
        public ActionResult Container(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new ContainerViewModel { ContainerID = id, ContainerName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的容器
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Container(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<Container>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.ContainerID;
                    res.Content = model.ContainerName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前容器
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateContainer(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<Container>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.ContainerName == Content && it.ContainerID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<Container>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new Container
                                {
                                    ContainerName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.ContainerID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑巷道-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改巷道")]
        public ActionResult Narrow(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new NarrowViewModel { NarrowID = id, NarrowName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的巷道
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Narrow(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<Narrow>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.NarrowID;
                    res.Content = model.NarrowName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前巷道
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateNarrow(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<Narrow>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.NarrowName == Content && it.NarrowID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<Narrow>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new Narrow
                                {
                                    NarrowName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.NarrowID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑拣货类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改拣货类型")]
        public ActionResult PickingType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new PickingTypeViewModel { PickingTypeID = id, PickingTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的拣货类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PickingType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<PickingType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.PickingTypeID;
                    res.Content = model.PickingTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前拣货类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdatePickingType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<PickingType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.PickingTypeName == Content && it.PickingTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<PickingType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new PickingType
                                {
                                    PickingTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.PickingTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑销售类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改销售类型")]
        public ActionResult SaleType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new SaleTypeViewModel { SaleTypeID = id, SaleTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的销售类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaleType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<SaleType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.SaleTypeID;
                    res.Content = model.SaleTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前销售类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateSaleType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<SaleType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.SaleTypeName == Content && it.SaleTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<SaleType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new SaleType
                                {
                                    SaleTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.SaleTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑单位-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改单位")]
        public ActionResult Unit(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new UnitViewModel { UnitID = id, UnitName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的单位
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Unit(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<Unit>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.UnitID;
                    res.Content = model.UnitName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前单位
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateUnit(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<Unit>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.UnitName == Content && it.UnitID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<Unit>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new Unit
                                {
                                    UnitName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.UnitID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑波次类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改波次类型")]
        public ActionResult WavePickingType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new WavePickingTypeViewModel { WavePickingTypeID = id, WavePickingTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的波次类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WavePickingType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<WavePickingType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.WavePickingTypeID;
                    res.Content = model.WavePickingTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前波次类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateWavePickingType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<WavePickingType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.WavePickingTypeName == Content && it.WavePickingTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<WavePickingType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new WavePickingType
                                {
                                    WavePickingTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.WavePickingTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑托盘类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改托盘类型")]
        public ActionResult TrayType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new TrayTypeViewModel { TrayTypeID = id, TrayTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的托盘类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TrayType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<TrayType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.TrayTypeID;
                    res.Content = model.TrayTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前托盘类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateTrayType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<TrayType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.TrayTypeName == Content && it.TrayTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<TrayType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new TrayType
                                {
                                    TrayTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.TrayTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }



        /// <summary>
        /// 编辑库区类型-页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改库区类型")]
        public ActionResult InventoryAreaType(string ID, string Content)
        {
            if (int.TryParse(ID, out int id))
            {
                return View(new InventoryAreaTypeViewModel { InventoryAreaTypeID = id, InventoryAreaTypeName = Content });
            }

            return RedirectToAction("NotFound", "Home");
        }

        /// <summary>
        /// [AJAX] - 选择要修改的库区类型
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InventoryAreaType(string ID)
        {
            var res = new ChangeEntityViewModel { Flag = "未找到对象，可能已被删除" };
            if (int.TryParse(ID, out int id))
            {
                //查找对象
                var model = new DbEntities<InventoryAreaType>().SimpleClient.GetById(id);

                //对象存在，并且没有被删除
                if (model != null && !model.IsAbandon)
                {
                    res.ID = model.InventoryAreaTypeID;
                    res.Content = model.InventoryAreaTypeName;
                    res.Flag = "OK";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// [AJAX] - 更新当前库区类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateInventoryAreaType(string ID, string Content)
        {
            string res = "出错了，名称不合法或请刷新页面.";
            string Name = Content.Trim();

            if (int.TryParse(ID, out int id) && !string.IsNullOrEmpty(Name))
            {
                var db = new DbEntities<InventoryAreaType>().SimpleClient;

                //是否重复
                if (db.IsAny(it => it.InventoryAreaTypeName == Content && it.InventoryAreaTypeID != id))
                {
                    res = "该名称已存在";
                }
                else
                {
                    //更新
                    int cnt = new DbContext().Db
                                .Updateable<InventoryAreaType>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                                it => new InventoryAreaType
                                {
                                    InventoryAreaTypeName = Name,
                                    Changer = CurUserID,
                                    ChangeTime = DateTime.Now,
                                    DataVersion = it.DataVersion + 1,
                                }).Where(it => it.InventoryAreaTypeID == id).ExecuteCommand();

                    res = cnt > 0 ? "更新成功" : "更新失败";
                }
            }

            return res;
        }

        #endregion
    }
}