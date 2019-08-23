using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;
using UserMgr.Security;

namespace UserMgr.Areas.Member.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20,ErrorMessage = "用户名长度在2-20之间",MinimumLength = 2)]
        public string RegisterUserName { get; set; }

        [Required]
        [StringLength(16,ErrorMessage = "密码长度在8-16之间",MinimumLength = 8)]
        public string RegisterUserPW { get; set; }

        [Required]
        [Compare("RegisterUserPW",ErrorMessage = "两次密码输入不一致")]
        public string ConfirmRegisterUserPW { get; set; }

        [Required]
        [EmailAddress]
        public string RegisterEmail { get; set; }

        /// <summary>
        /// 转换为对应的User对象
        /// </summary>
        /// <returns></returns>
        public User ConvertToUser()
        {
            return new User
            {
                UserName = RegisterUserName,
                UserPasswd = MD5PWD.GetMD5PWD(RegisterUserPW),
                UserEmail = RegisterEmail,
            };
        }
    }
}