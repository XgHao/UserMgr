using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Models
{
    public class TrayDetailViewModel : TrayDetail
    {

        public string CurTrayInfo { get; set; }

        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public TrayDetail InitAddTrayDetail(int creater)
        {
            return Formatterr.InitAddModel<TrayDetail>(this, creater);
        }
    }
}