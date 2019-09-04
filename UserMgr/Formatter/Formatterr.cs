using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserMgr.Formatter
{
    public static class Formatterr
    {
        /// <summary>
        /// 实体类转换为视图类-父类转换为子类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T ConvertToViewModel<T>(object entity) where T : class, new()
        {
            T model = new T();

            foreach (var item in typeof(T).GetProperties())
            {
                try
                {
                    item.SetValue(model, item.GetValue(entity));
                }
                catch
                {
                    throw;
                }
            }
            return model;
        }
    }
}