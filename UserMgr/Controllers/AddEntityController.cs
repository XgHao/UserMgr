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
        /// <summary>
        /// 增加用户组
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "增加用户组")]
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
        public ActionResult UserGroup(UserGroupViewModel model)
        {
            //验证模型
            if (ModelState.IsValid)
            {
                //获取当前用户信息
                if (new IdentityAuth().GetCurUserID(HttpContext,out int CurUserID))
                {
                    var db = new DbEntities<UserGroup>().SimpleClient;
                    //用户组名与编码不能重复
                    if (db.IsAny(ug => ug.UserGroupName == model.UserGroupName || ug.UserGroupCode == model.UserGroupCode))
                    {
                        //名称或者编码重复
                        ModelState.AddModelError("UserGroupCode", "用户组名或编码已存在");
                    }
                    else
                    {
                        //转换为实体模型
                        var usergroup = model.ConvertUserGroup(CurUserID);

                        //插入数据
                        if (db.Insert(usergroup))
                        {
                            //新增成功
                            TempData["Msg"] = "用户组 " + model.UserGroupName + " 添加成功";
                            return RedirectToAction("UserGroupMgr", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Msg", "添加失败");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Msg", "登录身份已过期，请重新登录");
                }
            }
            return View(model);
        }



        /// <summary>
        /// 增加供应商
        /// </summary>
        /// <returns></returns>
        [IdentityAuth(UrlName = "增加供应商")]
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
        public ActionResult Supplier(SupplierViewModel model)
        {
            //验证模型
            if (ModelState.IsValid)
            {
                //判断名称与编码重复
                var db = new DbEntities<Supplier>().SimpleClient;

                if (db.IsAny(su => su.SupplierName == model.SupplierName || su.SupplierCode == model.SupplierCode)) 
                {
                    ModelState.AddModelError("SupplierCode", "供应商名称或者编码已存在");
                }
                else
                {
                    if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                    {
                        //转换为实体
                        Supplier entity = model.ConvertToSupplier(curUserID);

                        if (new DbEntities<Supplier>().SimpleClient.Insert(entity))
                        {
                            TempData["Msg"] = "供应商 " + model.SupplierName + " 添加成功";
                            return RedirectToAction("Supplier", "Materials");
                        }
                        else
                        {
                            ModelState.AddModelError("Msg", "添加失败");
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
        public ActionResult MaterialType()
        {
            //设置下拉框
            SetSelectListItems.MaterialType(this);
            return View();
        }

        /// <summary>
        /// 物资种类[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MaterialType(MaterialTypeViewModel model)
        {
            //验证
            if (ModelState.IsValid)
            {

                //名称编码是否有重复
                var db = new DbEntities<MaterialType>().SimpleClient;

                if (db.IsAny(mt => mt.MaterialTypeName == model.MaterialTypeName || mt.MaterialTypeCode == model.MaterialTypeCode)) 
                {
                    ModelState.AddModelError("MaterialTypeCode", "物资种类名称或编码已存在");
                }
                else
                {
                    if (new IdentityAuth().GetCurUserID(HttpContext,out int curUserID))
                    {
                        //转换为对应实体
                        MaterialType entity = model.ConvertToMaterialType(curUserID);

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = "物资种类 " + entity.MaterialTypeName + " 添加成功";
                            return RedirectToAction("TypeList", "Materials");
                        }
                        else
                        {
                            ModelState.AddModelError("Msg", "添加失败");
                        }
                    }
                }
            }

            SetSelectListItems.MaterialType(this);
            return View();
        }



        /// <summary>
        /// 物资
        /// </summary>
        /// <returns></returns>
        public ActionResult Material()
        {
            //设置下拉框
            SetSelectListItems.MaterialType(this);
            SetSelectListItems.UnitList(this);

            return View();
        }

        /// <summary>
        /// 物资[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Material(MaterialViewModel model)
        {
            if (model.UnitInput == null && model.Unit == "-1")
            {
                ModelState.AddModelError("UnitInput", "请输入或选择一项单位");
            }
            if (model.MaterialTypeID == -1) 
            {
                ModelState.AddModelError("MaterialTypeID", "请选择一项物资种类");
            }
            if (ModelState.IsValid)
            {
                //获得对应实体
                if (new IdentityAuth().GetCurUserID(HttpContext, out int creater)) 
                {
                    Material entity = model.ConertMaterial(creater);

                    //插入数据
                    if (new DbEntities<Material>().SimpleClient.Insert(entity)) 
                    {
                        TempData["Msg"] = "添加成功";
                        return RedirectToAction("MaterialList", "Materials");
                    }
                }
            }
            
            //设置下拉框
            SetSelectListItems.MaterialType(this);
            SetSelectListItems.UnitList(this);
            TempData["Msg"] = "添加失败，请检查信息";
            return View();
        }
    }
}