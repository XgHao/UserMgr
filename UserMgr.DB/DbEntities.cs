using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using UserMgr.Entities;

namespace UserMgr.DB
{
    public class DbEntities : DbContext
    {


        public SimpleClient<Menu> MenuDb
        {
            get
            {
                return new SimpleClient<Menu>(Db);
            }
        }

        public SimpleClient<MenuGroup> MenuGroupDb
        {
            get
            {
                return new SimpleClient<MenuGroup>(Db);
            }
        }

        public SimpleClient<User> UserDb
        {
            get
            {
                return new SimpleClient<User>(Db);
            }
        }

        public SimpleClient<UserGroup> UserGroupDb
        {
            get
            {
                return new SimpleClient<UserGroup>(Db);
            }
        }

        public SimpleClient<Page> PageDb
        {
            get
            {
                return new SimpleClient<Page>(Db);
            }
        }
    }
}
