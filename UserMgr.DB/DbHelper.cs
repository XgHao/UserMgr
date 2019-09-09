using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Entities;
using SqlSugar;
using Newtonsoft.Json;
using System.Reflection;

namespace UserMgr.DB
{
    public class DbHelper : DbContext
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">接受一个实体模型</typeparam>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="sortName">排序名</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="offset">跳过几条</param>
        /// <param name="limit">选取几条</param>
        /// <param name="Cnt">总条数</param>
        /// <returns></returns>
        public List<T> GetDatas<T>(string keyword, string sortName, string sortOrder, int offset, int limit, out int Cnt) where T : class, new()
        {
            //判断当前类模型有无IsAbandon属性，有的话查找IsAbandon=false的记录
            //var lisst = Db.Queryable<T>().WhereIF(typeof(T).GetProperty("IsAbandon") != null, t => !t.GetType().GetProperty("IsAbandon").GetValue(t).ObjToBool());
            Type entity = typeof(T);
            string sql = "select * from " + entity.Name + (entity.GetProperty("IsAbandon") != null ? " where IsAbandon = 0" : "");
            //获取所有数据信息，并排序
            var list = Db.SqlQueryable<T>(sql).OrderByIF(!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder), sortName + " " + sortOrder).ToList();


            //遍历搜索
            List<T> newlist = new List<T>();
            foreach (var item in list)
            {
                foreach (var pro in typeof(T).GetProperties())
                {
                    var str = pro.GetValue(item).ObjToString();
                    if (str.Contains(keyword))
                    {
                        newlist.Add(item);
                        break;
                    }
                }
            }

            //数据总数
            Cnt = newlist.Count();

            return newlist.Skip(offset).Take(limit).ToList();
        }
    }
}
