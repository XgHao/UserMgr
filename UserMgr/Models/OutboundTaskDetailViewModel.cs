using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class OutboundTaskDetailViewModel : OutboundTaskDetail
    {
        /// <summary>
        /// 出库任务单编号
        /// </summary>
        public string OutboundTaskInfo_OutboundTaskNo { get; set; }

        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public OutboundTaskDetail InitAddOutboundTaskDetail(int creater)
        {
            OutboundTaskDetail entity = this as OutboundTaskDetail;
            entity.Status = 1;
            entity.DataVersion = 1;
            entity.Changer = entity.Creater = creater;
            entity.ChangeTime = entity.CreateTime = DateTime.Now;

            return entity;
        }
    }
}