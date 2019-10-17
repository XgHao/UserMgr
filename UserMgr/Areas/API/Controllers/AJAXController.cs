using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;
using UserMgr.Security;
using UserMgr.Models;
using UserMgr.Formatter;

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


        /// <summary>
        /// 删除入库类型
        /// </summary>
        /// <param name="InboundTypeID"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteInboundType(string InboundTypeID)
        {
            string res = "删除失败，对象不存在或者登录已过期";
            if (int.TryParse(InboundTypeID, out int id)) 
            {
                int cnt = new DbContext().Db
                            .Updateable<InboundType>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                            it => new InboundType
                            {
                                Changer = CurUserID,
                                ChangeTime = DateTime.Now,
                                DataVersion = it.DataVersion + 1,
                                IsAbandon = true
                            }).Where(it => it.InboundTypeID == id).ExecuteCommand();

                res = cnt > 0 ? "删除成功" : "操作失败";
            }

            return res;
        }


        /// <summary>
        /// 删除出库类型
        /// </summary>
        /// <param name="OutboundTypeID"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteOutboundType(string OutboundTypeID)
        {
            string res = "删除失败，对象不存在或者登录已过期";
            if (int.TryParse(OutboundTypeID, out int id))
            {
                int cnt = new DbContext().Db
                            .Updateable<OutboundType>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                            it => new OutboundType
                            {
                                Changer = CurUserID,
                                ChangeTime = DateTime.Now,
                                DataVersion = it.DataVersion + 1,
                                IsAbandon = true
                            }).Where(it => it.OutboundTypeID == id).ExecuteCommand();

                res = cnt > 0 ? "删除成功" : "操作失败";
            }

            return res;
        }


        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="ContainerID"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteContainer(string ContainerID)
        {
            string res = "删除失败，对象不存在或者登录已过期";
            if (int.TryParse(ContainerID, out int id))
            {
                int cnt = new DbContext().Db
                            .Updateable<Container>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                            it => new Container
                            {
                                Changer = CurUserID,
                                ChangeTime = DateTime.Now,
                                DataVersion = it.DataVersion + 1,
                                IsAbandon = true
                            }).Where(it => it.ContainerID == id).ExecuteCommand();

                res = cnt > 0 ? "删除成功" : "操作失败";
            }

            return res;
        }


        /// <summary>
        /// 删除巷道
        /// </summary>
        /// <param name="NarrowID"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteNarrow(string NarrowID)
        {
            string res = "删除失败，对象不存在或者登录已过期";
            if (int.TryParse(NarrowID, out int id)) 
            {
                int cnt = new DbContext().Db
                            .Updateable<Narrow>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                            it => new Narrow
                            {
                                Changer = CurUserID,
                                ChangeTime = DateTime.Now,
                                DataVersion = it.DataVersion + 1,
                                IsAbandon = true
                            }).Where(it => it.NarrowID == id).ExecuteCommand();

                res = cnt > 0 ? "删除成功" : "操作失败";
            }

            return res;
        }


        /// <summary>
        /// 删除拣货类型
        /// </summary>
        /// <param name="PickingTypeID"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeletePickingType(string PickingTypeID)
        {
            string res = "删除失败，对象不存在或者登录已过期";
            if (int.TryParse(PickingTypeID, out int id))
            {
                int cnt = new DbContext().Db
                            .Updateable<PickingType>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                            it => new PickingType
                            {
                                Changer = CurUserID,
                                ChangeTime = DateTime.Now,
                                DataVersion = it.DataVersion + 1,
                                IsAbandon = true
                            }).Where(it => it.PickingTypeID == id).ExecuteCommand();

                res = cnt > 0 ? "删除成功" : "操作失败";
            }

            return res;
        }


        /// <summary>
        /// 删除销售类型
        /// </summary>
        /// <param name="SaleTypeID"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteSaleType(string SaleTypeID)
        {
            string res = "删除失败，对象不存在或者登录已过期";
            if (int.TryParse(SaleTypeID, out int id))
            {
                int cnt = new DbContext().Db
                            .Updateable<SaleType>()
                            .SetColumnsIF(new IdentityAuth().GetCurUserID(HttpContext, out int CurUserID),
                            it => new SaleType
                            {
                                Changer = CurUserID,
                                ChangeTime = DateTime.Now,
                                DataVersion = it.DataVersion + 1,
                                IsAbandon = true
                            }).Where(it => it.SaleTypeID == id).ExecuteCommand();

                res = cnt > 0 ? "删除成功" : "操作失败";
            }

            return res;
        }
    }
}