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
    [SugarTable("UserGroup")]
    public partial class UserGroup
    {
        public UserGroup()
        {


        }
        /// <summary>
        /// Desc:用户组ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [Required]
        public int UserGroupID { get; set; }

        /// <summary>
        /// Desc:用户组名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "用户组名称")]
        [StringLength(15,MinimumLength = 1)]
        public string UserGroupName { get; set; }

        /// <summary>
        /// Desc:用户组编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "用户组编码")]
        [StringLength(20,MinimumLength = 1)]
        public string UserGroupNo { get; set; }

        /// <summary>
        /// Desc:用户组等级（用于识别访问权限）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "访问权限")]
        [RegularExpression(@"^[0-9]{1,3}$",ErrorMessage = "权限范围0-999")]
        public int? UserGroupClass { get; set; }

        /// <summary>
        /// Desc:菜单组编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuGroupCode { get; set; }

        /// <summary>
        /// Desc:用户组描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Display(Name = "用户组描述")]
        [MaxLength(45)]
        public string UserGroupDesc { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Creater { get; set; }

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
