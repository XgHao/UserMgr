using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Formatter;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class TrayTypeViewModel : TrayType
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public TrayType InitAddTrayType(int creater) => Formatterr.InitAddModel<TrayType>(this, creater);
    }
}