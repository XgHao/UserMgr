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
        public UserGroup ConvertUserGroup(int? LoggerId)
        {
            return new UserGroup
            {
                UserGroupName = UserGroupName,
                UserGroupCode = UserGroupCode,
                UserGroupClass = UserGroupClass,
                UserGroupDesc = UserGroupDesc,
                UserGroupCreater = LoggerId,
                UserGroupCreateTime = DateTime.Now
            };
        }
    }
}