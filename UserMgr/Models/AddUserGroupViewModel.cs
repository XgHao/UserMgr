﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class AddUserGroupViewModel
    {
        [Required]
        [Display(Name = "用户组名称")]
        [StringLength(15,MinimumLength = 1)]
        public string UserGroupName { get; set; }

        [Required]
        [Display(Name = "用户组编号")]
        [StringLength(20, MinimumLength = 1)]
        public string UserGroupCode { get; set; }

        [Required]
        [Display(Name = "访问权限")]
        public int UserGroupClass { get; set; }

        
        [Display(Name = "用户组编号")]
        [MaxLength(45)]
        public string UserGroupDesc { get; set; }

        public UserGroup ConvertUserGroup(int LoggerId)
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