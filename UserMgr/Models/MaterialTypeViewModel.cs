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
        public MaterialType ConvertToMaterialType(int? CurUserID)
        {
            return new MaterialType
            {
                MaterialTypeName = MaterialTypeName,
                MaterialTypeCode = MaterialTypeCode,
                MaterialTypePrice = MaterialTypePrice,
                MaterialTypeRoot = MaterialTypeRoot == -1 ? null : MaterialTypeRoot,
                Creater = CurUserID,
                CreateTime = DateTime.Now,
                DataVersion = 1
            };
        }
    }
}