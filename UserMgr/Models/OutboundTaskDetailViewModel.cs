using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Entities.View;
using UserMgr.Formatter;

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
            return Formatterr.InitAddModel<OutboundTaskDetail>(this, creater);
        }


        #region 视图模型
        /// <summary>
        /// 出库任务细节模型
        /// </summary>
        public View_OutboundTaskDetail OutboundTaskDetail { get; set; }

        /// <summary>
        /// 出库任务模型
        /// </summary>
        public View_OutboundTask OutboundTask { get; set; }
        #endregion
    }
}