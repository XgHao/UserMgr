using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserMgr.Areas.API.Models
{
    public class TablePaginModel<T> where T : class, new()
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// ---
        /// </summary>
        public int totalNotFiltered { get; set; }

        /// <summary>
        /// 具体数据集合
        /// </summary>
        public List<T> rows { get; set; }
    }
}