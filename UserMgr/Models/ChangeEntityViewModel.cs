using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserMgr.Models
{
    /// <summary>
    /// 编辑基础资料模型
    /// </summary>
    public class ChangeEntityViewModel
    {
        /// <summary>
        /// 结果标识
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 对象ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>
        public string Content { get; set; }
    }
}