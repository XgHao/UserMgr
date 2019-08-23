using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.DB;

namespace UserMgr.Controllers
{
    public class DbInitController : Controller
    {
        // GET: DbInit
        public ActionResult Init()
        {
            string path = @"c:\UserMgr\DbEntities";
            string nameSpace = "UserMgr.Entities";

            //创建数据库实体文件
            try
            {
                new DbContext().Db.DbFirst.IsCreateAttribute().IsCreateDefaultValue().CreateClassFile(path, nameSpace);
                return View(("生成实体类成功，路径：" + path)as object);
            }
            catch (Exception e)
            {
                return View(("生成实体类失败，错误消息：" + e.Message) as object);
            }
        }
    }
}