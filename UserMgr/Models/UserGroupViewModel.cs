using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class UserGroupViewModel : UserGroup
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="LoggerId"></param>
        /// <returns></returns>
        public UserGroup InitAddUserGroup(int? LoggerId)
        {
            UserGroup entity = this as UserGroup;
            entity.Creater = entity.Changer = LoggerId;
            entity.ChangeTime = entity.CreateTime = DateTime.Now;

            return entity;
        }
    }
}