﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Entities.View;

namespace UserMgr.Models
{
    public class InboundTaskDetailViewModel : InboundTaskDetail
    {
        /// <summary>
        /// 入库任务单编号
        /// </summary>
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


        #region 视图模型
        /// <summary>
        /// 入库任务细节模型
        /// </summary>
        public View_InboundTaskDetail InboundTaskDetail { get; set; }

        /// <summary>
        /// 入库任务模型
        /// </summary>
        public View_InboundTask InboundTask { get; set; }
        #endregion
    }
}