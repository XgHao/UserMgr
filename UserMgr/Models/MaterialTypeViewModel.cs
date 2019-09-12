using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class MaterialTypeViewModel : MaterialType
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="CurUserID"></param>
        /// <returns></returns>
        public MaterialType InitAddMaterialType(int? CurUserID)
        {
            MaterialType entity = this as MaterialType;
            entity.MaterialTypeRoot = MaterialTypeRoot == -1 ? null : entity.MaterialTypeRoot;
            entity.Creater = entity.Changer = CurUserID;
            entity.CreateTime = entity.ChangeTime = DateTime.Now;
            entity.DataVersion = 1;

            return entity;
        }
    }
}