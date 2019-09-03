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

namespace UserMgr.Controllers
{
    public class EditEntityController : Controller
    {
        /// <summary>
        /// 编辑页面访问权限
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改页面访问权限")]
        public ActionResult AccessMgr(string Id = "")
        {
            //设置SelectListItem，并返回当前编辑对象
            Page pageurl = SetSelectListItems.User(this, Id);

            if (pageurl == null)   
            {
                //返回列表页
                return RedirectToAction("AccessMgr", "Home");
            }

            return View(new PageViewModel
            {
                PageID = pageurl.PageID,
                PageUrl = pageurl.PageUrl,
                PageName = pageurl.PageName,
                PageClass = (int)pageurl.PageClass
            });
        }

        /// <summary>
        /// 编辑页面访问权限[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AccessMgr(PageViewModel model)
        {
            Page pageurl = SetSelectListItems.User(this, model.PageID.ToString());
            if (ModelState.IsValid) 
            {
                pageurl.PageClass = model.PageClass;
                pageurl.PageName = model.PageName;
                pageurl.WhiteList = model.WhiteList ?? "";
                pageurl.BlackList = model.BlackList ?? "";

                if (new DbEntities<Page>().SimpleClient.Update(pageurl)) 
                {
                    return RedirectToAction("AccessMgr", "Home");
                }
                ModelState.AddModelError("PageClass", "更新失败");
            }
            return View(model);
        }



        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改用户信息")]
        public ActionResult UserList(string Id = "")
        {
            var curuser = new DbEntities<User>().SimpleClient.GetById(Id);

            if (curuser != null) 
            {
                //查找用户组
                var grouplist = new DbEntities<UserGroup>().SimpleClient.GetList().Where(ug => ug.UserGroupID != 0);
                List<SelectListItem> selectLists = new List<SelectListItem>();
                foreach (var group in grouplist)
                {
                    selectLists.Add(new SelectListItem
                    {
                        Selected = group.UserGroupID == curuser.UserGroupID ? true : false,
                        Text = group.UserGroupName,
                        Value = group.UserGroupID.ToString()
                    });
                }

                return View(new UserListViewModel
                {
                    UserGroupID = curuser.UserGroupID,
                    UserCreater = curuser.UserCreater,
                    UserCreateTime = (DateTime)curuser.UserCreateTime,
                    UserDesc = curuser.UserDesc,
                    UserGroupList = selectLists.AsEnumerable(),
                    UserID = curuser.UserID,
                    UserName = curuser.UserName
                });
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
                int res = new DbEntities<User>().Db.Updateable<User>().SetColumns(it => new User() { UserDesc = model.UserDesc, UserGroupID = model.UserGroupID }).Where(it => it.UserID == model.UserID).ExecuteCommand();
                if (res >= 1) 
                {
                    TempData["Msg"] = "用户 " + model.UserName + " 更新成功";
                    return RedirectToAction("UserList", "Home");
                }
            }
            TempData["Msg"] = "更新失败，请检查信息";
            return View(model);
        }



        /// <summary>
        /// 编辑用户组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IdentityAuth(UrlName = "修改用户组信息")]
        public ActionResult UserGroup(string Id = "")
        {
            var curusergroup = new DbEntities<UserGroup>().SimpleClient.GetById(Id);

            if (curusergroup != null) 
            {

                return View(new UserGroupViewModel
                {
                    UserGroupID = curusergroup.UserGroupID,
                    UserGroupClass = (int)curusergroup.UserGroupClass,
                    UserGroupNo = curusergroup.UserGroupNo,
                    UserGroupDesc = curusergroup.UserGroupDesc,
                    UserGroupName = curusergroup.UserGroupName
                });
            }
            return RedirectToAction("UserGroup", "Home");
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
                int res = new DbEntities<UserGroup>().Db
                              .Updateable<UserGroup>()
                              .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curuserid),
                              it => new UserGroup {
                                  UserGroupNo = model.UserGroupNo,
                                  UserGroupClass = model.UserGroupClass,
                                  UserGroupDesc = model.UserGroupDesc,
                                  UserGroupName = model.UserGroupName,
                                  UserGroupChanger = curuserid,
                                  UserGroupChangeTime = DateTime.Now
                              })
                              .Where(it => it.UserGroupID == model.UserGroupID).ExecuteCommand();
            }
            return RedirectToAction("UserGroupMgr", "Home");
        }



        /// <summary>
        /// 编辑仓库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Warehouse(string Id = "")
        {
            var curWarehouse = new DbEntities<Warehouse>().SimpleClient.GetById(Id);

            if (curWarehouse != null) 
            {
                WarehouseViewModel model = new WarehouseViewModel();
                foreach (var item in typeof(Warehouse).GetProperties())
                {
                }
                

                return View(new WarehouseViewModel {
                    WarehouseID=curWarehouse.WarehouseID,
                    WarehouseName=curWarehouse.WarehouseName,
                    WarehouseNo=curWarehouse.WarehouseNo,
                });
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

                if (db.IsAny(w => w.WarehouseName == model.WarehouseName || w.WarehouseNo == model.WarehouseNo)) 
                {
                    ModelState.AddModelError("WarehouseNo", "仓库名称或编号已存在");
                }
                else
                {
                    //更新
                    int res = new DbEntities<Warehouse>().Db
                                .Updateable<Warehouse>()
                                .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int curUserID), 
                                it => new Warehouse {
                                    WarehouseNo = model.WarehouseNo,
                                    WarehouseName = model.WarehouseName,
                                    WarehouseType = model.WarehouseType,
                                    Remark = model.Remark,
                                    OtherInfo = model.Remark,
                                    Enable = model.Enable,
                                    DataVersion = model.DataVersion + 1,
                                    Changer = curUserID,
                                    ChangeTime = DateTime.Now
                                })
                                .Where(it => it.WarehouseID == model.WarehouseID).ExecuteCommand();
                }
            }
            return View();
        }
    }
}