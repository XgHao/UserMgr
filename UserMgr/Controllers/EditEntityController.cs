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
            var curPage = new DbEntities<Page>().SimpleClient.GetById(Id);
            if (curPage != null) 
            {
                //获取用户列表
                SetSelectListItems.User(this);

                return View(Formatterr.ConvertToViewModel<PageViewModel>(curPage));
            }

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
            if (ModelState.IsValid) 
            {
                //名称不重复
                var db = new DbEntities<Page>().SimpleClient;

                if (db.IsAny(p => p.PageName == model.PageName && p.PageID != model.PageID)) 
                {
                    ModelState.AddModelError("PageName", "该页面名称已存在");
                }
                else
                {
                    //更新
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

                    if (res > 0)
                    {
                        TempData["Msg"] = "页面 " + model.PageUrl + " 更新成功";
                        return RedirectToAction("AccessMgr", "Home");
                    }
                }
            }
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
            var curuser = new DbEntities<User>().SimpleClient.GetById(Id);

            if (curuser != null) 
            {
                SetSelectListItems.UserGroup(this, curuser.UserGroupID);

                return View(Formatterr.ConvertToViewModel<UserListViewModel>(curuser));
            }

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
            if (ModelState.IsValid)
            {
                //更新
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

                if (res > 0)
                {
                    TempData["Msg"] = "用户 " + model.UserName + " 更新成功";
                    return RedirectToAction("UserList", "Home");
                }
            }
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
            var curusergroup = new DbEntities<UserGroup>().SimpleClient.GetById(Id);

            if (curusergroup != null) 
            {
                return View(Formatterr.ConvertToViewModel<UserGroupViewModel>(curusergroup));
            }

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
                    //更新
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

                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("UserGroupMgr", "Home");
                    }
                }
            }

            TempData["Msg"] = "更新失败";
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
            var curWarehouse = new DbEntities<Warehouse>().SimpleClient.GetById(Id);

            if (curWarehouse != null) 
            {
                //转换为视图类
                return View(Formatterr.ConvertToViewModel<WarehouseViewModel>(curWarehouse));
            }

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
                    //更新
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
                                    DataVersion = model.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                })
                                .Where(it => it.WarehouseID == model.WarehouseID).ExecuteCommand();

                    if (res > 0) 
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("List", "Warehouse");
                    }
                }
            }
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
            var curInventoryArea = new DbEntities<InventoryArea>().SimpleClient.GetById(Id);

            if (curInventoryArea != null) 
            {
                //转换为视图类
                SetSelectListItems.Warehouse(this, curInventoryArea.WarehouseID);
                return View(Formatterr.ConvertToViewModel<InventoryAreaViewModel>(curInventoryArea));
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
                    //更新
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
                                    DataVersion = model.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                })
                                .Where(it => it.InventoryAreaID == model.InventoryAreaID).ExecuteCommand();

                    if (res > 0)
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("InventoryArea", "Warehouse");
                    }
                }
            }

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
            var curInventoryLocation = new DbEntities<InventoryLocation>().SimpleClient.GetById(Id);

            if (curInventoryLocation != null) 
            {
                //转为视图类
                SetSelectListItems.InventoryArea(this, curInventoryLocation.InventoryAreaID);
                return View(Formatterr.ConvertToViewModel<InventoryLocationViewModel>(curInventoryLocation));
            }

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
                    //更新
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
                                    DataVersion = model.DataVersion + 1
                                })
                                .Where(it => it.InventoryLocationID == model.InventoryLocationID).ExecuteCommand();

                    if (res > 0) 
                    {
                        TempData["Msg"] = "更新成功";
                        return RedirectToAction("InventoryLocation", "Warehouse");
                    }
                }
            }

            SetSelectListItems.InventoryArea(this);
            TempData["Msg"] = "更新失败";
            return View(model);
        }
    }
}