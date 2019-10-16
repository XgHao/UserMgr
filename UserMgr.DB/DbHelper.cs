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
        /// <param name="ExterSql">额外的Sql语句</param>
        /// <returns></returns>
        public List<T> GetDatas<T>(string keyword, string sortName, string sortOrder, int offset, int limit, out int Cnt, string ExterSql = null) where T : class, new()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Type typeT = typeof(T);

            //查询组合
            stringBuilder.Append($"SELECT * FROM [{typeT.Name}] WHERE 1=1");
            stringBuilder.Append(string.IsNullOrEmpty(ExterSql) ? $"" : $" AND {ExterSql}");
            stringBuilder.Append(typeT.GetProperty("IsAbandon") != null ? " AND IsAbandon = 0" : "");

            var list = Db.SqlQueryable<T>(stringBuilder.ToString()).OrderByIF(!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder), $"{sortName} {sortOrder}").ToList();

            if (string.IsNullOrEmpty(keyword))
            {
                Cnt = list.Count();
                return list.Skip(offset).Take(limit).ToList();
            }
            //遍历搜索
            else
            {
                List<T> newlist = new List<T>();
                foreach (var item in list)
                {
                    foreach (var pro in typeT.GetProperties())
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


        /// <summary>
        /// 返回现有数据，即IsAbandon=false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>() where T : class, new()
        {
            //判断当前类模型有无IsAbandon属性，有的话查找IsAbandon=false的记录
            Type typeT = typeof(T);

            StringBuilder stringBuilder = new StringBuilder($"SELECT * FROM [{typeT.Name}] WHERE 1=1");
            stringBuilder.Append(typeT.GetProperty("IsAbandon") != null ? $" AND IsAbandon = 0" : $"");

            return Db.SqlQueryable<T>(stringBuilder.ToString()).ToList();
        }
    }
}
