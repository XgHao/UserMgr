using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserMgr.DB;
using UserMgr.Entities;

namespace UserMgr.Formatter
{
    public static class SetSelectListItems
    {
        /// <summary>
        /// 物资种类
        /// </summary>
        /// <param name="controller">当前控制器</param>
        /// <param name="curMaterialTypeID">当前正在修改的种类ID，空值不传</param>
        public static void MaterialType(ControllerBase controller, int? curMaterialTypeID = null)
        {
            //生成物资List
            List<SelectListItem> MaterialList = new List<SelectListItem>
            {
                new SelectListItem{ Selected=true,Text="选择物资种类",Value="-1" }
            };

            //过滤当前ID，如果有
            var db = new DbEntities<MaterialType>().SimpleClient;
            var lists = curMaterialTypeID == null ? db.GetList() : db.GetList().Where(mt => mt.MaterialTypeID != curMaterialTypeID);

            foreach (var item in lists)
            {
                MaterialList.Add(new SelectListItem
                {
                    Text = item.MaterialTypeName,
                    Value = item.MaterialTypeID.ToString()
                });
            }

            controller.ViewData["MaterialTypeSelectList"] = MaterialList;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name = "controller" >当前控制器</ param >
        /// <param name="id">ULR管理ID</param>
        /// <param name="isSkipNotUse">是否跳过未审核用户</param>
        /// <returns></returns>
        public static Page User(ControllerBase controller,string id, bool isSkipNotUse = true)
        {
            var page = new DbEntities<Page>().SimpleClient.GetById(id);

            if (page != null)
            {
                var db = new DbEntities<User>().SimpleClient;

                //是否跳过未审核的用户
                var users = isSkipNotUse ? db.GetList().Where(u => u.UserGroupID != 0 && u.IsUse) : db.GetList().Where(u => u.UserGroupID != 0);

                List<SelectListItem> userlist = new List<SelectListItem>
                {
                    new SelectListItem{ Selected=true,Text="选择用户",Value="-1" }
                };

                //遍历用户
                foreach (var user in users)
                {
                    userlist.Add(new SelectListItem
                    {
                        Text = user.UserName,
                        Value = user.UserID.ToString()
                    });
                }

                controller.ViewData["UserSelectList"] = userlist;
            }
            return page;
        }

        /// <summary>
        /// 常用单位
        /// </summary>
        /// <param name="controller"></param>
        public static void UnitList(ControllerBase controller)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>
            {
                new SelectListItem{ Selected=true,Text="选择常用单位",Value="-1" }
            };

            var lists = new DbEntities<Unit>().SimpleClient.GetList();
            foreach (var item in lists)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.UnitName,
                    Value = item.UnitName
                });
            }

            controller.ViewData["UnitList"] = selectListItems;
        }
    }
}