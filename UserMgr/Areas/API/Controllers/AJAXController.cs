using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.DB;

namespace UserMgr.Areas.API.Controllers
{
    public class AJAXController : Controller
    {
        /// <summary>
        /// 审核用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string CheckUser(string UserID = "")
        {
            string res = "Error";

            //查找该用户
            var userdb = new DbEntities().UserDb;
            var curuser = userdb.GetById(UserID);

            if (curuser != null) 
            {
                //更新
                curuser.IsUse = true;
                if (userdb.Update(curuser))
                {
                    res = "OK";
                }
            }
            return res;
        }

        /// <summary>
        /// 拒绝用户注册请求
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string DenyUser(string UserID = "")
        {
            string res = "Error";

            //查找该用户
            var userdb = new DbEntities().UserDb;

            if (userdb.GetById(UserID) != null) 
            {
                //删除该项
                if (userdb.DeleteById(UserID))
                {
                    res = "OK";
                }
            }
            return res;
        }


        //public string ReLoadUrl(string PageID = "")
        //{

        //}
    }
}