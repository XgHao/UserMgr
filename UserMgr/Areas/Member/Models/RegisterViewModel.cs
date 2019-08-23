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
        [Display(Name = "用户名")]
        [StringLength(20,ErrorMessage = "用户名长度在2-20之间",MinimumLength = 2)]
        public string RegisterUserName { get; set; }

        [Required]
        [Display(Name = "用户密码")]
        [StringLength(16,ErrorMessage = "密码长度在8-16之间",MinimumLength = 8)]
        public string RegisterUserPW { get; set; }

        [Required]
        [Display(Name = "确认密码")]
        [Compare("RegisterUserPW",ErrorMessage = "两次密码输入不一致")]
        public string ConfirmRegisterUserPW { get; set; }

        [Required]
        [Display(Name = "电子邮箱")]
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