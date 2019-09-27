using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;

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
            var userdb = new DbEntities<User>().SimpleClient;
            var curuser = userdb.GetById(UserID);

            if (curuser != null) 
            {
                //更新
                curuser.IsChecked = true;
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
            var userdb = new DbEntities<User>().SimpleClient;

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

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public string DeleteSupplier(string SupplierID = "")
        {
            string res = "Error";
            if (int.TryParse(SupplierID, out int sid)) 
            {
                //更新该项设置为抛弃
                int cnt = new DbEntities<Supplier>().Db
                            .Updateable<Supplier>()
                            .SetColumnsIF(new DbEntities<Supplier>().SimpleClient.GetById(sid) != null,
                                s => new Supplier()
                                {
                                    IsAbandon = true
                                }).Where(s => s.SupplierID == sid).ExecuteCommand();

                if (cnt > 0) 
                {
                    res = "OK";
                }
            }

            return res;
        }

        /// <summary>
        /// 删除物资种类
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        public string DeleteMaterialType(string MaterialTypeID = "")
        {
            string res = "Error";
            var db = new DbEntities<MaterialType>();
            if (int.TryParse(MaterialTypeID, out int mtid)) 
            {
                //更新该项设置为抛弃
                int cnt = db.Db
                            .Updateable<MaterialType>()
                            .SetColumnsIF(new DbEntities<MaterialType>().SimpleClient.GetById(mtid) != null,
                            mt => new MaterialType()
                            {
                                IsAbandon = true
                            }).Where(mt => mt.MaterialTypeID == mtid).ExecuteCommand();

                if (cnt > 0)
                {
                    res = "OK";
                }
            }

            return res;
        }
    }
}