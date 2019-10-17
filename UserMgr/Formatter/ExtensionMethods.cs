using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;
using System.Reflection;
using System.Web.Mvc;

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

        /// <summary>
        /// 验证实体模型中的部分属性
        /// </summary>
        /// <param name="ModelState"></param>
        /// <param name="keys">要验证的属性集合</param>
        /// <returns></returns>
        public static bool IsPartValid(this ModelStateDictionary ModelState, List<string> keys)
        {
            //遍历要验证部分属性
            foreach (var item in keys)
            {
                //尝试获取对应键的值
                if (ModelState.TryGetValue(item, out ModelState modelState) && modelState.Errors.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}