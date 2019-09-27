using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;
using UserMgr.Security;

namespace UserMgr.Areas.Member.Models
{
    public class RegisterViewModel : User
    {
        [Required]
        [Display(Name = "确认密码")]
        [Compare("UserPasswd", ErrorMessage = "两次密码输入不一致")]
        public string ConfirmUserPasswd { get; set; }

        /// <summary>
        /// 转换为对应的User对象
        /// </summary>
        /// <returns></returns>
        public User InitAddUser()
        {
            User entity = this as User;
            entity.UserGroupID = 1;
            entity.UserPasswd = MD5PWD.GetMD5PWD(entity.UserPasswd);
            entity.IsChecked = false;
            entity.Changer = entity.Creater = -1;
            entity.CreateTime =entity.ChangeTime = DateTime.Now;

            return entity;
        }
    }
}