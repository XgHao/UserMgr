using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;
using System.Reflection;

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

        /// <summary>
        /// 尝试添加属性
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="entity"></param>
        /// <param name="obj"></param>
        public static void TrySetValue(this PropertyInfo propertyInfo, object entity, object obj)
        {
            if (propertyInfo != null) 
            {
                try
                {
                    propertyInfo.SetValue(entity, obj);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}