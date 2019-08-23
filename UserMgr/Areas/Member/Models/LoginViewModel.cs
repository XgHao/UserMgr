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
        [Required(ErrorMessage = "请输入用户名")]
        public string LoginUserName { get; set; }

        [Required(ErrorMessage = "请输入登录密码")]
        //[StringLength(16, ErrorMessage = "密码长度在8-16位", MinimumLength = 8)]
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