using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserMgr.Areas.Member.Models;
using UserMgr.DB;
using System.Web.Script.Serialization;
using UserMgr.Security;

namespace UserMgr.Areas.Member.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录动作[HttpPost]
        /// </summary>
        /// <param name="model">登录模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //验证模型
            if (ModelState.IsValid)
            {
                //查询
                var user = new DbEntities().UserDb.GetList().Where(u => u.UserName == model.LoginUserName && u.UserPasswd == MD5PWD.GetMD5PWD(model.LoginUserPW)).FirstOrDefault();

                if (user != null)
                {
                    //该用户是否经过审核
                    if (user.IsUse)
                    {
                        //清空Cookie
                        HttpContext.Response.Cookies.Clear();

                        //根据当前用户的id获取用户所在用户组的级别--用于判断是否有权限访问
                        var userGroup = new DbEntities().UserGroupDb.GetById(user.UserGroupID);

                        if (userGroup != null)
                        {
                            //生成用户验证信息模型
                            IdentityInfoModel infoModel = new IdentityInfoModel
                            {
                                CurUserID = user.UserID,
                                CurUserClass = userGroup.UserGroupID
                            };

                            //初始化凭据-为forms提供用户身份的票证，有效期六个小时
                            FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(1, model.LoginUserName, DateTime.Now, DateTime.Now.AddHours(6), false, new JavaScriptSerializer().Serialize(infoModel));

                            //加密该用户凭证
                            string encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);

                            //保存在Cookie中
                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            HttpContext.Response.Cookies.Add(authCookie);

                            //重定向到主页
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                    }
                    else
                    {
                        //该用户未被审核-添加错误信息
                        ModelState.AddModelError("LoginUserName", "用户" + model.LoginUserName + "为经过审核");
                    }
                }
                else
                {
                    //找不到该用户信息-添加错误信息
                    ModelState.AddModelError("LoginUserPW", "用户名不存在或密码错误");
                }
            }
            return View(model);
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册[HttpPost]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //验证模型
            if (ModelState.IsValid) 
            {
                //根据注册视图模型获取User对象
                var RegUser = model.ConvertToUser();
                RegUser.IsUse = false;
                RegUser.UserCreater = -1;   //-1代表用户自己注册
                RegUser.UserCreateTime = DateTime.Now;
                //RegUser.UserGroupID
            }

            return View(model);
        }
    }
}