using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Models
{
    public class MaterialTypeViewModel : MaterialType
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="CurUserID"></param>
        /// <returns></returns>
        public MaterialType InitAddMaterialType(int creater)
        {
            var entity = Formatterr.InitAddModel<MaterialType>(this, creater);
            entity.MaterialTypeRoot = MaterialTypeRoot == -1 ? null : entity.MaterialTypeRoot;

            return entity;
        }
    }
}