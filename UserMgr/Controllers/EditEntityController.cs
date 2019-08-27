using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Models;
using UserMgr.DB;
using UserMgr.Security;
using UserMgr.Entities;

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
            IEnumerable<SelectListItem> userlist = GetSelectDropList(Id, out Page pageurl);
            if (userlist == null)   
            {
                return RedirectToAction("AccessMgr", "Home");
            }
            return View(new PageViewModel
            {
                PageID = pageurl.PageID,
                PageUrl = pageurl.PageUrl,
                PageName = pageurl.PageName,
                PageClass = (int)pageurl.PageClass,
                UsersList = userlist
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
            model.UsersList = GetSelectDropList(model.PageID.ToString(), out Page pageurl);
            if (ModelState.IsValid) 
            {
                pageurl.PageClass = model.PageClass;
                pageurl.PageName = model.PageName;
                pageurl.WhiteList = model.WhiteList ?? "";
                pageurl.BlackList = model.BlackList ?? "";

                if (new DbEntities().PageDb.Update(pageurl)) 
                {
                    return RedirectToAction("AccessMgr", "Home");
                }
                ModelState.AddModelError("PageClass", "更新失败");
            }
            return View(model);
        }


        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelectDropList(string id,out Page pageurl)
        {
            //查找Page对象
            var db = new DbEntities();
            pageurl = db.PageDb.GetById(id);
            if (pageurl==null)
            {
                return null;
            }

            var users = db.UserDb.GetList().Where(u => u.UserGroupID != 0 && u.IsUse);

            //下拉框模型
            var userlist = new List<SelectListItem>
            {
                new SelectListItem{ Selected = true, Text = "选择用户", Value = null }
            };

            //遍历用户列表，填充下拉框模型
            foreach (var user in users)
            {
                userlist.Add(new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.UserID.ToString()
                });
            }

            return userlist.AsEnumerable();
        }
    }
}