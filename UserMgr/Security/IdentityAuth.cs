using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using UserMgr.Entities;
using UserMgr.DB;
using UserMgr.Security;

namespace UserMgr.Security
{
    /// <summary>
    /// 自定义身份验证器
    /// </summary>
    public class IdentityAuth : AuthorizeAttribute
    {
        public string UrlName { get; set; }

        /// <summary>
        /// 验证入口
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 验证核心-具体验证逻辑
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //1.首先判断有无用户凭据
            if (httpContext.Request.IsAuthenticated)
            {
                //根据登录用户获取其用户组
                //1.获取Cookie
                var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    //解密Cookie获取凭据
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);

                    try
                    {
                        //获取登陆者用户验证信息
                        IdentityInfoModel identityInfo = new JavaScriptSerializer().Deserialize<IdentityInfoModel>(ticket.UserData);

                        //从数据库中获取当前请求的页面Url
                        string Url = httpContext.Request.Url.LocalPath;

                        //获取当前Url的Page模型
                        var pageDB = new DbEntities<Page>().SimpleClient;
                        var pagemodel = pageDB.GetList().Where(p => p.PageUrl.Equals(Url)).FirstOrDefault();

                        //判断当前Url有无权限记录，如果没有则添加
                        if (pagemodel == null)
                        {
                            Page newPage = new Page
                            {
                                PageUrl = Url,
                                PageName = string.IsNullOrEmpty(UrlName) ? "空标题" : UrlName,
                                BlackList = "",
                                WhiteList = ""
                            };

                            //添加当前url的权限记录
                            if (!pageDB.Insert(newPage)) return false;
                        }
                        else
                        {
                            //当前用户ID是否在当前页的黑名单
                            if (pagemodel.BlackList.Contains(identityInfo.CurUserID.ToString()))
                            {
                                return false;
                            }
                            //当前用户ID是否在白名单
                            else if (pagemodel.WhiteList.Contains(identityInfo.CurUserID.ToString()))
                            {
                                return true;
                            }
                            else
                            {
                                //比较权限“排名”
                                //用户访问等级不低于页面等级，返回True，否则返回False
                                return identityInfo.CurUserGroupClass <= pagemodel.PageClass ? true : false;
                            }
                        }
                    }
                    catch { return false; }
                }
            }
            else
            {
                return false;
            }

            return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// 处理未能授权的Http请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                filterContext.RequestContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl);
            }
            base.HandleUnauthorizedRequest(filterContext);
        }


        /// <summary>
        /// 获取当前用户ID [-1表示失败]
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public bool GetCurUserID(HttpContextBase httpContext,out int CurUserID)
        {
            //验证
            if (httpContext.Request.IsAuthenticated)
            {
                //获取Cookie
                var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    //解密Cookie获取凭据
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);

                    try
                    {
                        //获取登录用户的验证模型
                        IdentityInfoModel identityInfo = new JavaScriptSerializer().Deserialize<IdentityInfoModel>(ticket.UserData);
                        CurUserID = identityInfo.CurUserID;
                        return true;
                    }
                    catch {}
                }
            }
            CurUserID = -1;
            return false;
        }
    }
}