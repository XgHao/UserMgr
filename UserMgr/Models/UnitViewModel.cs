using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Formatter;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class UnitViewModel : Unit
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public Unit InitAddUnit(int creater) => Formatterr.InitAddModel<Unit>(this, creater);
    }
}