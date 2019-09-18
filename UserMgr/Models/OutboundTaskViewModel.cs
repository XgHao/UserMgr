using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class OutboundTaskViewModel : OutboundTask
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public OutboundTask InitAddOutboundTask(int creater)
        {
            OutboundTask entity = this as OutboundTask;
            entity.Status = 1;
            entity.Creater = entity.Changer = creater;
            entity.CreateTime = entity.ChangeTime = DateTime.Now;
            entity.DataVersion = 1;

            return entity;
        }
    }
}