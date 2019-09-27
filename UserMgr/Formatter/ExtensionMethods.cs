using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserMgr.Formatter
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// 判断当前int? 是否是null或者-1
        /// -1代表未选择
        /// </summary>
        /// <param name="curID"></param>
        /// <returns></returns>
        public static bool IsNullOrUnchecked(this int? curID) => (curID == null || curID == -1) ? true : false;
    }
}