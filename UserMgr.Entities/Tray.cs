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
    [SugarTable("Tray")]
    public partial class Tray
    {
        public Tray()
        {
            Status = 1;
        }
        /// <summary>
        /// Desc:托盘ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long TrayID { get; set; }

        /// <summary>
        /// Desc:托盘类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "托盘类型")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择托盘类型")]
        public int? TrayType { get; set; }

        /// <summary>
        /// Desc:托盘编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "托盘编号")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "型号只能由4-40个数字或字母组成")]
        public string TrayNo { get; set; }

        /// <summary>
        /// Desc:托盘条码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "托盘条码")]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "条码为0-9999999999")]
        public string TrayCode { get; set; }

        /// <summary>
        /// Desc:容器号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择容器")]
        public int? Container { get; set; }

        /// <summary>
        /// Desc:重量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "重量")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// Desc:高度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "高度")]
        public decimal? Height { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

        /// <summary>
        /// Desc:入库时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? InboundTime { get; set; }

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

        /// <summary>
        /// Desc:数据版本
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? DataVersion { get; set; }

    }
}
