using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UserMgr.Entities;
using UserMgr.Security;

namespace UserMgr.Areas.Member.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "登录名")]
        public string LoginUserName { get; set; }

        [Required]
        [Display(Name = "登录密码")]
        public string LoginUserPW { get; set; }

        /// <summary>
        /// 转化为对应的User对象
        /// </summary>
        /// <returns></returns>
        public User ConvertToUser()
        {
            return new User
            {
                UserName = LoginUserName,
                UserPasswd = MD5PWD.GetMD5PWD(LoginUserPW)
            };
        }
    }
}