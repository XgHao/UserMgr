using System;
using System.Linq;
using System.Text;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("User")]
    public partial class User
    {
        public User()
        {
            IsUse = false;
        }
        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// Desc:用户组ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "范围0-999")]
        public int UserGroupID { get; set; }

        /// <summary>
        /// Desc:用户编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserCode { get; set; }

        /// <summary>
        /// Desc:用户名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "用户名")]
        [StringLength(20, ErrorMessage = "用户名长度在2-20之间", MinimumLength = 2)]
        public string UserName { get; set; }

        /// <summary>
        /// Desc:用户密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required]
        [Display(Name = "用户密码")]
        [StringLength(16, ErrorMessage = "密码长度在8-16之间", MinimumLength = 8)]
        public string UserPasswd { get; set; }

        /// <summary>
        /// Desc:用户编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserNo { get; set; }

        /// <summary>
        /// Desc:用户描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        [MaxLength(50)]
        [Display(Name = "备注")]
        public string UserDesc { get; set; }

        /// <summary>
        /// Desc:用户邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "电子邮箱")]
        [EmailAddress]
        public string UserEmail { get; set; }

        /// <summary>
        /// Desc:用户联系方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserPhoneNum { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsUse { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Creater { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:修改人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Changer { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ChangeTime { get; set; }
    }
}
