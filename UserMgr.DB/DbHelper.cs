using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Entities;
using SqlSugar;

namespace UserMgr.DB
{
    public class DbHelper : DbContext
    {
        public List<Page> GetPages(string keyword,string sortName,string sortOrder,int offset,int limit,out int Cnt)
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

    }
}
