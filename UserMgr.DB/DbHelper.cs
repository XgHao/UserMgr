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
            //获取所有数据信息，并排序
            var list = Db.Queryable<T>().OrderByIF(!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder), sortName + " " + sortOrder).ToList();

            //foreach (T item in list)
            //{
            //    List<PropertyInfo> sss = typeof(T).GetProperties().ToList();
            //    object ob = sss.First().GetValue(item);
            //    string obb;
            //    var zz = sss.Where(p => p.GetValue(item).ObjToString().Contains("Page")).ToList();
            //    //var zz = sss.Where(p => p.Name.Contains("Page")).ToList();
            //    //List<PropertyInfo> qqq = sss.(p => (p.GetValue(item).ToString().Length > 2));
            //}

            //List<PropertyInfo> s = typeof(T).GetProperties().ToList();

            //list.TakeWhile(t => s.TakeWhile(p => p.GetValue(t).ObjToString().Contains(keyword)).Count() > 0);


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





        /// <summary>
        /// URl
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="Cnt"></param>
        /// <returns></returns>
        public List<Page> GetPages(string keyword, string sortName, string sortOrder, int offset, int limit, out int Cnt)
        {
            //关键词搜索
            var res = Db.Queryable<Page>().WhereIF(!string.IsNullOrEmpty(keyword), u => u.PageUrl.Contains(keyword));

            Cnt = res.Count();

            //返回当前页数据
            return res.Skip(offset)
                      .Take(limit)
                      .OrderByIF(!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder), sortName + " " + sortOrder)   //排序
                      .ToList();
        }

        public List<UserGroup> GetUserGroups(string keyword, string sortName, string sortOrder, int offset, int limit, out int Cnt)
        {
            //关键词搜索
            var res = Db.Queryable<UserGroup>().WhereIF(!string.IsNullOrEmpty(keyword), ug => ug.UserGroupName.Contains(keyword) || ug.UserGroupDesc.Contains(keyword) || ug.UserGroupCode.Contains(keyword));

            Cnt = res.Count();

            //返回当前页数据
            return res.Skip(offset)
                      .Take(limit)
                      .OrderByIF(!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder), sortName + " " + sortOrder)   //排序
                      .ToList();
        }
    }
}
