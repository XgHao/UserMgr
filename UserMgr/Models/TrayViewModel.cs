using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class TrayViewModel : Tray
    {

        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public Tray InitAddTray(int creater)
        {
            Tray entity = this as Tray;
            entity.Creater = entity.Changer = creater;
            entity.CreateTime = entity.ChangeTime = entity.InboundTime = DateTime.Now;
            entity.DataVersion = 1;

            return entity;
        }
    }
}