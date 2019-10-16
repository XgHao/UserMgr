using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Models
{
    public class UserGroupViewModel : UserGroup
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="LoggerId"></param>
        /// <returns></returns>
        public UserGroup InitAddUserGroup(int creater) => Formatterr.InitAddModel<UserGroup>(this, creater);
    }
}