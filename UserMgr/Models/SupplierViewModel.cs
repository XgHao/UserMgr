using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Models
{
    public class SupplierViewModel : Supplier
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="curUserID"></param>
        /// <returns></returns>
        public Supplier InitAddSupplier(int creater)
        {
            return Formatterr.InitAddModel<Supplier>(this, creater);
        }
    }
}