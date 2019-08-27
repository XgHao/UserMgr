using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Models;
using UserMgr.Security;
using System.Web.Mvc;
using UserMgr.DB;

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
                    var db = new DbEntities().UserGroupDb;
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
                            ModelState.AddModelError("Msg", "添加成功");
                            return View(new UserGroupViewModel());
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
    }
}