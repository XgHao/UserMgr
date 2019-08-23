using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SqlSugar;

namespace UserMgr.DB
{
    /// <summary>
    /// 数据库连接-配置信息
    /// </summary>
    public class DbContext
    {
        private static ConnectionConfig ConConfig = new ConnectionConfig
        {
            ConnectionString = ConfigurationManager.AppSettings["DBConn"].ToString(),
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        };

        /// <summary>
        /// 连接实例
        /// </summary>
        public SqlSugarClient Db { get; }

        public DbContext()
        {
            Db = new SqlSugarClient(ConConfig);
        }
    }
}
