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
    [SugarTable("WavePickingDetail")]
    public partial class WavePickingDetail
    {
        public WavePickingDetail()
        {


        }
        /// <summary>
        /// Desc:波次明细ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long WavePickingDetailID { get; set; }

        /// <summary>
        /// Desc:波次ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        public int? WavePickingID { get; set; }

        /// <summary>
        /// Desc:出库单明细ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "出单明细单ID")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择出库明细单")]
        public int? OutboundTaskDetailID { get; set; }

        /// <summary>
        /// Desc:物资规格ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物资规格")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择物资规格")]
        public int? MaterialSizeID { get; set; }

        /// <summary>
        /// Desc:物资数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物资数量")]
        public decimal? MaterialNum { get; set; }

        /// <summary>
        /// Desc:批号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "批号")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "批号只能由4-40个数字或字母组成")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Desc:托盘明细ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "托盘明细")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择托盘明细")]
        public long? TrayDetailID { get; set; }

        /// <summary>
        /// Desc:库存ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库存")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择库存")]
        public long? InventoryID { get; set; }

        /// <summary>
        /// Desc:冰衣
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "冰衣")]
        public string Glaze { get; set; }

        /// <summary>
        /// Desc:单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "单位")]
        public string Unit { get; set; }

        /// <summary>
        /// Desc:分配数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "分配数量")]
        [RegularExpression(@"^[\+\-]?(([1-9]\d*)|\d)$",ErrorMessage = "非法的输入")]
        public decimal? QuantityAllotted { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

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
