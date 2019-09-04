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
    [SugarTable("Page")]
    public partial class Page
    {
        public Page()
        {
            this.PageClass = Convert.ToInt32("999");
        }
        /// <summary>
        /// Desc:页面ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [Required]
        public int PageID { get; set; }

        /// <summary>
        /// Desc:页面URL
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        public string PageUrl { get; set; }

        /// <summary>
        /// Desc:页面标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "页面名称")]
        public string PageName { get; set; }

        /// <summary>
        /// Desc:页面访问等级
        /// Default:999
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "访问权限")]
        [RegularExpression(@"^[0-9]{1,3}$",ErrorMessage = "权限范围0-999")]
        public int? PageClass { get; set; }

        /// <summary>
        /// Desc:黑名单
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BlackList { get; set; }

        /// <summary>
        /// Desc:白名单
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WhiteList { get; set; }

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
