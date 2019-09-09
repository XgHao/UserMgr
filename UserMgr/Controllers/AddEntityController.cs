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
                    if (db.IsAny(ug => ug.UserGroupName == model.UserGroupName || ug.UserGroupNo == model.UserGroupNo))
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
        [IdentityAuth(UrlName = "物资种类")]
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

                if (db.IsAny(mt => mt.MaterialTypeName == model.MaterialTypeName || mt.MaterialTypeNo == model.MaterialTypeNo)) 
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
        [IdentityAuth(UrlName = "新增物资")]
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
                        Warehouse entity = model as Warehouse;
                        entity.Creater = curUserID;
                        entity.CreateTime = DateTime.Now;
                        entity.DataVersion = 1;

                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = "仓库 " + entity.WarehouseName + " 添加成功";
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
                        InventoryArea entity = model as InventoryArea;
                        entity.Creater = curUserID;
                        entity.CreateTime = DateTime.Now;
                        entity.DataVersion = 1;
                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = "库区 " + entity.InventoryAreaName + " 添加成功";
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
                        InventoryLocation entity = model as InventoryLocation;
                        entity.Creater = curUserID;
                        entity.CreateTime = DateTime.Now;
                        entity.DataVersion = 1;
                        if (db.Insert(entity))
                        {
                            TempData["Msg"] = "库区 " + entity.InventoryLocationName + " 添加成功";
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
        public ActionResult InventoryAllocation(Models.InventoryAllocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (new IdentityAuth().GetCurUserID(HttpContext, out int curUserID))
                {
                    Entities.InventoryAllocation entity = model as InventoryAllocation;
                    entity.Creater = curUserID;
                    entity.CreateTime = DateTime.Now;
                    entity.DataVersion = 1;
                    if (new DbEntities<Entities.InventoryAllocation>().SimpleClient.Insert(entity))
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
    }
}