using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using UserMgr.Entities;

namespace UserMgr.DB
{
    public class DbEntities<T> : DbContext where T : class, new()
    {
        public SimpleClient<T> SimpleClient => new SimpleClient<T>(Db);
    }
}
