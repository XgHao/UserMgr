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
            this.IsUse = false;
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
        [RegularExpression(@"^[0-9]{1,3}$",ErrorMessage = "范围0-999")]
        public int UserGroupID { get; set; }

        /// <summary>
        /// Desc:用户编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserNo { get; set; }

        /// <summary>
        /// Desc:用户名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Desc:用户密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserPasswd { get; set; }

        /// <summary>
        /// Desc:用户编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserNum { get; set; }

        /// <summary>
        /// Desc:用户描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserDesc { get; set; }

        /// <summary>
        /// Desc:用户邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
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
        public int UserCreater { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UserCreateTime { get; set; }

    }
}
