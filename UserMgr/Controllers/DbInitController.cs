using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;
using UserMgr.Security;

namespace UserMgr.Controllers
{
    public class DbInitController : Controller
    {
        public ActionResult Index()
        {
            //string path = @"c:\UserMgr\DbView";
            //string nameSpace = "UserMgr.Entities.View";
            return View();
        }


        /// <summary>
        /// 数据库初始化-实体类生成
        /// </summary>
        /// <returns></returns>
        public ActionResult EntitiesInit()
        {
            string path = @"c:\UserMgr\DbEntities";
            string nameSpace = "UserMgr.Entities";

            //创建数据库实体文件
            try
            {
                new DbContext().Db.DbFirst.Where(it => !it.StartsWith("View")).IsCreateAttribute().IsCreateDefaultValue().CreateClassFile(path, nameSpace);
                return View("Index", ("生成实体类成功，路径：" + path) as object);
            }
            catch (Exception e)
            {
                return View("Index", ("生成实体类失败，错误消息：" + e.Message) as object);
            }
        }

        /// <summary>
        /// 数据库初始化-视图类生成
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewInit()
        {
            string path = @"c:\UserMgr\DbView";
            string nameSpace = "UserMgr.Entities.View";

            //创建数据库实体文件
            try
            {
                new DbContext().Db.DbFirst.Where(it => it.StartsWith("View")).IsCreateAttribute().IsCreateDefaultValue().CreateClassFile(path, nameSpace);
                return View("Index", ("生成视图类成功，路径：" + path) as object);
            }
            catch (Exception e)
            {
                return View("Index", ("生成视图类失败，错误消息：" + e.Message) as object);
            }
        }

        /// <summary>
        /// 用户初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInit()
        {
            string result = string.Empty;

            var userGroupDB = new DbEntities<UserGroup>().SimpleClient;
            //没有用户组则创建“ROOT”用户组
            if (userGroupDB.GetList().Count == 0)
            {
                UserGroup userGroup = new UserGroup
                {
                    UserGroupID = 0,
                    UserGroupName = "超级用户",
                    UserGroupNo = "ROOT",
                    UserGroupClass = 0,
                    UserGroupDesc = "系统初始化生成的超级用户",
                    CreateTime = DateTime.Now
                };

                result += userGroupDB.Insert(userGroup) ? "超级用户组创建完成。\n" : "超级用户组创建失败。\n";
            }
            else
            {
                result += "超级用户组已存在。\n";
            }

            //创建用户
            var userDB = new DbEntities<User>().SimpleClient;
            if (userDB.GetList().Count == 0) 
            {
                User user = new User
                {
                    UserID = 0,
                    UserGroupID = 0,
                    UserName = "郑兴豪",
                    UserNo = "XgHao",
                    UserPasswd = MD5PWD.GetMD5PWD("root"),
                    Creater = 0,
                    IsUse = true,
                    CreateTime = DateTime.Now
                };
                result += userDB.Insert(user) ? "初始用户创建成功。\n" : "初始用户创建失败。\n";
            }
            else
            {
                result += "初始用户已存在。\n";
            }

            return View(result as object);
        }
    }
}