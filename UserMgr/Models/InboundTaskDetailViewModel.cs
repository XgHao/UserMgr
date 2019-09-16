using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class InboundTaskDetailViewModel : InboundTaskDetail
    {
        public string InboundTaskInfo_InboundTaskNo { get; set; }

        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        public InboundTaskDetail InitAddInboundTaskDetail(int creater)
        {
            InboundTaskDetail entity = this as InboundTaskDetail;
            entity.Status = 1;
            entity.DataVersion = 1;
            entity.Changer = entity.Creater = creater;
            entity.ChangeTime = entity.CreateTime = DateTime.Now;

            return entity;
        }
    }
}