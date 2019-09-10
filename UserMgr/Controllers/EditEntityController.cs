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
        /// <summary>
        /// 编辑页面访问权限
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改页面访问权限")]
        public ActionResult AccessMgr(string Id = "-1")
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
                    return View(Formatterr.ConvertToViewModel<PageViewModel>(curPage));
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
                    int res = new DbEntities<Page>().Db
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
                        TempData["Msg"] = "页面 " + model.PageUrl + " 更新成功";
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
        public ActionResult UserList(string Id = "-1")
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
                    return View(Formatterr.ConvertToViewModel<UserListViewModel>(curuser));
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
            //验证
            if (ModelState.IsValid)
            {
                //更新-指定列
                int res = new DbEntities<User>().Db
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
                    TempData["Msg"] = "用户 " + model.UserName + " 更新成功";
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
        public ActionResult UserGroup(string Id = "-1")
        {
            if (int.TryParse(Id, out int ugid)) 
            {
                //获取对象
                var curusergroup = new DbEntities<UserGroup>().SimpleClient.GetById(ugid);
                if (curusergroup != null)
                {
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<UserGroupViewModel>(curusergroup));
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
                    int res = new DbEntities<UserGroup>().Db
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
        public ActionResult Supplier(string Id = "-1")
        {
            if (int.TryParse(Id, out int sid)) 
            {
                //获取对象
                var curSupplier = new DbEntities<Supplier>().SimpleClient.GetById(sid);
                if (curSupplier != null)
                {
                    //返回视图模型
                    return View(Formatterr.ConvertToViewModel<SupplierViewModel>(curSupplier));
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
                    int res = new DbEntities<Supplier>().Db
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
        public ActionResult MaterialType(string Id = "-1")
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
                    return View(Formatterr.ConvertToViewModel<MaterialTypeViewModel>(curMaterialType));
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
                    int res = new DbEntities<MaterialType>().Db
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
        public ActionResult Warehouse(string Id = "-1")
        {
            if (int.TryParse(Id, out int wid)) 
            {
                //获取对象
                var curWarehouse = new DbEntities<Warehouse>().SimpleClient.GetById(Id);

                if (curWarehouse != null)
                {
                    //转换为视图类
                    return View(Formatterr.ConvertToViewModel<WarehouseViewModel>(curWarehouse));
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
                    int res = new DbEntities<Warehouse>().Db
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
        public ActionResult InventoryArea(string Id = "-1")
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
                    return View(Formatterr.ConvertToViewModel<InventoryAreaViewModel>(curInventoryArea));
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
                    int res = new DbEntities<InventoryArea>().Db
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
        public ActionResult InventoryLocation(string Id = "-1")
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
                    return View(Formatterr.ConvertToViewModel<InventoryLocationViewModel>(curInventoryLocation));
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
                    int res = new DbEntities<InventoryLocation>().Db
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
        public ActionResult Tray(string Id = "-1")
        {
            if (int.TryParse(Id, out int trid)) 
            {
                //获取对象
                var curTray = new DbEntities<Tray>().SimpleClient.GetById(trid);

                if (curTray != null) 
                {
                    //设置容器
                    SetSelectListItems.Container(this, curTray.Container);
                    //转为视图类
                    return View(Formatterr.ConvertToViewModel<TrayViewModel>(curTray));
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
                if (db.IsAny(tr => (tr.TrayNo == model.TrayNo || tr.TrayCode == model.TrayCode) && tr.TrayID == model.TrayID)) 
                {
                    ModelState.AddModelError("TrayCode", "托盘编号或条码已存在");
                }
                else
                {
                    //更新-指定列
                    int res = new DbEntities<Tray>().Db
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
                                    DataVersion = it.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                }).Where(it => it.TrayID == model.TrayID).ExecuteCommand();

                    //更新成功
                    if (res > 0)
                    {

                    }
                }
            }

            SetSelectListItems.Container(this, model.Container);
            return View();
        }
    }
}